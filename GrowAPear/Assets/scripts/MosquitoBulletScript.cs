using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoBulletScript : MonoBehaviour
{
	[SerializeField] private float minSpeed = 3f;
	[SerializeField] private float maxSpeed = 5f;
	[SerializeField] private float destroyTime = 3f;

	private Vector3 targetPosition;
	private float speed = 0f;
	private bool isBoss = false;
	private PlayerHealth playerHealth;
	private Transform shooterTransform;

	public void Init(PlayerHealth health, Transform st = null, bool boss = false)
	{
		playerHealth = health;
		targetPosition = (playerHealth.transform.position - transform.position).normalized;
		speed = Random.Range(minSpeed, maxSpeed);
		shooterTransform = st;
		isBoss = boss;
		Invoke("Destroy", destroyTime);
	}

	private void Update()
	{
		transform.position += targetPosition * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			TryHitPlayer();
		}
		else if (other.CompareTag("Enemy") && playerHealth.HasOrange)
		{
			other.GetComponent<EnemyHealth>()?.TakeDamage(5);
			Destroy(gameObject);
		}
	}

	private void TryHitPlayer()
	{
		if (playerHealth.HasPepper)
		{
			if (!isBoss && Random.value < 0.25f)
			{
				targetPosition = (shooterTransform.position - transform.position).normalized;
				CancelInvoke("Destroy");
				Invoke("Destroy", destroyTime);
			}
			else
			{
				playerHealth.TakeDamage(5);
				Destroy(gameObject);
			}
		}
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}