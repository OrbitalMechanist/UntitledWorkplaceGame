using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EmployeeAttribute
{
    //DO NOT RENAME THIS VARIABLE, IT IS ACCESSED BY NAME-AS-STRING IN THE GENERATOR
    public const string attributeTitle = "Default Attibute Do Not Use";

    abstract public int capabilityDelta { get; set; }
    abstract public int ethicDelta { get; set; }
    abstract public int personalDelta { get; set; }

    protected Employee owner;

    public EmployeeAttribute()
    {
        Debug.Log("defaulted");
    }

    public EmployeeAttribute(Employee empToAttachTo)
    {
        owner = empToAttachTo;
        owner.capability += capabilityDelta;
        owner.ethic += ethicDelta;
        owner.personal += personalDelta;
        Debug.Log("got here");
    }

    public virtual void Affect()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
