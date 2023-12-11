using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupTypes
{
    Banana, // bananapoleon 1
    Cucumber, // hercucumber 2
    BokChoy, // usain bok 3
    Plum, // Plumario 4
    Chili, // Achilis 5
    Peppers, // Kylian mbappe 6 
    Grape, // Alexander the grape 7
    Apple, // Hippocratapple 8
    Carrot, // Micarrot jordan 9
    Berry, // Alberry Einstein 10
    Orange, // Orangeheimer 11
    Kiwi, // Kiwianu Reeves 12
    Broccoli, // Mohammed Broccoli 13
}

public class PowerupScripts : MonoBehaviour
{
    [SerializeField] private PowerupPanelScript powerupPanelScript;
    [SerializeField] private PlayerPowerup playerPowerup;
    private List<PowerupTypes> acquiredPowerupTypes = new();
    private List<PowerupTypes> availablePowerupTypes = new();

    private void Awake()
    {
        availablePowerupTypes.Add(PowerupTypes.Banana);
        availablePowerupTypes.Add(PowerupTypes.Cucumber);
    }

    public void CreatePowerupPanelScript(Action onComplete = null)
    {
        powerupPanelScript.Init(PowerupTypes.Banana, PowerupTypes.Cucumber, this, onComplete);
    }

    public void ApplyPowerup(PowerupTypes powerupType)
    {
        acquiredPowerupTypes.Add(powerupType);
        availablePowerupTypes.Remove(powerupType);
        playerPowerup.ApplyPowerups(powerupType);
    }
}
