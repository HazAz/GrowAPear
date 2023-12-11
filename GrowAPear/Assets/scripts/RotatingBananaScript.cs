using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBananaScript : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float speed = 120f;

	// Update is called once per frame
	void Update()
	{
		transform.position = target.position;
		transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
	}
}
