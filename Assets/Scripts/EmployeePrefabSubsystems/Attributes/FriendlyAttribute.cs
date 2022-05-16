using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Friendly";

    public override int ethicDelta { get; set; } = 0;
    public override int personalDelta { get; set; } = +20;
    public override int capabilityDelta { get; set; } = 0;
    public override string tooltipHeaderText { get; set; } = "<color=green>Friendly</color>\nPersonal Skill: <color=green>+20</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee has a friendly attitude";

    public FriendlyAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
