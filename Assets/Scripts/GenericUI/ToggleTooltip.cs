using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTooltip : MonoBehaviour
{
    public GameObject UIElementWithTooltip;
    
    public void ToggleTooltipOnElement() {
        UIElementWithTooltip.GetComponent<TooltipInterface>().enableTooltip = !UIElementWithTooltip.GetComponent<TooltipInterface>().enableTooltip;
    }
}
