using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoBulletScript : MonoBehaviour
{
	[SerializeField] private float minSpeed = 3f;
	[SerializeField] private float maxSpeed = 5f;

	private Vector3 targetPosition;
	private float speed = 0f;

	public void Init(Vector3 positionToTarget)
	{
		targetPosition = (positionToTarget - transform.position).normalized;
		speed = Random.Range(minSpeed, maxSpeed);
		Invoke("Destroy", 3f);
	}

	private void Update()
	{
		transform.position += targetPosition * speed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}