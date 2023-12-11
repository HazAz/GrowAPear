using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsTester : MonoBehaviour
{
	[SerializeField] private PlayerPowerup powerupScript;
	// Start is called before the first frame update
	void Start()
	{
		Invoke("AddAllAbilities", 1f);
	}

   private void AddAllAbilities()
   {
		var availablePowerups = new List<PowerupTypes>(StaticPowerupScript.AvailablePowerups);
		foreach (var powerup in availablePowerups)
		{
			powerupScript.ApplyPowerups(powerup);
		}
	}
}
