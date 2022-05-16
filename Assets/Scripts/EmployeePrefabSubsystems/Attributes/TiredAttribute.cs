using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Tired";

    public override int ethicDelta { get; set; } = -40;
    public override int personalDelta { get; set; } = -10;
    public override int capabilityDelta { get; set; } = -10;
    public override string tooltipHeaderText { get; set; } = "<color=red>Tired</color>\nWork Ethic: <color=red>-40</color>\nPersonal Skill: <color=red>-10</color>\nCapability: <color=red>-10</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee could probably use some more sleep...";

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
