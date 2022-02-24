using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[SerializeField] 
    private Image buttonImage;
    private Color highlightColour = new Color(242f/255f, 182f/255f, 4f/255f);

    void Start (){
        buttonImage = GetComponentInChildren<Image>(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //this.GetComponentInChildren().color = new Color(242, 182, 4);
        buttonImage.color = Color.white;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        //this.GetComponentInChildren().color = Color.white;
        buttonImage.color = highlightColour;
    }
}
