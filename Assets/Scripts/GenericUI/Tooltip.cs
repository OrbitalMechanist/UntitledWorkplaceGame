using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    /** Horizontal offset of the tooltip relative to the mouse position. */
    private const float offset = 15f;

    /** Camera of the UI. */
    public Camera uiCamera;

    /** Mina nd max boundaries of the camera. */
    private Vector3 min;
    private Vector3 max;

    /** Text object of the tooltip. */
    private Text tooltipText;

    /** Rect of the tooltip. */
    private RectTransform rect;

    private void Awake() {
        // Get rect and text of tooltip
        rect =  this.gameObject.GetComponent<RectTransform>();
        tooltipText =  this.gameObject.GetComponentInChildren<Text>();

        // Set min and max boundaries
        min = new Vector3(0, 0, 0);
        max = new Vector3(uiCamera.pixelWidth, uiCamera.pixelHeight, 0);
    }

    private void Update() {
        // Get the tooltip position with offset
        Vector3 position = new Vector3(Input.mousePosition.x + offset + rect.rect.width / 2, Input.mousePosition.y - rect.rect.height, 0f);

        // Clamp it to the screen size so it doesn't go outside
        this.gameObject.transform.position = new Vector3(Mathf.Clamp(position.x, min.x + rect.rect.width/2, max.x - rect.rect.width/2), Mathf.Clamp(position.y, min.y + rect.rect.height / 2, max.y - rect.rect.height / 2), transform.position.z);
    }

    public void ShowTooltip(string tooltipString) {
        // Show tooltip
        this.gameObject.SetActive(true);

        // Set tooltip text with unescaped characters
        tooltipText.text = Regex.Unescape(tooltipString);
    }

    public void HideTooltip() {
        // Hide tooltip
        this.gameObject.SetActive(false);

        // Temporarily move tooltip position out of view to avoid flashing issues
        this.gameObject.transform.position = new Vector3(-100, -100, -100);
    }
}
