using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sidebar : MonoBehaviour
{
    public GameObject Panel;
    public Button panelButton;
    
    public void Start() {
        panelButton.onClick.AddListener(TogglePanel);
    }

    public void TogglePanel()
    {
        Panel.SetActive(!Panel.activeSelf);
    }
}
