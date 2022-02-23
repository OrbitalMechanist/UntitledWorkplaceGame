 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

public class SplashToTitle : MonoBehaviour
{
    private const int splashLength = 5;
    private const string title = "title";

    void Start()
    {
        StartCoroutine(loadTitleAfterSplash());
    }

    IEnumerator loadTitleAfterSplash()
    {
        yield return new WaitForSeconds(splashLength);
        SceneManager.LoadScene (sceneName:title); 
    }
}
