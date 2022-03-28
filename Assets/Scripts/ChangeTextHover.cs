using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ChangeTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;
    private Color highlightColour = new Color(242f/255f, 182f/255f, 4f/255f);

    void Start (){
        buttonText = GetComponentInChildren<Text>(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = highlightColour;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.white;
    }
}
