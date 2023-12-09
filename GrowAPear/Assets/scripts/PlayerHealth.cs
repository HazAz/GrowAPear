using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    [SerializeField] private HealthBar healthBar;

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

        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver");

    }
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            healthBar.SetHealth(currentHealth += 15);
        }
    }
}

