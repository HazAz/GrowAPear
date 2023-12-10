using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 100;
	public int currentHealth;
	[SerializeField] private HealthBar healthBar;
	[SerializeField] private PlayerSFXScript playerSFXScript;
	
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;

		if (currentHealth < 0)
		{
			currentHealth = 0;
			Die();
		}
		else
		{
			playerSFXScript.PlayDamageTakenSFX();
		}

		healthBar.SetHealth(currentHealth);
	}

	private void Die()
	{
		playerSFXScript.PlayDeathSFX();
		Invoke("GameOver", 1f);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerSFXScript.PlayerSwimmingSFX();
			healthBar.SetHealth(currentHealth += 15);
		}
	}

	private void GameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
}

