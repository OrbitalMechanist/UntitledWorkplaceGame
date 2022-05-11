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

    /** The threshold of characters that can be on a line in the tooltip. If a word is added and the characters surpass this limit, 
        the tooltip will make a new line. Note that this is not a hard cap. */
    private const int MAX_LINE_CHAR_LENGTH = 30;

    private void Start()
    {
        // Get the tooltip locator
        tooltipLocator = GameObject.Find("TooltipLocator").GetComponent<TooltipLocator>();

        // Get tooltip script from the locator
        tooltipScript = tooltipLocator.GetToolTipScript();

        // Break tooltip text into lines
        headerText = BreakStringIntoLines(headerText, MAX_LINE_CHAR_LENGTH);
        descriptionText = BreakStringIntoLines(descriptionText, MAX_LINE_CHAR_LENGTH);
    }

    public void setTooltipHeaderText(string newText) {
        // Break header text into lines
        headerText = BreakStringIntoLines(newText, MAX_LINE_CHAR_LENGTH);
    }

    public string getTooltipHeaderText() {
        return headerText;
    }

    public void setTooltipDescriptionText(string newText) {
        // Break description text into lines
        descriptionText = BreakStringIntoLines(descriptionText, MAX_LINE_CHAR_LENGTH);
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

    private string BreakStringIntoLines(string text, int maxLineCharLength) {
        // Split text into words
        string[] words = text.Split(' ');
        string finalString = "";

        // Track characters on the current line
        int charCounter = 0;
        for (int i = 0; i < words.Length; i++) {
            // Add word to the string
            finalString += words[i] + ' ';

            // Add number of chars to the char counter
            charCounter += words[i].Length;

            // Add a new line to the string if the number of chars has exceeded the specified max length
            if (charCounter > maxLineCharLength) {
                finalString += "\n";
                charCounter = 0;
            } 
        }
        
        return finalString;
    }
}
