using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareEncourage : MonoBehaviour
{   
    /** The text to display the share encouragement. */
    public Text text;

    /** The TextAsset file containing the share encouragement strings, separated by newlines. */
    public TextAsset shareEncouragementFile;

    void Start()
    {
        // Read text from text file and split it by newlines
        string[] shareEncourageTextList = shareEncouragementFile.text.Split('\n');

        // Get random index and display a random share encouragement string
        int randIndex = Random.Range(0, shareEncourageTextList.Length);
        text.text = shareEncourageTextList[randIndex];
    }
}
