using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 16;

    //How much money per unit of work an employee gives the company.
    private static int REVENUE = 500;

    public int salary = 1000;

    public int personal;
    public int capability;
    public int ethic;
    public int happiness = 125;

    public string fName;
    public string lName;

    public List<EmployeeAttribute> attributes = new List<EmployeeAttribute>();
    public List<string> attributeNames = new List<string>();

    //A marker to which the employee goes to work.
    public GameObject workplaceInstance;
    //A marker to which the employee goes when on break.
    public GameObject distractionInstance;


    //The thing containing the logic for messing with company finances.
    //Used due to existing design.
    public GameObject companyManagerInstance;

    //used to denote when this employee is on break
    private bool isWorking = true;
    //These are used for keeping track of when the employee
    //should generate revenue for the company or go on break.
    private int framesSinceIncome;
    private int framesSinceBreak;

    // Start is called before the first frame update
    void Start()
    {
        //I ended up doing this in the generation instead.
        //foreach(EmployeeAttribute attr in attributes)
        //{
        //    attributeNames.Add((string)attr.GetType().GetField("attributeTitle").GetRawConstantValue());
        //}
    }

    public bool AddAttribute(EmployeeAttribute attr)
    {
        if (HasAttributeOfType(attr.GetType()))
        {
            return false;
        }
        attributes.Add(attr);
        attributeNames.Add((string)attr.GetType().GetField("attributeTitle").GetRawConstantValue());
        return true;
    }

    public bool HasAttributeOfType(System.Type attrType)
    {
        foreach (var existing in attributes)
        {
            if (existing.GetType() == attrType)
            {
                return false;
            }
        }
        return false;
    }

    public void Create(string first, string last, int p, int c, int e, int sal)
    {
        fName = first;
        lName = last;
        personal = p;
        capability = c;
        ethic = e;
        salary = sal;
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
            if(framesSinceIncome / 100 > MAX_STAT - capability)
            {
                companyManagerInstance.GetComponent<EventFunctions>().RaiseMoney(REVENUE, 0); //emp param doesn't matter here
                framesSinceIncome = 0;
            } else
            {
                framesSinceIncome++;
            }
            if((framesSinceBreak * Random.value - ethic * 10)   > MAX_STAT * 5 + ethic * 15) //this math needs heavy tuning
            {
                StartCoroutine(GetDistracted());
                framesSinceBreak = 0;
            } else
            {
                framesSinceBreak++;
            }
        }
    }

    public void ChangeCapability(int delta)
    {
        capability = Mathf.Clamp(capability + delta, MIN_STAT, MAX_STAT);
    }

    public void ChangeWorkEthic(int delta)
    {
        ethic = Mathf.Clamp(ethic + delta, MIN_STAT, MAX_STAT);
    }

    public void ChangePersonal(int delta)
    {
        personal = Mathf.Clamp(personal + delta, MIN_STAT, MAX_STAT);
    }
}
