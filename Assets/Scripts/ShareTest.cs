using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareTest : MonoBehaviour
{
    private const string TWITTER_ADDRESS = "http://twitter.com/intent/tweet";
    private const string TWEET_LANGUAGE = "en";
    public static string descriptionParam;
    private string appStoreLink = "http://www.YOUROWNAPPLINK.com";

    public void ShareToTW()
    {
        string nameParameter = "YOUR AWESOME GAME MESSAGE!";//this is limited in text length 
        Application.OpenURL(TWITTER_ADDRESS + "?text=" + WWW.EscapeURL(nameParameter + "\n" + descriptionParam + "\n" + "Get the Game:\n" + appStoreLink));
    }
}
