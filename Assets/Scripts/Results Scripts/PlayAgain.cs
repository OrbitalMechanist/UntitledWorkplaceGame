using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void backToTitleScreen()
    {
        // Destroy the company and employee owner to get rid of lingering employees
        Destroy(GameObject.Find("Company"));
        Destroy(GameObject.Find("employeeOwner"));

        // Load to title screen
        SceneManager.LoadScene("title"); 
    }
}
