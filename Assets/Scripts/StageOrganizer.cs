using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOrganizer : MonoBehaviour
{
    public GameObject company;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("employeeOwner").transform.SetParent(company.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
