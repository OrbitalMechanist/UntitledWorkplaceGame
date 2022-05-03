using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class TooltipInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /** The text to display. */
    public string text;

    /** The Tooltip game object. */
    public GameObject tooltipObject;

    /** The Tooltip script. */
    private Tooltip tooltipScript;

    private void Start()
    {
        // Get tooltip script
        tooltipScript = tooltipObject.GetComponent<Tooltip>();
    }

    public void SetInterfaceTooltipText(string newText) {
        // Set this interface's text
        text = newText;
    }

    public string GetInterfaceTooltipText() {
        // Return this interface's text
        return text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the tooltip with this interface's text when the cursor enters this interface's area
        tooltipScript.ShowTooltip(text);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip when the mouse cursor leaves this interface area
        tooltipScript.HideTooltip();
    }
}
