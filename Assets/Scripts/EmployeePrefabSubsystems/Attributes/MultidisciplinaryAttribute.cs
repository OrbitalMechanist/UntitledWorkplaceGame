using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultidisciplinaryAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Broad Skills";
//    public new const string attributeTitle = "Multidisciplinary";

    public override int ethicDelta { get; set; } = 0;
    public override int personalDelta { get; set; } = 0;
    public override int capabilityDelta { get; set; } = 10;
    public override string tooltipHeaderText { get; set; } = "<color=green>Multidisciplinary</color>\nCapability: <color=green>+10</color>";
    public override string tooltipDescriptionText { get; set; } = "This employee has an education in multiple relevant fields.";

    public MultidisciplinaryAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
