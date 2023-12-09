using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoScript : MonoBehaviour
{
	[SerializeField] private float speed = 3f;
	[SerializeField] private float shootingRange = 10f;
	[SerializeField] private float meleeRange = 2f;
	[SerializeField] private Animator animator;

	private Transform player;
	private MosquitoBulletScript bulletPrefab;
	private bool isMelee = false;

	[SerializeField] private Transform shootPoint;
	private bool inAction = false;

	private PlayerHealth playerHealth;
	private EnemySpawner enemySpawner;

	public void Init(Transform playerTransform, MosquitoBulletScript bullet, EnemySpawner es)
	{
		player = playerTransform;
		playerHealth = player.GetComponent<PlayerHealth>();
		bulletPrefab = bullet;
		isMelee = Random.value < 0.5f;
		enemySpawner = es;
	}

	private void Update()
	{
		if (player == null || inAction)
		{
			return;
		}

		float distanceToPlayer = Vector3.Distance(transform.position, player.position);

		if (isMelee)
		{
			if (distanceToPlayer > meleeRange)
			{
				animator.Play("Skeeter-FlyingAnim");
				ChasePlayer();
			}
			else
			{
				StartCoroutine(MeleeAttack());
			}

			return;
		}

		if (distanceToPlayer < shootingRange)
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
		yield return new WaitForSeconds(0.5f);
		var bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
		bullet.Init(playerHealth);
		yield return new WaitForSeconds(0.5f);
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
				playerHealth.TakeDamage(10);
			}
		}

		yield return new WaitForSeconds(0.5f);
		animator.Play("Skeeter-FlyingAnim");
		yield return new WaitForSeconds(0.5f);
		inAction = false;
	}

	public void Die()
	{
		enemySpawner.EnemyDied();
		Destroy(gameObject);
	}
}
