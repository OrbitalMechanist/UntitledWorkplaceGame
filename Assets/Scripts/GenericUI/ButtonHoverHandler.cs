using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  

public class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D changeCursorTexture;

    public void OnPointerEnter(PointerEventData eventData)
    {
       Cursor.SetCursor(changeCursorTexture, Vector2.zero, CursorMode.Auto);
    }
       
    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
