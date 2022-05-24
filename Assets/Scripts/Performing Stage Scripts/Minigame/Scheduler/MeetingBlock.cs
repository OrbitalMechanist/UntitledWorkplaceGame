using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  

public class MeetingBlock : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /** The tile width of the meeting block. */
    public int tileWidth;

    /** The tile height of the meeting block. */
    public int tileHeight;

    /** Camera of the UI. */
    public Camera uiCamera;

    /** The rect transform of the meeting block. */
    private RectTransform rectTransform;

    /** The canvas group of the meeting block. */
    private CanvasGroup canvasGroup;

    /** Whether or not this meeting block is being dragged. */
    private bool isDragging = false;

    /** Whether or not this meeting block is currently placed on the grid. */
    private bool placedOnGrid = false;

    /** The scale factor for the width of the meeting block. Needed to fix scaling issues on other resolutions due to manual position calculation. */
    private float scaleWidth;

    /** The scale factor for the height of the meeting block. Needed to fix scaling issues on other resolutions due to manual position calculation. */
    private float scaleHeight;

    /** The list of colliders this object has collided with. Note: should be implemnted so that only grid tiles are added to this list. */
    private List<Collider2D> collisionList = new List<Collider2D>();

    /** Min and max boundaries of the camera. */
    private Vector3 min;
    private Vector3 max;

    /** The initial position of this meeting block. */
    private Vector3 initialPosition;

    /** Whether or not the initial position of this meeting block has been obtained. */
    private bool obtainedInitialPos = false;

    /** Whether or not the meeting block fits on the currently available space. */
    private bool fitsOnOpenSpace = false;

    /** Whether or not this meeting block is currently colliding with a tile. */
    private bool isColliding = false;

    /** The default width that the game was initially developed with. */
    private const int DEFAULT_WIDTH = 800;

    /** The default height that the game was initially developed with. */
    private const int DEFAULT_HEIGHT = 600;

    /** The number to represent the tiles of the block in the 2D array form. */
    private const int FILLED_TILE_NUM = 1;

    private void Start() {
        // Get meeting tile components
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Calculate the scaling required for the current resolution
        // i.e. 1600 x 1200 screen = 2x scaling required
        scaleWidth = (float)uiCamera.pixelWidth / (float)DEFAULT_WIDTH;
        scaleHeight = (float)uiCamera.pixelHeight / (float)DEFAULT_HEIGHT;

        // Set min and max boundaries of the screen
        min = new Vector3(0, 0, 0);
        max = new Vector3(uiCamera.pixelWidth, uiCamera.pixelHeight, 0); 
    }

    private void Update() {
        // Obtaine the initial position of this meeting tile if it hasn't already
        if (!obtainedInitialPos) {
            initialPosition = rectTransform.anchoredPosition;
            obtainedInitialPos = true;
        }

        // Check if the tiles currently colliding with the meeting block are enough to fit the meeting block
        if (collisionList.Count != tileWidth * tileHeight) {
            fitsOnOpenSpace = false;
        } else {
            fitsOnOpenSpace = true;
        }

        // Snap the meeting block back to its initial position of it did not get successfully placed
        if (!isDragging && !placedOnGrid && !fitsOnOpenSpace && !isColliding) {
            rectTransform.anchoredPosition = initialPosition; 
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
        // Update status bools
        isDragging = true;
        placedOnGrid = false;
        fitsOnOpenSpace = false;

        // Unfill the tiles this block was occupying when it is moved, if any
        if (collisionList.Count > 0) {
            foreach (Collider2D collider in collisionList) {
                collider.gameObject.GetComponent<Tile>().unFillTile();
            }
        }
        // reset the collision list
        collisionList = new List<Collider2D>();
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x + (eventData.delta.x / scaleWidth),
                                                     rectTransform.anchoredPosition.y + (eventData.delta.y / scaleHeight), 0f);                            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void OnTriggerStay2D(Collider2D collider) {
        // If this meeting block is not being dragged and is not already placed on the grid, add the collided tile to the collision list
        if (!isDragging && !placedOnGrid) {
            // If the tile is colliding and has not already been added to the list, add the collider to the list
            if (isColliding && !collisionList.Contains(collider)) {
                collisionList.Add(collider);
            }  
        }

        // If this meeting block is not being dragged, is not already placed on the grid and has collided with exactly the number of tiles needed to 
        // fit it, place it onto the grid
        if (!isDragging && !placedOnGrid && collisionList != null && collisionList.Count == tileWidth * tileHeight) {
            placedOnGrid = true;
            placeTile();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider) {
        // If the object this meeting block has collided with is a tile and is not filled, consider it to be collided with
        if (collider.gameObject.GetComponent<Tile>() != null && !collider.gameObject.GetComponent<Tile>().getFillStatus()) {
            isColliding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collider) {
        isColliding = false;

        // Remove collider from the collider list on exit
        collisionList.Remove(collider);
    }

    public void placeTile() {
        Vector3 averagedPosition = new Vector3();

        // Get the center of all the collided tiles by averaging the positions of them
        foreach (Collider2D collider in collisionList) {
            collider.gameObject.GetComponent<Tile>().fillTile();
            averagedPosition += collider.gameObject.GetComponent<RectTransform>().position;
        }
        averagedPosition /= collisionList.Count;

        // Snap the position of this meeting block to the averaged position; this should fit neatly onto the grid
        rectTransform.position = averagedPosition;
    }
}
