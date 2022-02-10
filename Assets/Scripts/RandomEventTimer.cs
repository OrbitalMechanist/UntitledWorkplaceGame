using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventTimer : MonoBehaviour
{
    int count;
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
            } else {
                time-=Time.deltaTime;
            }
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
