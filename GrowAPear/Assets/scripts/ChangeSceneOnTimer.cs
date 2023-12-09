using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTimer : MonoBehaviour
{

    public float changeTime;
    public string SceneName;
   
    private void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0) 
        {
            SceneManager.LoadScene(SceneName);
        }
        
    }
}
