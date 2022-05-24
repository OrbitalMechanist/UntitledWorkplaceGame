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
    public List<int> investorDebts;
    public List<int> investorPayBack;
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
    public void payDebts() {
        //Loops through every set of investors that you have
        for (int i = 0; i < investorDebts.Count;) {
            //Deducts your payback value from the company's money as well as from the debt
            investorDebts[i] -= investorPayBack[i];
            cash -= investorPayBack[i];
            if (investorDebts[i]<=0) {
                //Removes investor from list if the debt is paid
                investorDebts.Remove(i);
                investorPayBack.Remove(i);
            } else {
                //This needs to be here instead of up above so we don't skip over any investors should we remove one
                i++;
            }
        }
    }
    public void increaseCompany() {
        companySize += 5;
    }
}
