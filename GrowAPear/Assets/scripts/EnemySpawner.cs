using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private MosquitoScript mosquitoPrefab;
    [SerializeField] private GameObject insectPrefab;
	[SerializeField] private MosquitoBulletScript bulletPrefab;

	[SerializeField] private int maxMosquitoCount = 5;
    [SerializeField] private int maxInsectCount = 5;


    [SerializeField] private int minTimeToSpawn = 2;
    [SerializeField] private int maxTimeToSpawn = 5;

    [SerializeField] private List<Transform> mosquitoSpawnPositions = new();
    [SerializeField] private List<Transform> insectSpawnPositions = new();

    private int numMosquitoSpawned = 0;
    private int numInsectSpawned = 0;

    private int totalEnemiesCount;
    private int enemiesDead = 0;

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
		totalEnemiesCount = maxMosquitoCount + maxInsectCount;
        StartCoroutine(SpawnerCoroutine());
	}

    IEnumerator SpawnerCoroutine()
    {
        while (player  == null)
        {
            yield return new WaitForSeconds(1f);
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
        bool canSpawnMosquito = true;
        bool canSpawnInsect = true;

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

        if (enemiesDead == totalEnemiesCount)
        {
            // WIN CONDITION
        }
    }

}
