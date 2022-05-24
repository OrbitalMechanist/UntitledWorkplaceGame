using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptableAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Adaptable";

    public override int ethicDelta { get; set; } = 5;
    public override int personalDelta { get; set; } = 0;
    public override int capabilityDelta { get; set; } = 15;
    public override string tooltipHeaderText { get; set; } = "<color=green>Specialist</color>\nCapability: <color=green>+15</color>\nWork Ethic: <color=green>+5</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee could probably learn to do anything we could ever need if they don't know it already.";

    public AdaptableAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
