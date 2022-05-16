using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaholicAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Workaholic";

    public override int ethicDelta { get; set; } = 50;
    public override int personalDelta { get; set; } = -25;
    public override int capabilityDelta { get; set; } = 0;

    public WorkaholicAttribute(Employee empToAttachTo) : base(empToAttachTo) {
    }

    public override void Affect()
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
