using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeeterBossScript : MonoBehaviour
{
	[SerializeField] private float speed = 8f;
	[SerializeField] private float shootingRange = 15f;
	[SerializeField] private float meleeRange = 4f;
	[SerializeField] private Animator animator;

	private Transform player;
	[SerializeField] private MosquitoBulletScript bulletPrefab;

	[SerializeField] private Transform shootPoint;
	private bool inAction = false;

	private PlayerHealth playerHealth;
	private EnemySpawner enemySpawner;

	public void Init(Transform playerTransform, EnemySpawner es)
	{
		player = playerTransform;
		playerHealth = player.GetComponent<PlayerHealth>();
		enemySpawner = es;
	}

	private void Update()
	{
		if (player == null || inAction)
		{
			return;
		}

		float distanceToPlayer = Vector3.Distance(transform.position, player.position);

		if (distanceToPlayer < meleeRange)
		{
			StartCoroutine(MeleeAttack());
			
		}
		else if(distanceToPlayer < shootingRange)
		{
			StartCoroutine(Shoot());
		}
		else
		{
			animator.Play("Skeeter-FlyingAnim");
			ChasePlayer();
		}
	}

	private void ChasePlayer()
	{
		transform.LookAt(player);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	private IEnumerator Shoot()
	{
		inAction = true;
		// Play animation
		yield return new WaitForSeconds(1f);
		var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
		bullet.Init(playerHealth);
		yield return new WaitForSeconds(2f);
		inAction = false;
	}

	private IEnumerator MeleeAttack()
	{
		inAction = true;
		animator.Play("Skeeter-Melee");
		yield return new WaitForSeconds(0.6f);

		if (Physics.Raycast(transform.position, transform.forward, out var hit, meleeRange))
		{
			// Check if the hit object is the player
			if (hit.collider.CompareTag("Player"))
			{
				playerHealth.TakeDamage(25);
			}
		}

		yield return new WaitForSeconds(0.5f);
		animator.Play("Skeeter-FlyingAnim");
		yield return new WaitForSeconds(2.5f);
		inAction = false;
	}

	public void Die()
	{
		enemySpawner.EnemyDied();
		Destroy(gameObject);
	}
}