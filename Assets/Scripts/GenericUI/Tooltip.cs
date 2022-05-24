using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is the general use tooltip class meant to control the behaviour of the tooltip itself.
// To add tooltips to more scenes, make a copy of the Tooltip prefab and place it anywhere in the scene.
// Note: only one tooltip object is required per scene
public class Tooltip : MonoBehaviour
{
    /** Horizontal offset of the tooltip relative to the mouse position. */
    private const float HORIZONTAL_OFFSET = 15f;

    /** Vertical offset of the tooltip relative to the mouse position. */
    private const float VERTICAL_OFFSET = 45f;

    /** Camera of the UI. */
    public Camera uiCamera;

    /** Min and max boundaries of the camera. */
    private Vector3 min;
    private Vector3 max;

    /** Text object of the tooltip. */
    private Text tooltipText;

    /** Rect of the tooltip. */
    private RectTransform rect;

    /** The default width that the game was initially developed with. */
    private const int DEFAULT_WIDTH = 800;

    /** The default height that the game was initially developed with. */
    private const int DEFAULT_HEIGHT = 600;

    /** The width of the tooltip, if fixed size is enabled. */
    private const int TOOLTIP_WIDTH = 350;

    /** The scale factor for the width of the tooltip. Needed to fix scaling issues on other resolutions due to manual position calculation. */
    private float scaleWidth;

    /** The scale factor for the height of the tooltip. Needed to fix scaling issues on other resolutions due to manual position calculation. */
    private float scaleHeight;

    private void Awake() {
        // Get rect and text of tooltip
        rect =  this.gameObject.GetComponent<RectTransform>();
        tooltipText =  this.gameObject.GetComponentInChildren<Text>();

        // Calculate the scaling required for the width and height values of the tooltip based on the current resolution
        // i.e. 1600 x 1200 screen = 2x scaling required
        scaleWidth = (float)uiCamera.pixelWidth / (float)DEFAULT_WIDTH;
        scaleHeight = (float)uiCamera.pixelHeight / (float)DEFAULT_HEIGHT;

        // Set min and max boundaries
        min = new Vector3(0, 0, 0);
        max = new Vector3(uiCamera.pixelWidth, uiCamera.pixelHeight, 0); 
    }

    private void Update() {
        // Calcualte the tooltip's scaled width and height
        int scaledTooltipWidth = (int)(scaleWidth * rect.rect.width);
        int scaledTooltipHeight = (int)(scaleHeight * rect.rect.height);

        // If the tooltip would go off the bottom of the screen, make it to appear above the mouse instead of under
        float tooltipY;
        if (Input.mousePosition.y - (VERTICAL_OFFSET + scaledTooltipHeight) < min.y) {
            tooltipY = Input.mousePosition.y + ((VERTICAL_OFFSET * scaleHeight) + (scaledTooltipHeight / 2));
        } else {
            tooltipY = Input.mousePosition.y - ((VERTICAL_OFFSET * scaleHeight) + (scaledTooltipHeight / 2)); 
        }

        Vector3 position  = new Vector3(Input.mousePosition.x + HORIZONTAL_OFFSET + (scaledTooltipWidth / 2), tooltipY, 0f);
        
        // Clamp it to the screen size so it doesn't go outside
        this.gameObject.transform.position = new Vector3(Mathf.Clamp(position.x, min.x + (scaledTooltipWidth / 2), max.x - (scaledTooltipWidth / 2)), 
                                                         Mathf.Clamp(position.y, min.y + (scaledTooltipHeight / 2), max.y - (scaledTooltipHeight / 2)), 
                                                         transform.position.z);
    }

    public void ShowTooltip(string tooltipString) {
        // Show tooltip
        this.gameObject.SetActive(true);

        // Set tooltip text with unescaped characters
        tooltipText.text = tooltipString;
    }

    public void HideTooltip() {
        // Hide tooltip
        this.gameObject.SetActive(false);

        // Temporarily move tooltip position out of view to avoid flashing issues
        this.gameObject.transform.position = new Vector3(-1000, -1000, -1000);
    }

    public void disableFixedSize() {
        this.gameObject.GetComponentInChildren<LayoutElement>().preferredWidth = 0;
    }

    public void enableFixedSize() {
        this.gameObject.GetComponentInChildren<LayoutElement>().preferredWidth = TOOLTIP_WIDTH;
    }
}
