using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneyDisplay;
    public Text happyDisplay;
    public GameObject company;

    /** The  game object with the TooltipInterface. */
    public GameObject tooltipObject;

    /** The TooltipInterface script. */
    private TooltipInterface tooltipInterfaceScript;

    void Start() {
        // Get tooltip interface script
        tooltipInterfaceScript = tooltipObject.GetComponent<TooltipInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the difference between the previous cash value and the new cash value
        int difference = company.GetComponent<Company>().cash - int.Parse(moneyDisplay.text, NumberStyles.AllowThousands);

        // Update cash tooltip and money display if there has been a change
        if (difference != 0) {
            updateCashTooltip(difference);
            
            moneyDisplay.text = parseCash(company.GetComponent<Company>().cash);
        }

        // Happiness
        happyDisplay.text = parseCash(company.GetComponent<Company>().happiness);
    }

    void updateCashTooltip(int difference) {
        string colour;
        string income;

        // Check if money was gained or lost, and update tooltip colour and +/- gain indicator
        if (difference < 0) {
            colour = "red";
            income = parseCash(difference);
        } else {
            colour = "green";
            income = "+" + parseCash(difference);
        }

        // Set tooltip text
        tooltipInterfaceScript.setTooltipText("Money: <color=" + colour + ">" + income + "</color> \n --------------------\n The amount of money your company has");
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
