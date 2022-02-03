using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEventTimer : MonoBehaviour
{
    public float time;
    public GameObject Event;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(10.0f, 30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Event.activeSelf) {
            if (time<=0.0f) {
                time = Random.Range(5.0f, 10.0f);
                Event.SetActive(true);
            } else {
                time-=Time.deltaTime;
            }
        }
    }
}
