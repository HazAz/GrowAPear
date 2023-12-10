using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PowerupPanelScript : MonoBehaviour
{
    private PowerupTypes leftPowerupType;
    private PowerupTypes rightPowerupType;

    [SerializeField] private TextMeshProUGUI leftBuffName;
    [SerializeField] private TextMeshProUGUI rightBuffName;
    [SerializeField] private Image leftBuffImage;
    [SerializeField] private Image rightBuffImage;

    [SerializeField] private Sprite bananaSprite;
    [SerializeField] private Sprite cherrySprite;

    private PowerupScripts powerupScripts;
    private Action OnComplete;

    public void Init(PowerupTypes left, PowerupTypes right, PowerupScripts ps, Action onComplete = null)
    {
        leftPowerupType = left;
        rightPowerupType = right;

        SetupPowerup(leftPowerupType, leftBuffName, leftBuffImage);
        SetupPowerup(rightPowerupType, rightBuffName, rightBuffImage);

        powerupScripts = ps;

		OnComplete = onComplete;

        gameObject.SetActive(true);
	}

    private void SetupPowerup(PowerupTypes type, TextMeshProUGUI name, Image image)
    {
        switch (type)
        {
            case PowerupTypes.Banana:
                name.text = "BANANAAAA";
                image.sprite = bananaSprite;
                break;

			case PowerupTypes.Cherry:
				name.text = "CHERRYYYYYYY";
				image.sprite = cherrySprite;
				break;
		}
    }

    public void ApplyLeftBuff()
    {
        Debug.LogError("Left buff applied");
        OnComplete?.Invoke();
        gameObject.SetActive(false);
    }

    public void ApplyRightBuff()
    {
        Debug.LogError("Right buff apoplied");
        OnComplete?.Invoke();
		gameObject.SetActive(false);
	}
}
