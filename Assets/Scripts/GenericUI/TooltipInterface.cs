using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class TooltipInterface : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /** The text to display. */
    [SerializeField]
    private string text;

    /** Object that contains a variable with the tooltip object. */
    private TooltipLocator tooltipLocator;

    /** The Tooltip script. */
    private Tooltip tooltipScript;

    /** The maximum number of characters that can be on a line in the tooltip. */
    private const int MAX_LINE_CHAR_LENGTH = 40;

    private void Start()
    {
        // Get the tooltip locator
        tooltipLocator = GameObject.Find("TooltipLocator").GetComponent<TooltipLocator>();

        // Get tooltip script from the locator
        tooltipScript = tooltipLocator.GetToolTipScript();

        // Break tooltip text into lines
        text = BreakStringIntoLines(text, MAX_LINE_CHAR_LENGTH);
    }

    public void setTooltipText(string newText) {
        text = BreakStringIntoLines(newText, MAX_LINE_CHAR_LENGTH);
    }

    public string getTooltipText() {
        return text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Verify tooltip script was found
        verifyTooltip();
        
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
        string unescapedText = Regex.Unescape(text);
        string[] stringSplit = unescapedText.Split(' ');
        int charCounter = 0;
        string finalString = "";
 
        for (int i = 0; i < stringSplit.Length; i++) {
            if (stringSplit[i] == "\n") {
                finalString += stringSplit[i];
                charCounter = 0;
            } else {
                finalString += stringSplit[i] + ' ';
            }

            charCounter += stringSplit[i].Length;

            if (charCounter > maxLineCharLength) {
                finalString += "\n";
                charCounter = 0;
            } 
        }
        return finalString;
    }
}
