using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{

    public float changeTime;
   
   
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0) 
        {
            SkipVideo();
        }
        
    }

    public void SkipVideo()
    {
		SceneManager.LoadScene("Level1");
	}
}
