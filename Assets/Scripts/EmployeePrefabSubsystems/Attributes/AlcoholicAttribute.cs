using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholicAttribute : EmployeeAttribute
{

    public new const string attributeTitle = "Alcoholic";

    public override int ethicDelta { get; set; } = -10;
    public override int personalDelta { get; set; } = -25;
    public override int capabilityDelta { get; set; } = -50;
    public override string tooltipHeaderText { get; set; } = "<color=red>Alcoholic</color>\nWork Ethic: <color=red>-10</color>\nPersonal Skill: <color=red>-25</color>";
    public override string tooltipDescriptionText { get; set; } = "We should probably remove this from the game...";

    public AlcoholicAttribute(Employee empToAttachTo) : base(empToAttachTo)
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
