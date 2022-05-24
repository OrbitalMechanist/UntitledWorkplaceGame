using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbedAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Absorbed";

    public override int ethicDelta { get; set; } = 50;
    public override int personalDelta { get; set; } = -25;
    public override int capabilityDelta { get; set; } = 0;
    public override string tooltipHeaderText { get; set; } = "<color=green>Absorbed</color>\nWork Ethic: <color=green>+25</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee sometimes gets bursts of inspiration and works extra hard," +
        " but is a nice person about it.";

    public AbsorbedAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
