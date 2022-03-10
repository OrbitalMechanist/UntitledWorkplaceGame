using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class ShareLinkedIn : MonoBehaviour
{
    [SerializeField]
    public Text result;
    private const string appStoreLink = "http://www.PLACEHOLDER.ca"; //UPDATE WHEN HOST LINK IS AVAILABLE

    private const string clientID = "";
    private const string callbackURL = "";
    private const string clientSecret = "";
    private const string accessTokenRequestURL = "https://www.linkedin.com/oauth/v2/accessToken";
    private const string shareURL = "https://api.linkedin.com/v2/shares";

    public void GenerateRequest()
    {
        StartCoroutine(ProcessRequest());
    }

    private IEnumerator ProcessRequest()
    {
        string oAuthURL = "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id=" + clientID 
                        + "&redirect_uri=" + callbackURL + "&state=foobar&scope=r_liteprofile%20r_emailaddress%20w_member_social";
        string authResponse = "";
        string code = "";
        string accessToken ="";
    
        using (UnityWebRequest www = UnityWebRequest.Get(oAuthURL))
        {
            yield return www.SendWebRequest();

            string[] pages = oAuthURL.Split('/');
            int page = pages.Length - 1;

            switch (www.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + www.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + www.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + www.downloadHandler.text);
                    authResponse = www.downloadHandler.text;
                    code = authResponse; //requires parsing
                    break;
            }
        }

        WWWForm tokenForm = new WWWForm();
        tokenForm.AddField("grant_type", "authorization_code");
        tokenForm.AddField("code", code);
        tokenForm.AddField("redirect_uri", callbackURL);
        tokenForm.AddField("client_id", clientID);
        tokenForm.AddField("client_secret", clientSecret);

        using (UnityWebRequest www = UnityWebRequest.Post(accessTokenRequestURL, tokenForm))
        {
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                accessToken = www.downloadHandler.text;
            }
        }

        WWWForm shareForm = new WWWForm();
        shareForm.AddField("author", "authorization_code");
        shareForm.AddField("code", code);
        shareForm.AddField("redirect_uri", callbackURL);
        shareForm.AddField("client_id", clientID);
        shareForm.AddField("client_secret", clientSecret);

        using (UnityWebRequest www = UnityWebRequest.Post(shareURL, ""))
        {
            //www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
