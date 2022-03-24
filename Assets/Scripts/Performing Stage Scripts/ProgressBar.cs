using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    Text progressText;
    [SerializeField]
    RectTransform currentProgress;
    GameObject eventOrganizer;
    RandomEventTimer eventScript;
    int currentTicks;
    private const int maxTicks = 20;
    private float progressLength;

    // Start is called before the first frame update
    void Start()
    {
        progressLength = currentProgress.rect.width;
        eventOrganizer = GameObject.Find("Organizer");
        eventScript = eventOrganizer.GetComponent<RandomEventTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTicks = eventScript.count;

        double percentProgress = (double)currentTicks / (double)maxTicks;
        progressText.text = "Progress: " + (int)(percentProgress * 100) + "%";
        setProgress((float)(progressLength - (progressLength * percentProgress)));
    }

    private void setProgress(float right) {
        currentProgress.offsetMax = new Vector2(-right, currentProgress.offsetMax.y);
    }
}
