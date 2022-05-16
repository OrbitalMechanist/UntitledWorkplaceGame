using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DisplayMinigameResults : MonoBehaviour
{
    public Text resultHeader;
    public Text blurb;
    public Text rewards;

    public string resultHeaderText;
    public string blurbText;
    public string rewardText;

    public void DisplayResults() {
        resultHeader.text = resultHeaderText;
        blurb.text = blurbText;
        rewards.text = Regex.Unescape(rewardText);
    }
}
