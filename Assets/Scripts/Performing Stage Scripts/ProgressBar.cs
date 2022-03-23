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
    int currentTicks = 0;
    private const int maxTicks = 100;
    private const int progressLength = 285;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTicks++;
        progressText.text = "Progress: " + currentTicks / maxTicks + "%";
        setProgress(progressLength - (currentTicks / maxTicks));
    }

    private void setProgress(float right) {
        currentProgress.offsetMax = new Vector2(-right, currentProgress.offsetMax.y);
    }
}
