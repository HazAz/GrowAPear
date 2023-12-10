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
	[SerializeField] private Image leftBuffImage;
	[SerializeField] private TextMeshProUGUI leftBuffDesc;

	[SerializeField] private TextMeshProUGUI rightBuffName;
    [SerializeField] private Image rightBuffImage;
	[SerializeField] private TextMeshProUGUI rightBuffDesc;

	[SerializeField] private Sprite bananaSprite;
    [SerializeField] private Sprite cherrySprite;

    private PowerupScripts powerupScripts;
    private Action OnComplete;

    public void Init(PowerupTypes left, PowerupTypes right, PowerupScripts ps, Action onComplete = null)
    {
        leftPowerupType = left;
        rightPowerupType = right;

        SetupPowerup(leftPowerupType, leftBuffName, leftBuffDesc, leftBuffImage);
        SetupPowerup(rightPowerupType, rightBuffName, rightBuffDesc, rightBuffImage);

        powerupScripts = ps;

		OnComplete = onComplete;

        gameObject.SetActive(true);
	}

    private void SetupPowerup(PowerupTypes type, TextMeshProUGUI title, TextMeshProUGUI desc, Image image)
    {
        switch (type)
        {
            case PowerupTypes.Banana:
                title.text = "BANANAAAA";
                image.sprite = bananaSprite;
                desc.text = "IT DOES SOMETHING!!!";
                break;

			case PowerupTypes.Cherry:
				title.text = "CHERRYYYYYYY";
				image.sprite = cherrySprite;
				desc.text = "IT DOES SOMETHING ELSE!!!";
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
