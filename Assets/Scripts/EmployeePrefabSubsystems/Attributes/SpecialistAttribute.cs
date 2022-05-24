using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialistAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Specialist";

    public override int ethicDelta { get; set; } = 0;
    public override int personalDelta { get; set; } = 0;
    public override int capabilityDelta { get; set; } = 35;
    public override string tooltipHeaderText { get; set; } = "<color=green>Specialist</color>\nWork Capability: <color=green>+35</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee has some very deep if oddly specific knowledge in an area we could absolutely use.";

    public SpecialistAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
