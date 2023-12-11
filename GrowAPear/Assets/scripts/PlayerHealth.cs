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
	[SerializeField] private PlayerMovement playerMovement;

	private bool hasChili = false;
	private bool hasPepper = false;
	private bool hasBerry = false;
	private bool hasOrange = false;
	private bool hasKiwi = false;
	public bool HasChili { get { return hasChili; } set { hasChili = value; } }
	public bool HasPepper { get { return hasPepper; } set { hasPepper = value; } }
	public bool HasBerry { get { return hasBerry; } set { hasBerry = value; } }
	public bool HasOrange { get {  return hasOrange; } set { hasOrange = value; } }
	public bool HasKiwi { get {  return hasKiwi; } set { hasKiwi = value; } }
	
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	public void TakeDamage(int damage)
	{
		if (hasKiwi && Random.value < 0.15f)
		{
			return;
		}

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
		playerMovement.IsDead = true;
		CancelInvoke();
		playerSFXScript.PlayDeathSFX();
		Invoke("GameOver", 1f);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			playerSFXScript.PlayerSwimmingSFX();
			InvokeRepeating("HealFromWater", 1f, 3f);
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Water"))
		{
			CancelInvoke("HealFromWater");
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

