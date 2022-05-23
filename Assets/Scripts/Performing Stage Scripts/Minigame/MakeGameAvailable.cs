using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeGameAvailable : MonoBehaviour
{
    /** The organizer object, which contains the EventHandler script. */
    public GameObject organizer;

    /** The button to access the minigame. */
    public GameObject gameButton;

    /** The notification icon on the button. */ 
    public GameObject notificationIcon;

    /** The notification pop-up to alert the user that the button has been enabled. */ 
    public GameObject notification;

    /** The notification header text. */ 
    public string notificationHeaderText;

    /** The notification text. */ 
    public string notificationText;

    /** The number of events that have already been processed; used to limit the re-enabling of the button once per event count. */
    private int lastEventCount = 0;

    /** The interval at which the minigame ill be playable again; i.e. every 5 events the game will be playable again. */
    public const int REPLAYABLE_EVENT_INTERVAL = 5;

    void Update()
    {
        // Get the event handler to access the current event count
        EventHandler eventHandler = organizer.GetComponent<EventHandler>();

        // Enable the minigame button if the event count has changed since last checked and the current number of events 
        // is a multiple of the replayable interval
        if (lastEventCount != eventHandler.eventCount && eventHandler.eventCount % REPLAYABLE_EVENT_INTERVAL == 0) {
            // Make notification icon appear
            notificationIcon.SetActive(true);

            // Enable the button itself
            gameButton.GetComponent<Button>().interactable = true;

            // Enable button's tooltip
            gameButton.GetComponent<TooltipInterface>().enableTooltip = true;

            // Display the notifcation that the button is available to the user
            notification.GetComponent<Notification>().NotificationPopUp(notificationHeaderText, notificationText);

            // Update the number of events already processed
            lastEventCount = eventHandler.eventCount;
        }
    }
}
