using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class dataToSend 
{
    public string[] content { get; set; }
}

public class ShareLinkedIn : MonoBehaviour
{
    [SerializeField]
    public Text result;
    private const string appStoreLink = "http://www.PLACEHOLDER.ca"; //UPDATE WHEN HOST LINK IS AVAILABLE

    private const string URL = "https://api.linkedin.com/v2/shares";

    public void GenerateRequest()
    {
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        //WWWForm form = new WWWForm();
        //form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post(URL, ""))
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
