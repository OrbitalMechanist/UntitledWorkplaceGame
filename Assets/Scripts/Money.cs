using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text display;
    public GameObject company;
    // Update is called once per frame
    void Update()
    {
        display.text = parseCash(company.GetComponent<Company>().cash);
    }

    string parseCash(int cash) {
        if (cash>=1000) {
            int higher = cash/1000;
            int dif = cash-(1000*higher);
            string stringDif = dif.ToString();
            if (dif<100) {
                if (dif<10) {
                    stringDif = "0" + stringDif;
                }
                stringDif = "0" + stringDif;
            }
            return (parseCash(higher) + "," + stringDif);
        } else {
            return cash.ToString();
        }
    }
}
