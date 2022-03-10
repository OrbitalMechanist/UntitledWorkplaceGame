using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class RandomEventTimer : MonoBehaviour
{
    bool hasEvent = false;
    int count;
    int randEvent;
    UnityAction buttonCallBack;
    
    float time;
    public Button button;
    public GameObject canvas;
    public GameObject Event;
    public GameObject company;
    public class EventObject {
        public string[] Events;
        public int[][] EventButtons;
        public string[] ButtonTexts;
        public int [][] ButtonIndices;
        public int [][] ButtonValues;
    }
    public delegate void MethodDelegate (int delta);
    List<MethodDelegate> delList;
    EventObject myEvents;
    string eventJson;
    // Start is called before the first frame update
    void Start()
    {
        eventJson = File.ReadAllText("./Assets/Data/Event Lists/Events.json");
        eventJson = eventJson.Replace("\n", "").Replace("\r", "").Replace("    ", "");
        myEvents = JsonUtility.FromJson<EventObject>(eventJson);
        delList = new List<MethodDelegate> {RaiseMoney, ChangeHappiness, ChangePersonality, ChangeCapability, ChangeEthic, MassChangeHappiness};
        count = 0;
        time = Random.Range(5.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEvent) {
            if (time<=0.0f) {
                count++;
                time = Random.Range(5.0f, 10.0f);
                Event.SetActive(true);
                GameObject test = randomizeEvents();
                test.transform.SetParent(canvas.transform, false);
                hasEvent = true;
            } else {
                time-=Time.deltaTime;
            }
        }
    }

    public GameObject randomizeEvents() {
        GameObject newEvent = Instantiate(Event);
        newEvent.GetComponentInChildren<Text>().text = myEvents.Events[0];
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, -350);
        int buttonCount = Random.Range(0, 3);
        for (int i = 0; i < 4; i++) {
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            Button newButton = Instantiate(button);
            newButton.transform.SetParent(newEvent.transform, false);
            newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            newButton.onClick.AddListener(delegate{delList[0](100);closeEvent(newEvent);});
        }
        return newEvent;
    }
    public void closeEvent(GameObject thisEvent) {
        Destroy(thisEvent);
        hasEvent = false;
    }
    public void RaiseMoney(int delta)
    {
        company.GetComponent<Company>().cash+=delta;
    }
    public void ChangeHappiness(int delta) {

    }
    public void ChangePersonality(int delta) {

    }
    public void ChangeCapability(int delta) {

    }
    public void ChangeEthic(int delta) {

    }
    public void MassChangeHappiness(int delta) {

    }
}
