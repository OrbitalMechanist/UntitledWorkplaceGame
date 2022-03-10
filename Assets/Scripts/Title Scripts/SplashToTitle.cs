 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

public class SplashToTitle : MonoBehaviour
{
    private const int splashLength = 5;
    private const int titleSceneIndex = 1;

    void Start()
    {
        StartCoroutine(loadTitleAfterSplash());
    }

    IEnumerator loadTitleAfterSplash()
    {
        yield return new WaitForSeconds(splashLength);
        SceneManager.LoadScene(titleSceneIndex); 
    }
}
