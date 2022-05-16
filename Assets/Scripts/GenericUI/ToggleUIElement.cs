using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUIElement : MonoBehaviour
{
    public GameObject UIElement;

    public void ToggleUIElementVisibility() {
        UIElement.SetActive(!UIElement.activeSelf);
    }
}
