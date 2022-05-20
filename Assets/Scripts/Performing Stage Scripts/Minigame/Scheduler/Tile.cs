using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  

public class Tile : MonoBehaviour, IDropHandler
{
    public bool enabled = true;
    public int x;
    public int y;

    public void OnDrop(PointerEventData eventData) {
        if (enabled) {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<MeetingBlock>().PlaceMeetingBlock(x, y);
        }
    }
}
