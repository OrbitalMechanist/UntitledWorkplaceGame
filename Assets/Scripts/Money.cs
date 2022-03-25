using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    [SerializeField]
    Text progressText;
    [SerializeField]
    RectTransform currentProgress;
    public Text moneyDisplay;
    public Text happyDisplay;
    public GameObject company;
    private const int multiplyCount = 5;
    private const int progressLength = 285;
    // Update is called once per frame
    void Update()
    {
        moneyDisplay.text = parseCash(company.GetComponent<Company>().cash);
        happyDisplay.text = parseCash(company.GetComponent<Company>().happiness);
        progressText.text = "Progress: " + company.GetComponent<Company>().count * multiplyCount + "%";
        setProgress(progressLength - (company.GetComponent<Company>().count * multiplyCount));
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

    private void setProgress(float right) {
        currentProgress.offsetMax = new Vector2(-right, currentProgress.offsetMax.y);
    }
}
