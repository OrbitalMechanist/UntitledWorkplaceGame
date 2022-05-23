using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class MeetingBlock : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public int tileWidth;
    public int tileHeight;

    /** Camera of the UI. */
    public Camera uiCamera;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    private bool placedOnGrid = false;

    private float scaleWidth;
    private float scaleHeight;

    private List<Collider2D> collisionList;

    /** Min and max boundaries of the camera. */
    private Vector3 min;
    private Vector3 max;

    private Vector3 initialPosition;

    private bool obtainedInitialPos = false;

    /** The default width that the game was initially developed with. */
    private const int DEFAULT_WIDTH = 800;

    /** The default height that the game was initially developed with. */
    private const int DEFAULT_HEIGHT = 600;

    /** The number to represent the blocks of the tile in the 2D array form. */
    private const int FILLED_TILE_NUM = 1;

    private void Start() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Calculate the scaling required for the current resolution
        // i.e. 1600 x 1200 screen = 2x scaling required
        scaleWidth = (float)uiCamera.pixelWidth / (float)DEFAULT_WIDTH;
        scaleHeight = (float)uiCamera.pixelHeight / (float)DEFAULT_HEIGHT;

        // Set min and max boundaries
        min = new Vector3(0, 0, 0);
        max = new Vector3(uiCamera.pixelWidth, uiCamera.pixelHeight, 0); 
    }

    private void Update() {
        if (!obtainedInitialPos) {
            initialPosition = rectTransform.anchoredPosition;
            obtainedInitialPos = true;
        }
        
        if (!isDragging && !placedOnGrid) {
            //rectTransform.anchoredPosition = initialPosition; 
        }
    }

    public int[,] getTileLayout() {
        // Create a height x width 2D array to represent the tile 
        int[,] tileLayout = new int[tileHeight, tileWidth];

        // Set the values in the tile to all be 1
        for (int y = 0; y < tileLayout.GetLength(0); y++)
        {
            for (int x = 0; x < tileLayout.GetLength(1); x++)
            {
                tileLayout[y, x] = FILLED_TILE_NUM;
            }
        }
            
        return tileLayout; 
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (collisionList != null) {
            foreach (Collider2D collider in collisionList) {
                collider.gameObject.GetComponent<Tile>().unFillTile();
            }
        }
        collisionList = new List<Collider2D>();

        canvasGroup.blocksRaycasts = false;
        isDragging = true;
        placedOnGrid = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + (eventData.delta.x / scaleWidth),
                                                     rectTransform.anchoredPosition.y + (eventData.delta.y / scaleHeight), 0f);                            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.ignoreParentGroups = true;
        isDragging = false;
    }

    public void OnTriggerStay2D(Collider2D collider) {
        if (!isDragging && !placedOnGrid) {
            if (collider.gameObject.GetComponent<Tile>() != null && !collisionList.Contains(collider)) {
                collisionList.Add(collider);
            }
        }

        if (!isDragging && !placedOnGrid && collisionList != null && collisionList.Count == tileWidth * tileHeight) {
            placedOnGrid = true;
            placeTile();
        }
    }

    public void placeTile() {
        Vector3 averagedPosition = new Vector3();
        foreach (Collider2D collider in collisionList) {
            collider.gameObject.GetComponent<Tile>().fillTile();
            averagedPosition += collider.gameObject.GetComponent<RectTransform>().position;
        }
        averagedPosition /= collisionList.Count;
        rectTransform.position = averagedPosition;
    }
}
