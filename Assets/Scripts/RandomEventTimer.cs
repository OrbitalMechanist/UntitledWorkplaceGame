using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RandomEventTimer : MonoBehaviour
{
    int count;
    int randEvent;
    UnityAction buttonCallBack;
    
    public float time;
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
        if (!Event.activeSelf) {
            if (time<=0.0f) {
                count++;
                time = Random.Range(5.0f, 10.0f);
                Event.SetActive(true);
                randomizeEvents();
            } else {
                time-=Time.deltaTime;
            }
        }
    }

    public void randomizeEvents() {
        Button ChoiceA = GameObject.Find("Choice A").GetComponent<Button>();
        Button ChoiceB = GameObject.Find("Choice B").GetComponent<Button>();
        Text desc = GameObject.Find("Desc").GetComponent<Text>();
        ChoiceA.onClick.RemoveAllListeners();
        ChoiceB.onClick.RemoveAllListeners();
        randEvent = Random.Range(1, 4);
        if (randEvent==1) {
            desc.text = "Both buttons will raise money by $100";
            ChoiceA.onClick.AddListener(delegate{RaiseMoney();});
            ChoiceB.onClick.AddListener(delegate{RaiseMoney();});
        } else if (randEvent==2) {
            desc.text = "Both buttons will lower money by $100";
            ChoiceA.onClick.AddListener(delegate{LowerMoney();});
            ChoiceB.onClick.AddListener(delegate{LowerMoney();});
        } else if (randEvent==3) {
            desc.text = "Button A will raise money by $100, Button B will lower money by $100";
            ChoiceA.onClick.AddListener(delegate{RaiseMoney();});
            ChoiceB.onClick.AddListener(delegate{LowerMoney();});            
        } else if (randEvent==4) {
            desc.text = "Button A will lower money by $100, Button B will raise money by $100";
            ChoiceA.onClick.AddListener(delegate{LowerMoney();});
            ChoiceB.onClick.AddListener(delegate{RaiseMoney();});              
        }
    }

    public void RaiseMoney()
    {
        company.GetComponent<Company>().cash+=100;
        Event.SetActive(false);
    }

    public void LowerMoney()
    {
        company.GetComponent<Company>().cash-=100;
        Event.SetActive(false);
    }
}
