using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictorySceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToNextScene", 10f); 
    }

    private void GoToNextScene()
    {
        SceneManager.LoadScene("Credits");
    }
}
