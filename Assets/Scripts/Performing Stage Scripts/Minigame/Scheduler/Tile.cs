using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class Tile : MonoBehaviour
{
    /** The X position of this tile. */
    public int x;

    /** The Y position of this tile. */
    public int y;

    /** Whether or not this tile has been filled with a meeting block. */
    private bool isFilled = false;

    /** Whether or not this tile is enabled. */
    private bool tileIsEnabled = true;

    /** The colour of this tile when it is enabled. */
    private Color enabledColour = new Color(242, 182, 4);

    /** The colour of this tile when it is disabled. */
    private Color disabledColour = Color.grey;

    public void enableTile() {
        tileIsEnabled = true;
        this.gameObject.GetComponent<Image>().color = enabledColour;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void disableTile() {
        tileIsEnabled = false;
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
        return tileIsEnabled;
    }

    public bool getFillStatus() {
        return isFilled;
    }
}
