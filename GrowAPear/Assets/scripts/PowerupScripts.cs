using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupTypes
{
    Banana,
    Cherry,

}

public class PowerupScripts : MonoBehaviour
{
    [SerializeField] private PowerupPanelScript powerupPanelScript;
    private List<PowerupTypes> acquiredPowerupTypes = new();
    private List<PowerupTypes> availablePowerupTypes = new();

    private void Awake()
    {
        availablePowerupTypes.Add(PowerupTypes.Banana);
        availablePowerupTypes.Add(PowerupTypes.Cherry);
    }

    public void CreatePowerupPanelScript(Action onComplete = null)
    {
        powerupPanelScript.Init(PowerupTypes.Banana, PowerupTypes.Cherry, this, onComplete);
    }

    
}
