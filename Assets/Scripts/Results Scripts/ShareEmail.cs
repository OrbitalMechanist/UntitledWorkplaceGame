using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShareEmail : MonoBehaviour
{
    [SerializeField]
    public Text result;
    private const string appStoreLink = "http://www.PLACEHOLDER.ca"; //UPDATE WHEN HOST LINK IS AVAILABLE

    public void ShareByEmail()
    {
        string email = "";
        string subject = CreateEscapeURL("Untitled Workplace Culture Game!");
        string body = CreateEscapeURL("I got " + result.text + " on the Untitled Workplace Culture Game!\n\nTry the game yourself here:\n" + appStoreLink);

        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    private string CreateEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+","%20");
    }
}
