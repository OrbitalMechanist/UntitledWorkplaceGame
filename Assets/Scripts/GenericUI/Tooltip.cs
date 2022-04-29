using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    /** Horizontal offset of the tooltip relative to the mouse position. */
    private const float offset = 15f;

    /** Camera of the UI. */
    public Camera uiCamera;

    /** The tooltip object. */
    public GameObject tooltip;

    /** Text to be displayed on the tooltip. */
    public string text;

    /** Mina nd max boundaries of the camera. */
    private Vector3 min;
    private Vector3 max;

    /** Text object of the tooltip. */
    private Text tooltipText;

    /** Rect of the tooltip. */
    private RectTransform rect;

    private void Start() {
        // Get rect and text of tooltip
        rect = tooltip.GetComponent<RectTransform>();
        tooltipText = tooltip.GetComponentInChildren<Text>();

        // Set min and max boundaries
        min = new Vector3(0, 0, 0);
        max = new Vector3(uiCamera.pixelWidth, uiCamera.pixelHeight, 0);
    }

    private void Update() {
        // Get the tooltip position with offset
        Vector3 position = new Vector3(Input.mousePosition.x + offset + rect.rect.width / 2, Input.mousePosition.y - rect.rect.height, 0f);

        // Clamp it to the screen size so it doesn't go outside
        tooltip.transform.position = new Vector3(Mathf.Clamp(position.x, min.x + rect.rect.width/2, max.x - rect.rect.width/2), Mathf.Clamp(position.y, min.y + rect.rect.height / 2, max.y - rect.rect.height / 2), transform.position.z);
    }

    private void ShowTooltip(string tooltipString) {
        // Set tooltip text
        tooltipText.text = tooltipString;

        // Show tooltip
        tooltip.SetActive(true);
    }

    private void HideTooltip() {
        // Hide tooltip
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip(text);
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }
}
