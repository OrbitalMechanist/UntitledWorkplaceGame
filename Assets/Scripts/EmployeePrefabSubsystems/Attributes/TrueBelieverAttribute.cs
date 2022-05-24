using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueBelieverAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "True Believer";

public override int ethicDelta { get; set; } = 50;
public override int personalDelta { get; set; } = 5;
public override int capabilityDelta { get; set; } = 10;
public override string tooltipHeaderText { get; set; } = "<color=green>True Believer</color>\nWork Ethic: <color=green>+50</color>\nPersonal Skill: <color=green>5</color>\nCapability: <color=green>10</color>";
public override string tooltipDescriptionText { get; set; } = "This employee really loves this company and the work that we do (at least without having seen inside) and will work hard to make a large personal contribution.";

public TrueBelieverAttribute(Employee empToAttachTo) : base(empToAttachTo) {
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
