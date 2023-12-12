using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
	[SerializeField] private PlayerHealth playerHealth;
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			InvokeRepeating("HealPlayer", 1f, 3f);
		}

		if (playerHealth == null)
		{
			playerHealth = other.GetComponent<PlayerHealth>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("Player"))
		{
			CancelInvoke();
		}
	}

	private void HealPlayer()
	{
		playerHealth?.HealFromWater();
	}
}
