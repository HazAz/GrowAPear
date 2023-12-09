using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoBulletScript : MonoBehaviour
{
	[SerializeField] private float minSpeed = 4f;
	[SerializeField] private float maxSpeed = 7f;

	private Vector3 targetPosition;
	private float speed = 0f;

	public void Init(Vector3 positionToTarget)
	{
		targetPosition = positionToTarget;
		speed = Random.Range(minSpeed, maxSpeed);
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Enemy"))
		{
			Destroy(gameObject);
		}
	}
}