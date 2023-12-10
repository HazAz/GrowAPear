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
	private PlayerHealth playerHealth;

	public void Init(PlayerHealth health)
	{
		playerHealth = health;
		targetPosition = (playerHealth.transform.position - transform.position).normalized;
		speed = Random.Range(minSpeed, maxSpeed);
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
			playerHealth.TakeDamage(5);
		}

		Destroy(gameObject);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}