using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Text notificationHeader;
    public Text notificationText;
    
    public float notificationTimer = 4.0f;
    public float fadeInTimer = 0.25f;
    public float fadeOutTimer = 1.0f;

    public void NotificationPopUp(string header, string text)
    {
        // Display text for notification
        notificationHeader.text = header;
        notificationText.text = text;

        // Fade in notification
        FadeInNotification();

        // Fade out notification
        StartCoroutine(FadeOutNotification(this.gameObject, notificationTimer, fadeOutTimer));
    }

    private void FadeInNotification()
    {       
        // Set notification alphas to 0
        // Note: this also might be a bad way to do this
        this.gameObject.GetComponent<Image>().canvasRenderer.SetAlpha( 0.0f );
        this.gameObject.transform.GetChild(0).GetComponent<Text>().canvasRenderer.SetAlpha( 0.0f );
        this.gameObject.transform.GetChild(1).GetComponent<Text>().canvasRenderer.SetAlpha( 0.0f );
        this.gameObject.transform.GetChild(2).GetComponent<Image>().canvasRenderer.SetAlpha( 0.0f );

        this.gameObject.SetActive(true);

        // Fade in notifcation
        // Note: this also might be a bad way to do this
        this.gameObject.GetComponent<Image>().CrossFadeAlpha(1.0f, fadeInTimer, false);
        this.gameObject.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(1.0f, fadeInTimer, false);
        this.gameObject.transform.GetChild(1).GetComponent<Text>().CrossFadeAlpha(1.0f, fadeInTimer, false);
        this.gameObject.transform.GetChild(2).GetComponent<Image>().CrossFadeAlpha(1.0f, fadeInTimer, false);
    }

    private static IEnumerator FadeOutNotification(GameObject target, float notificationTimer, float fadeTimer)
    {
        // Wait for the duration that the notification stays up
        yield return new WaitForSeconds(notificationTimer);
        
        // Fade out notifcation and all its parts
        // Note: this might be a bad way to do this
        target.GetComponent<Image>().CrossFadeAlpha(0.0f, fadeTimer, false);
        target.transform.GetChild(0).GetComponent<Text>().CrossFadeAlpha(0.0f, fadeTimer, false);
        target.transform.GetChild(1).GetComponent<Text>().CrossFadeAlpha(0.0f, fadeTimer, false);
        target.transform.GetChild(2).GetComponent<Image>().CrossFadeAlpha(0.0f, fadeTimer, false);

        // Disable notifcation after fading complete
        yield return new WaitForSeconds(fadeTimer);
        target.SetActive(false);
    }
}
