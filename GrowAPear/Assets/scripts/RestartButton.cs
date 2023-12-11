using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartButton : MonoBehaviour
{
    public void OnRestartButton()
    {
        if (StaticPowerupScript.currentSceneName != "")
        {
			SceneManager.LoadScene(StaticPowerupScript.currentSceneName);
		}
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}

