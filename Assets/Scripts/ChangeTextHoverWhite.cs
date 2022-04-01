using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ChangeTextHoverWhite : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;

    void Start (){
        buttonText = GetComponentInChildren<Text>(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = Color.white;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = Color.black;
    }
}
