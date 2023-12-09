using UnityEngine;

public class AttackColliderScript : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyHealth>()?.TakeDamage(10);
		}
	}
}
