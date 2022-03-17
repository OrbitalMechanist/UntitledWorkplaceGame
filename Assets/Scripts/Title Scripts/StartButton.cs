using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private const int titleSceneIndex = 2;

    public void ToNextScene() {
        SceneManager.LoadScene(titleSceneIndex); 
    }
}
