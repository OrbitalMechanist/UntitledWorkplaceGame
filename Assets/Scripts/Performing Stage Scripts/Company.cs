using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Company : MonoBehaviour
{
    public int cash = 10000;
    public int count;
    public int endState;
    public int happiness = 100;
    public int companySize;
    public string [] investors;
    public int [] investorDebts;
    public int [] investorPayBack;
    // Start is called before the first frame update
    void Start()
    {
        //Does this count as a singleton?
        foreach(var i in GameObject.FindGameObjectsWithTag(this.gameObject.tag))
        {
            if(i != this.gameObject)
            {
                Debug.Log("destroying new company"); //this will be a useful warning and rare enough not to cause performance issues
                Destroy(gameObject); //we are destroying the *new* Company object being created.
            }
        }
        companySize = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
