using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareTest : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    private string appStoreLink = "http://www.PLACEHOLDER.ca"; //UPDATE WHEN HOST LINK IS AVAILABLE

    [SerializeField]
    public Text result;

    public void ShareToTW()
    {
        string tweetMessage = "I got " + result.text + " on the Untitled Workplace Culture Game!"; //this is limited to 280 chars or less
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(tweetMessage + "\n\n" + "Try the game yourself here:\n" + appStoreLink));
    }
}
