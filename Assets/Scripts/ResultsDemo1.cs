using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsDemo1 : MonoBehaviour
{
    [SerializeField] 
    private Text buttonText;

    [SerializeField] 
    private Text resultHeader;

    [SerializeField] 
    private Text blurb;

    [SerializeField] 
    private Text revenue;

    [SerializeField] 
    private Text happiness;

    public void OnButtonClick()
    {
        if (buttonText.text == "Result 1 Demo") {
            resultHeader.text = "Result 1";
            blurb.text = "You got the best ending!";
            revenue.text = "$1,000,000"; 
            happiness.text = "100";

            buttonText.text = "Result 2 Demo";
        } else if (buttonText.text == "Result 2 Demo") {
            resultHeader.text = "Result 2";
            blurb.text = "You did okay";
            revenue.text = "$500,000"; 
            happiness.text = "75";

            buttonText.text = "Result 1 Demo";
        }
    }
}
