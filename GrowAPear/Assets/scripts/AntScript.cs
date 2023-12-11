using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntScript : MonoBehaviour
{
	[SerializeField] private float speed = 6f;
	[SerializeField] private float meleeRange = 4f;
	[SerializeField] private Animator animator;
	[SerializeField] private EnemyHealth enemyHealth;

	private Transform player;

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

		if (distanceToPlayer > meleeRange)
		{
			animator.Play("Skeeter-FlyingAnim");
			ChasePlayer();
		}
		else
		{
			StartCoroutine(MeleeAttack());
		}
	}

	private void ChasePlayer()
	{
		transform.LookAt(player);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
				playerHealth.TakeDamage(20);

				if (playerHealth.HasChili)
				{
					enemyHealth.TakeDamage(5);
				}
			}
			else if (hit.collider.CompareTag("Enemy") && playerHealth.HasBerry)
			{
				hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(10);
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
