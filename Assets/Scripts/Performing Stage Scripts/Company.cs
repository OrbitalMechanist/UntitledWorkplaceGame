using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int cash;
    public int count;
    public int endState;
    public int happiness;
    public string [] investors;
    public int [] investorDebts;
    public int [] investorPayBack;
    // Start is called before the first frame update
    void Start()
    {
        cash = 10000;
        happiness = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
