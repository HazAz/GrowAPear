using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 20;
	private int currentHealth;
	[SerializeField] private MosquitoScript mosquitoScript;
	[SerializeField] private SkeeterBossScript skeeterBossScript;

	void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth < 0)
		{
			Die();
		}
	}

	private void Die()
	{
		mosquitoScript?.Die();
		skeeterBossScript?.Die();
	}
}
