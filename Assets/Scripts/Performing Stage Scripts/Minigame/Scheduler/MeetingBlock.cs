using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  

public class MeetingBlock : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int tileWidth;
    public int tileHeight;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private const int FILLED_TILE_NUM = 1;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public int[,] getTileLayout() {
        int[,] tileLayout = new int[tileWidth, tileHeight];

        for (int x = 0; x < tileLayout.GetLength(0); x++)
        {
            for (int y = 0; y < tileLayout.GetLength(1); y++)
            {
                tileLayout[x, y] = FILLED_TILE_NUM;
            }
        }
            
        return tileLayout; 
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.ignoreParentGroups = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void PlaceMeetingBlock(int tileX, int tileY) {
        
    }
}
