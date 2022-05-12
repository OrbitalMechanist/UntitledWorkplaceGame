using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The purpose of this middleman for the tooltip is to allow mid-game generated UI elements (i.e. the employee cards) to use the same tooltip object
// in the scene, preventing the generation of tooltip objects for every UI element.
public class TooltipLocator : MonoBehaviour
{
    /** The Tooltip game object. */
    public GameObject tooltipObject;

    /** The Tooltip script. */
    private Tooltip tooltipScript;

    void Start()
    {
        // Get tooltip script
        tooltipScript = tooltipObject.GetComponent<Tooltip>();
    }

    public Tooltip GetToolTipScript() {
        return tooltipScript;
    }
}
