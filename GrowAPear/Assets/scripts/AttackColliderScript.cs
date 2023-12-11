using UnityEngine;

public class AttackColliderScript : MonoBehaviour
{
	private int damage = 10;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			other.GetComponent<EnemyHealth>()?.TakeDamage(damage);
		}
	}

	public void IncreaseAttack()
	{
		damage = 15;
	}
	public void SetGrapeCollider()
	{
		damage = 5;
	}
}
