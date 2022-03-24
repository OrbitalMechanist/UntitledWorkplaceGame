using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareEmail : MonoBehaviour
{
    public Text result;

    private const string GAME_LINK = "https://www.game.cultureindex.io/";

    public void ShareByEmail()
    {
        // Email header
        string email = "";
        string subject = CreateEscapeURL("Untitled Workplace Culture Game!");

        // Create excape URL with email message
        string body = CreateEscapeURL("I got " + result.text + " on the Untitled Workplace Culture Game!\n\nTry the game yourself here:\n" + GAME_LINK);

        // Open email applcation
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string CreateEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+","%20");
    }
}
