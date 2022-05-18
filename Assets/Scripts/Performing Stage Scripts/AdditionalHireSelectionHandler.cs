using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalHireSelectionHandler : MonoBehaviour
{
    //WARNING: This script is hardcoded to the extreme and relies on a certain structure.
    //It is modified from EmployeeSelectionHandler found in the Forming Stage; do NOT confuse them.
    //Intended to be used exclusively in the Performing Stage scene.

    //The generatedOwnerInstance must contain the freshly-generated employees up for selection as its first children.
    //THIS IS NOT THE OBJECT CALLED "employeeOwner"!!! That one contains the employees already at the company.
    public GameObject generatedOwnerInstance;

    //The employeeItemContainer contains nothing but EmployeePanel items,
    //in the order that they are stored in the employeeOwner.
    //The EmployeePanel must have a Toggle as its child with the index 11.
    public GameObject employeeItemContainerInstance;

    //The Company object that contains the Company script to use for money.
    //This script will have to find it at runtime.
    public GameObject companyInstance;

    //A Text UI element that says how many characters were selected.
    //Modified with extra text and colors!!!
    public GameObject statusTextInstance;

    //A Text UI element that says how much money will be left after the selected employees are hired.
    //Modified with more text.
    public GameObject budgetTextInstance;

    //The Button to block while the wrong number of employees is selected.
    public GameObject blockableButtonInstance;

    //How many employees already existed when the midgame hiring popped up.
    public int selectionStart = 0;

    //How many employees are allowed to be selected.
    public int selectionLimit = 5;

    //this function is very destructive. Used to transfer the employees we want to keep to the object
    //called employeeOwner.
    public void transferSelectedToEmployeeOwner()
    {
        GameObject owner = GameObject.Find("employeeOwner");
        if(owner == null)
        {
            return;
        }
        
        //this ought to reduce the number of calls.
        Transform targetTransform = owner.transform;
        Transform originTransform = generatedOwnerInstance.transform;

        cleanEmployeeObjectsBySelection();

        while(originTransform.childCount > 0) {
            originTransform.GetChild(0).SetParent(targetTransform, false);
        }

        //this is clunky but once again it's instance variables for a prefab asset
        GameObject.Find("Organizer").GetComponent<StageOrganizer>().AssignTargetsToEmployees();

        Time.timeScale = 1;
    }

    //This function serves to strip out unselected employees before
    //their container is handed over to SwitchToScenePreservingItem.
    public void cleanEmployeeObjectsBySelection()
    {
        LinkedList<int> selectedIndeces = new LinkedList<int>();
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for (int i = 0; i < numEmployees; i++)
        {
            if (employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().isOn)
            {
                selectedIndeces.AddFirst(i);
            }
        }

        //Doing this in reverse is necessary so that deleting objects doesn't affect the index
        //of the ones we deal with later on
        for (int i = numEmployees - 1; i >= 0; i--)
        {
            if (selectedIndeces.Count == 0 || selectedIndeces.First.Value != i)
            {
                //this has to be done by unparenting first and only then deleting. I don't know why
                //but the destroyed gameobject is still apparently a child.
                GameObject victim = generatedOwnerInstance.transform.GetChild(i).gameObject;
                victim.transform.SetParent(null);
                Destroy(victim);
            }
            else
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
        statusTextInstance.GetComponent<Text>().text = "Selected: " + (selectionStart + selected) + "/" + selectionLimit;
        int compCash = companyInstance.GetComponent<Company>().cash;
        int selCash = totalSelectedSalaries();
        budgetTextInstance.GetComponent<Text>().text = "Budget Left: $" + (compCash - selCash);

        blockableButtonInstance.GetComponent<Button>().interactable = selectionLimit >= selected + selectionStart
            && compCash > selCash;
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
                result += generatedOwnerInstance.transform.GetChild(i).gameObject.GetComponent<Employee>().salary;
            }
        }
        return result;
    }


    // Have to add the function to update selection to the toggle's onValueChange here because
    //this script depends on instance variables from the general scene, which can't be set for the prefab.
    void Start()
    {
        selectionStart = GameObject.Find("employeeOwner").transform.childCount;
        companyInstance = GameObject.FindGameObjectWithTag("Company");

        StartCoroutine(AddTriggers()); //need to wait for the triggers to get spawned
    }

    IEnumerator AddTriggers()
    {
        Canvas.ForceUpdateCanvases();

        yield return null;

        int numEmployees = employeeItemContainerInstance.transform.childCount;
        Debug.LogWarning(employeeItemContainerInstance);

        for (int i = 0; i < numEmployees; i++)
        {
            employeeItemContainerInstance.transform.GetChild(i).GetChild(11).gameObject.GetComponent<Toggle>().onValueChanged.AddListener(
                delegate { updateSelectionStatus(); }
            );
        }

        updateSelectionStatus();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
