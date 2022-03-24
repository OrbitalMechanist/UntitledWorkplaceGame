using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Text progressText;
    public RectTransform currentProgress;
    private GameObject eventOrganizer;
    private RandomEventTimer eventScript;

    private int currentTicks;
    private float progressLength;

    private const int MAX_TICKS = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get length of the progress bar UI element
        progressLength = currentProgress.rect.width;

        // Get event timer script so event ticks can be accessed
        eventOrganizer = GameObject.Find("Organizer");
        eventScript = eventOrganizer.GetComponent<RandomEventTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update progress if another tick has passed
        if (currentTicks != eventScript.count) {
            // Get new tick count
            currentTicks = eventScript.count;

            // Update progress on UI
            double percentProgress = (double)currentTicks / (double)MAX_TICKS;
            progressText.text = "Progress: " + (int)(percentProgress * 100) + "%";
            setProgress((float)(progressLength - (progressLength * percentProgress)));
        }
    }

    private void setProgress(float right) {
        // Note: very specific use case
        // Set the right offset of the current progress bar in order to move it
        currentProgress.offsetMax = new Vector2(-right, currentProgress.offsetMax.y);
    }
}
