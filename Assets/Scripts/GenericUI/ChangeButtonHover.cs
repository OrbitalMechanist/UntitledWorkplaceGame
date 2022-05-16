using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class ChangeButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button buttonUI;
    public Image buttonImage;
    private GameObject myEventSystem;
    private Image button;
    private Color highlightColour = new Color(242f/255f, 182f/255f, 4f/255f);

    void Start (){
        myEventSystem = GameObject.Find("EventSystem");

        button = buttonUI.GetComponentInChildren<Image>(); 
        buttonImage = buttonImage.GetComponentInChildren<Image>(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //button.color = Color.white;
        buttonImage.color = highlightColour;
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        //button.color = highlightColour;
        buttonImage.color = Color.white;
        myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}
