using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaholicAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Workaholic";

    public override int ethicDelta { get; set; } = 50;
    public override int personalDelta { get; set; } = -25;
    public override int capabilityDelta { get; set; } = 0;
    public override string tooltipHeaderText { get; set; } = "<color=green>Workaholic</color>\nWork Ethic: <color=green>+50</color>\nPersonal Skill: <color=red>-25</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee has an incredible work ethic, but perhaps gets absorbed by their work a bit too much";

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
