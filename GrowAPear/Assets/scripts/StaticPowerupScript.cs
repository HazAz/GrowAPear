using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticPowerupScript
{
	public static List<PowerupTypes> AvailablePowerups = new();
	public static List<PowerupTypes> AcquiredPowerups = new();
	public static List<PowerupTypes> TempPowerups = new();

	public static void NewGameStart()
	{
		AvailablePowerups.Clear();
		AcquiredPowerups.Clear();
		TempPowerups.Clear();

		AvailablePowerups.Add(PowerupTypes.Banana);
		AvailablePowerups.Add(PowerupTypes.Cucumber);
		AvailablePowerups.Add(PowerupTypes.BokChoy);
		AvailablePowerups.Add(PowerupTypes.Plum);
		AvailablePowerups.Add(PowerupTypes.Chili);
		AvailablePowerups.Add(PowerupTypes.Peppers);
		AvailablePowerups.Add(PowerupTypes.Grape);
		AvailablePowerups.Add(PowerupTypes.Apple);
		AvailablePowerups.Add(PowerupTypes.Carrot);
		AvailablePowerups.Add(PowerupTypes.Berry);
		AvailablePowerups.Add(PowerupTypes.Orange);
		AvailablePowerups.Add(PowerupTypes.Kiwi);
		AvailablePowerups.Add(PowerupTypes.Broccoli);
	}

	public static void EnteredNewLevel()
	{
		foreach (var powerup in TempPowerups)
		{
			AcquiredPowerups.Add(powerup);
			AvailablePowerups.Remove(powerup);
		}
	}

	public static void OnDeath()
	{
		foreach(var powerup in TempPowerups)
		{
			AvailablePowerups.Add(powerup);
		}

		TempPowerups.Clear();
	}

	public static void AddPowerupInLevel(PowerupTypes powerup)
	{
		TempPowerups.Add(powerup);
		AvailablePowerups.Remove(powerup);
	}

	public static void AddAllPowerups()
	{
		NewGameStart();

		foreach (var powerup in AvailablePowerups)
		{
			AddPowerupInLevel(powerup);
		}
	}
}
