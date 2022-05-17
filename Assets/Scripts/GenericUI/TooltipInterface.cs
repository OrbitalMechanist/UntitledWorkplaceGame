using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using System.Text.RegularExpressions;

// This is a ganeral purpose interface for interacting with the tooltip object in scenes. 
// Attach to objects that you wish tooltips to appear under when hovered over.
// Note: requires a tooltip object in the scene to use.
public class TooltipInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /** Controls displaying the tooltip description text, enabled by default. */
    public bool enableDescription = true;

    /** Controls whether the tooltip will be displayed, enabled by default. */
    public bool enableTooltip = true;

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
        // Note that the name of the tooltip locator is hard coded in order to have tooltips function on UI elements generated mid-game
        tooltipLocator = GameObject.Find("TooltipLocator").GetComponent<TooltipLocator>();

        // Get tooltip script from the locator
        tooltipScript = tooltipLocator.GetToolTipScript();

        // Reformat text
        setTooltipHeaderText(headerText);
        setTooltipDescriptionText(descriptionText);
    }

    public void setTooltipHeaderText(string newText) {
        // Regex required for newline characters inserted in the Unity editor
        headerText = Regex.Unescape(newText);
    }

    public string getTooltipHeaderText() {
        return headerText;
    }

    public void setTooltipDescriptionText(string newText) {
        // Regex required for newline characters inserted in the Unity editor
        descriptionText = Regex.Unescape(newText);
    }

    public string getTooltipDescriptionText() {
        return descriptionText;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // If tooltip is disabled, don't proceed to display tooltip
        if (!enableTooltip) {
            return;
        } 

        // Verify tooltip script was found
        verifyTooltip();

        string text;

        if (enableDescription) {
            // Combine header and description text
            text = headerText + "\n--------------------\n" + descriptionText;
        } else {
            // Only display header
            text = headerText;
        }
        
        // Show the tooltip with this interface's text when the cursor enters this interface's area
        tooltipScript.ShowTooltip(text);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        // Verify tooltip script was found
        verifyTooltip();
        
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
