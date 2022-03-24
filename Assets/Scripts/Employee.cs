using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 16;

    public int personal;
    public int capability;
    public int ethic;
    public int happiness = 125;

    public string fName;
    public string lName;

    //A marker to which the employee goes to work.
    public GameObject workplaceInstance;
    //A marker to which the employee goes when on break.
    public GameObject distractionInstance;

    //used to denote when this employee is on break
    private bool isWorking = true;
    //These are used for keeping track of when the employee
    //should generate revenue for the company or go on break.
    private int framesSinceIncome;
    private int framesSinceBreak;

    // Start is called before the first frame update
    void Start()
    {
        //        personal = (int)(Random.value * (MAX_STAT - MIN_STAT)) + MIN_STAT;
        //        capability = (int)(Random.value * (MAX_STAT - MIN_STAT)) + MIN_STAT;
        //        ethic = (int)(Random.value * (MAX_STAT - MIN_STAT)) + MIN_STAT;
    }

    public void Create(string first, string last, int p, int c, int e)
    {
        fName = first;
        lName = last;
        personal = p;
        capability = c;
        ethic = e;
        Debug.Log("Created Employee: " + first +" "+last +", P: " +personal+" C:"+capability+" E:"+ethic);
    }

    //The employee will move to the break location, sit there for a bit and come back.
    IEnumerator GetDistracted()
    {
        isWorking = false;
        Debug.Log(fName + " " + lName + " is breaking");
        this.GetComponent<TileNav>().MoveTo(distractionInstance.transform.position);
        yield return new WaitUntil(() => !this.GetComponent<TileNav>().moving);
        yield return new WaitForSeconds(3);
        Debug.Log(fName + " " + lName + " is returning");
        this.GetComponent<TileNav>().MoveTo(workplaceInstance.transform.position);
        yield return new WaitUntil(() => !this.GetComponent<TileNav>().moving);
        Debug.Log(fName + " " + lName + " is back");
        isWorking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (distractionInstance != null && isWorking) // if the employee is working, add progress gaining money and check if it's time to go on break
        {
            if(framesSinceIncome > MAX_STAT - capability)
            {
                //Code to add money to company goes here
                framesSinceIncome = 0;
            } else
            {
                framesSinceIncome++;
            }
            if((framesSinceBreak - ethic) * Random.value > MAX_STAT * 10) //this math needs heavy tuning
            {
                StartCoroutine(GetDistracted());
                framesSinceBreak = 0;
            } else
            {
                framesSinceBreak++;
            }
        }
    }
}
