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

    public string fName;
    public string lName;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
