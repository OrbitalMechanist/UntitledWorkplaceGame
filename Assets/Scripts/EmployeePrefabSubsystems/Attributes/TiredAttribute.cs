using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Tired";

    public override int ethicDelta { get; set; } = -40;
    public override int personalDelta { get; set; } = -10;
    public override int capabilityDelta { get; set; } = -10;

    public TiredAttribute(Employee empToAttachTo) : base(empToAttachTo)
    {
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
