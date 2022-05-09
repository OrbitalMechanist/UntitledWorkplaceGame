using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidebar : MonoBehaviour
{
    public GameObject Panel;
    public Button panelButton;
    
    public void Start() {
        panelButton.onClick.AddListener(OpenPanel);
    }

    public void OpenPanel()
    {
        Panel.SetActive(true);
        panelButton.onClick.AddListener(ClosePanel);
        panelButton.onClick.RemoveListener(OpenPanel);
    }

    public void ClosePanel()
    {
        Panel.SetActive(false);
        panelButton.onClick.AddListener(OpenPanel);
        panelButton.onClick.RemoveListener(ClosePanel);
    }
}
