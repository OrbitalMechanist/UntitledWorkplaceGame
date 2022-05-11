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
    abstract public string tooltipHeaderText { get; set; }
    abstract public string tooltipDescriptionText { get; set; }

    protected Employee owner;

    public EmployeeAttribute(Employee empToAttachTo)
    {
        owner = empToAttachTo;
        owner.ChangeCapability(capabilityDelta);
        owner.ChangeWorkEthic(ethicDelta);
        owner.ChangePersonal(personalDelta);
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
