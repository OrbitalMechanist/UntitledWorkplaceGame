using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public GameObject button;

    public void toggleButtonInteraction() {
        button.GetComponent<Button>().interactable = !button.GetComponent<Button>().interactable;
    }
}
