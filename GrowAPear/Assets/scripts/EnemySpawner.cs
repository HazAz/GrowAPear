using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MosquitoScript mosquitoPrefab;
    [SerializeField] private SkeeterBossScript skeeterBossPrefab;
	[SerializeField] private MosquitoBulletScript bulletPrefab;

	[SerializeField] private GameObject insectPrefab;

	[SerializeField] private int maxMosquitoCountWave1= 5;
	[SerializeField] private int maxMosquitoCountWave2 = 10;
    [SerializeField] private int maxInsectCountWave1 = 5;
    [SerializeField] private int maxInsectCountWave2 = 10;


    [SerializeField] private int minTimeToSpawn = 2;
    [SerializeField] private int maxTimeToSpawn = 5;

    [SerializeField] private List<Transform> mosquitoSpawnPositions = new();
    [SerializeField] private List<Transform> insectSpawnPositions = new();

    private int numMosquitoSpawned = 0;
    private int numInsectSpawned = 0;

    private int totalEnemiesCountWave1;
    private int totalEnemiesCountWave2;
    private int currentTotalEnemiesCount;
    private int enemiesDead = 0;

    private int wave = 1;

    [SerializeField] private Transform player;
    [SerializeField] private PowerupScripts playerPowerupScripts;

    // Start is called before the first frame update
    void Start()
    {
		totalEnemiesCountWave1 = maxMosquitoCountWave1 + maxInsectCountWave1;
		totalEnemiesCountWave2 = maxMosquitoCountWave2 + maxInsectCountWave2;
        currentTotalEnemiesCount = totalEnemiesCountWave1;
        StartCoroutine(SpawnerCoroutine());
	}

    IEnumerator SpawnerCoroutine()
    {
        while (player == null)
        {
            yield return new WaitForSeconds(1f);
			player = GameObject.FindGameObjectWithTag("Player").transform;
			playerPowerupScripts = player.GetComponent<PowerupScripts>();
		}

        bool canSpawnMosquito = true;
        bool canSpawnInsect = true;

        int maxMosquitoCount = wave == 1 ? maxMosquitoCountWave1 : maxMosquitoCountWave2;
        int maxInsectCount = wave == 1 ? maxInsectCountWave1 : maxInsectCountWave2;
        int totalEnemiesCount = wave == 1 ? totalEnemiesCountWave1 : totalEnemiesCountWave2;

        while (numMosquitoSpawned + numInsectSpawned < totalEnemiesCount)
        {
            yield return new WaitForSeconds(Random.Range(minTimeToSpawn, maxTimeToSpawn));

            if (numMosquitoSpawned >= maxMosquitoCount)
            {
                canSpawnMosquito = false;
            }

			if (numInsectSpawned >= maxInsectCount)
			{
				canSpawnInsect = false;
			}

            if (canSpawnMosquito && !canSpawnInsect)
            {
                SpawnMosquito();
			}
            else if (!canSpawnMosquito && canSpawnInsect)
            {
                SpawnInsect();
			}
            else
            {
                var chance = Random.value;
                if (chance < 0.5f)
                {
                    SpawnMosquito();
				}
                else
                {
                    SpawnInsect();
				}
            }
		}
    }

    private void SpawnMosquito()
    {
		var mosquito = Instantiate(mosquitoPrefab, GetPositionForMosquito(), Quaternion.identity);
        mosquito.Init(player, bulletPrefab, this);
		++numMosquitoSpawned;
	}

    private void SpawnInsect()
    {

		Instantiate(insectPrefab, GetPositionForInsect(), Quaternion.identity);
		++numInsectSpawned;
	}

    private Vector3 GetPositionForMosquito()
    {
        if (mosquitoSpawnPositions.Count == 0)
        {
            return Vector3.zero;
        }

        return mosquitoSpawnPositions[(int)Random.Range(0, mosquitoSpawnPositions.Count)].position;
    }

	private Vector3 GetPositionForInsect()
	{
		if (insectSpawnPositions.Count == 0)
		{
			return Vector3.zero;
		}

		return insectSpawnPositions[(int)Random.Range(0, insectSpawnPositions.Count)].position;
    }

    public void EnemyDied()
    {
        enemiesDead++;

        if (enemiesDead == currentTotalEnemiesCount)
        {
            WaveEnded();
        }
    }

    private void WaveEnded()
    {
        if (wave < 3)
        {
            playerPowerupScripts.CreatePowerupPanelScript(onComplete: CompleteWave);
        }
    }

    private void CompleteWave()
    {
        switch (wave)
        {
            case 1:
                ++wave;
				currentTotalEnemiesCount = totalEnemiesCountWave2;
                ResetParams();
				StartCoroutine(SpawnerCoroutine());
                break;

            case 2:
                ++wave;
                enemiesDead = 0;
				ResetParams();
				SpawnFinalBoss();
                break;

            case 3:
                // next scene
                break;
	    }
    }

    private void ResetParams()
    {
		enemiesDead = 0;
        numInsectSpawned = 0;
        numMosquitoSpawned = 0;
	}

    private void SpawnFinalBoss()
    {
        currentTotalEnemiesCount = 0;

        if (skeeterBossPrefab != null)
        {
			var skeeterBoss = Instantiate(skeeterBossPrefab, GetPositionForMosquito(), Quaternion.identity);
			skeeterBoss.Init(player, this);
            currentTotalEnemiesCount++;
		}
    }
}
