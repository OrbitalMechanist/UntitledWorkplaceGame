using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareTwitter : MonoBehaviour
{
    public Text result;

    private const string GAME_LINK = "https://www.game.cultureindex.io/";
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";

    public void ShareToTW()
    {
        // Create Tweet text message
        // Note: this is limited to 280 chars or less as per Twitter's character limit
        string tweetMessage = "I got " + result.text + " on the Untitled Workplace Culture Game!"; 
        
        // Open twitter with message link
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(tweetMessage + "\n\n" + "Try the game yourself here:\n" + GAME_LINK));
    }
}
