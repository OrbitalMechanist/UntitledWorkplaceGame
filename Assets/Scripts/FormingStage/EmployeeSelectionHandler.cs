using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeSelectionHandler : MonoBehaviour
{
    //WARNING: This script is hardcoded to the extreme and relies on a certain structure.
    //Intended to be used exclusively in the ForminStage scene.

    //The employeeOwner must contain the employees up for selection as its first children.
    public GameObject employeeOwnerInstance;

    //The employeeItemContainer contains nothing but EmployeePanel items,
    //in the order that they are stored in the employeeOwner.
    //The EmployeePanel must have a Toggle as its child with the index 11.
    public GameObject employeeItemContainerInstance;

    //The Company object that contains the Company script to use for money.
    public GameObject companyInstance;

    //A Text UI element that says how many characters were selected.
    //Modified with extra text and colors!!!
    public GameObject statusTextInstance;

    //A Text UI element that says how much money will be left after the selected employees are hired.
    //Modified with more text.
    public GameObject budgetTextInstance;

    //The Button to block while the wrong number of employees is selected.
    public GameObject blockableButtonInstance;

    //How many employees are allowed to be selected.
    public int selectionLimit = 5;

    //This function serves to strip out unselected employees before
    //their container is handed over to SwitchToScenePreservingItem.
    public void cleanEmployeeObjectsBySelection()
    {
        LinkedList<int> selectedIndeces = new LinkedList<int>();
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for(int i = 0; i < numEmployees; i++)
        {
            if(employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().isOn)
            {
                selectedIndeces.AddFirst(i);
            }
        }

        //Doing this in reverse is necessary so that deleting objects doesn't affect the index
        //of the ones we deal with later on
        for (int i = numEmployees - 1; i >= 0; i--)
        {
            if(selectedIndeces.Count == 0 || selectedIndeces.First.Value != i)
            {
                Destroy(employeeOwnerInstance.transform.GetChild(i).gameObject);
            } else
            {
                selectedIndeces.RemoveFirst();
            }
        }

    }

    public void applyHiringCosts()
    {
        companyInstance.GetComponent<Company>().cash -= totalSelectedSalaries();
    }

    public void updateSelectionStatus()
    {
        int selected = countSelectedEmployees();
        statusTextInstance.GetComponent<Text>().text = "Selected: " + selected + "/" + selectionLimit;
        int compCash = companyInstance.GetComponent<Company>().cash;
        int selCash = totalSelectedSalaries();
        budgetTextInstance.GetComponent<Text>().text = "Budget Left: $" + (compCash - selCash);

        blockableButtonInstance.GetComponent<TooltipInterface>().enableTooltip = selectionLimit != selected &&  compCash > selCash;
        blockableButtonInstance.GetComponent<Button>().interactable = selectionLimit == selected &&  compCash > selCash;
        
    }

    public int countSelectedEmployees()
    {
        int result = 0;
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for (int i = 0; i < numEmployees; i++)
        {
            if (employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().isOn)
            {
                result++;
            }
        }
        return result;
    }

    public int totalSelectedSalaries()
    {
        int result = 0;
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for (int i = 0; i < numEmployees; i++)
        {
            if (employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().isOn)
            {
                result += employeeOwnerInstance.transform.GetChild(i).gameObject.GetComponent<Employee>().salary;
            }
        }
        return result;
    }

    // Have to add the function to update selection to the toggle's onValueChange here because
    //this script depends on instance variables from the general scene, which can't be set for the prefab.
    void Start()
    {
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for (int i = 0; i < numEmployees; i++)
        {
            employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().onValueChanged.AddListener(
                delegate { updateSelectionStatus(); }
            );
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
