using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TooltipInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /** The header text to display, appears above the --------- line in the tooltip. */
    [SerializeField]
    private string headerText;

    /** The description text to display appears below the --------- line in the tooltip. */
    [SerializeField]
    private string descriptionText;

    /** Object that contains a variable with the tooltip object. */
    private TooltipLocator tooltipLocator;

    /** The Tooltip script. */
    private Tooltip tooltipScript;

    private void Start()
    {
        // Get the tooltip locator
        tooltipLocator = GameObject.Find("TooltipLocator").GetComponent<TooltipLocator>();

        // Get tooltip script from the locator
        tooltipScript = tooltipLocator.GetToolTipScript();

        // Break tooltip text into lines
        setTooltipHeaderText(headerText);
        setTooltipDescriptionText(descriptionText);
    }

    public void setTooltipHeaderText(string newText) {
        headerText = Regex.Unescape(newText);
    }

    public string getTooltipHeaderText() {
        return headerText;
    }

    public void setTooltipDescriptionText(string newText) {
        descriptionText = Regex.Unescape(newText);
    }

    public string getTooltipDescriptionText() {
        return descriptionText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Verify tooltip script was found
        verifyTooltip();

        // Combine header and description text
        string text = headerText + "\n--------------------\n" + descriptionText;
        
        // Show the tooltip with this interface's text when the cursor enters this interface's area
        tooltipScript.ShowTooltip(text);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip when the mouse cursor leaves this interface area
        tooltipScript.HideTooltip();
    }

    private void verifyTooltip() {
        // Check if tooltip script was successfully assigned, if null grab again
        if (tooltipScript == null) {
            tooltipScript = tooltipLocator.GetToolTipScript();
        }
    }
}
