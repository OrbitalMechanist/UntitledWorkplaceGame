using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {
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
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, -350);
        int buttonCount = Random.Range(0, 3);
        for (int i = 0; i <= buttonCount; i++) {
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            Button newButton = Instantiate(button);
            newButton.transform.SetParent(newEvent.transform, false);
            newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            newButton.onClick.AddListener(delegate{RaiseMoney(100);closeEvent(newEvent);});
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

    public void LowerMoney(int delta)
    {
        company.GetComponent<Company>().cash-=delta;
    }
}
