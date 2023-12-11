using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
	[SerializeField] private GameObject rotatingBanana;
	[SerializeField] private AttackColliderScript attackCollider;
	[SerializeField] private AttackColliderScript grapeCollider;
	[SerializeField] private PlayerMovement playerMovement;
	[SerializeField] private PlayerHealth playerHealth;
	public void ApplyPowerups(PowerupTypes powerup)
	{
		switch (powerup)
		{
			case PowerupTypes.Banana:
				rotatingBanana.SetActive(true);
				break;

			case PowerupTypes.Cucumber:
				attackCollider.IncreaseAttack();
				break;

			case PowerupTypes.BokChoy:
				playerMovement.IncreaseMovementSpeed();
				break;

			case PowerupTypes.Plum:
				playerMovement.AllowDoubleJump();
				break;

			case PowerupTypes.Chili:
				playerHealth.HasChili = true;
				break;

			case PowerupTypes.Berry:
				playerHealth.HasBerry = true;
				break;

			case PowerupTypes.Grape:
				grapeCollider.gameObject.SetActive(true);
				grapeCollider.SetGrapeCollider();
				break;

			case PowerupTypes.Apple:
				playerHealth.SetHasApple();
				break;

		}
	}
}
