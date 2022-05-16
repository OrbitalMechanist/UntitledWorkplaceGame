using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrganizer : MonoBehaviour
{
    public GameObject company;
    //workplaces to put the employees in;
    //must have more than or equal to number of incoming employees, will not work otherwise
    public GameObject[] workplacesInstances;
    //a distraction for the employees to go to when on break
    public GameObject distractionInstance;

    //The thing containing the logic for messing with company finances.
    //Used due to existing design.
    public GameObject companyManagerInstance;

    public void AssignTargetsToEmployees()
    {
        GameObject eo = GameObject.Find("employeeOwner");
        eo.transform.SetParent(company.transform, false);
        GameObject ts = GameObject.FindGameObjectWithTag("Nav Tile System");
        //not exactly error handling but it won't crash at least
        for (int i = 0; i < eo.transform.childCount && i < workplacesInstances.Length; i++)
        {
            GameObject emp = eo.transform.GetChild(i).gameObject;
            emp.GetComponent<Employee>().distractionInstance = distractionInstance;
            emp.GetComponent<Employee>().workplaceInstance = workplacesInstances[i];
            emp.GetComponent<Employee>().companyManagerInstance = companyManagerInstance;
            emp.GetComponent<TileNav>().navigableTileSystemInstance = ts;
            emp.transform.position = workplacesInstances[i].transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //this is incredibly useful for testing.
        if(GameObject.Find("employeeOwner") == null)
        {
            new GameObject("employeeOwner");
        }
        
        AssignTargetsToEmployees();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
