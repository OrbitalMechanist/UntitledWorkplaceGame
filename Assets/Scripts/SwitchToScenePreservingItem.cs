using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToScenePreservingItem : MonoBehaviour
{
    public GameObject itemToPreserve;

    public string sceneName;

    public void SwitchScenes()
    {
        SceneManager.LoadScene(sceneName);
        DontDestroyOnLoad(itemToPreserve);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
