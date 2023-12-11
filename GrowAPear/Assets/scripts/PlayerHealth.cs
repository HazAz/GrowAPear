using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth = 100;
	private int currentHealth;
	[SerializeField] private HealthBar healthBar;
	[SerializeField] private PlayerSFXScript playerSFXScript;

	private bool hasChili = false;
	private bool hasBerry = false;
	public bool HasChili { get { return hasChili; } set { hasChili = value; } }
	public bool HasBerry { get { return hasBerry; } set { hasBerry = value; } }
	
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
		CancelInvoke();
		playerSFXScript.PlayDeathSFX();
		Invoke("GameOver", 1f);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			playerSFXScript.PlayerSwimmingSFX();
			
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			InvokeRepeating("HealFromApple", 1f, 3f);
		}
	}

	public void SetHasApple()
	{
		InvokeRepeating("HealFromApple", 0f, 10f);
	}

	private void HealFromApple()
	{
		currentHealth = Mathf.Min(maxHealth, currentHealth + 10);

		healthBar.SetHealth(currentHealth);
	}

	private void HealFromWater()
	{
		currentHealth = Mathf.Min(maxHealth, currentHealth + 15);

		healthBar.SetHealth(currentHealth);
	}

	private void GameOver()
	{
		SceneManager.LoadScene("GameOver");
	}
}

