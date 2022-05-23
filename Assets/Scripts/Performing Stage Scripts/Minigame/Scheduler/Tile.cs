using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    private bool isFilled = false;
    private bool enabled = true;
    private Color enabledColour = new Color(242, 182, 4);
    private Color disabledColour = Color.grey;

    public void enableTile() {
        enabled = true;
        this.gameObject.GetComponent<Image>().color = enabledColour;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void disableTile() {
        enabled = false;
        isFilled = false;
        this.gameObject.GetComponent<Image>().color = disabledColour;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void fillTile() {
        isFilled = true;
    }

    public void unFillTile() {
        isFilled = false;
    }

    public bool isEnabled() {
        return enabled;
    }

    public bool getFillStatus() {
        return isFilled;
    }
}
