using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadEmployeeList : MonoBehaviour
{
    public GameObject employeeManagerInstance; 
    public GameObject UiContainerInstance;
    public GameObject UiDisplayItemPrefab;
    public GameObject attributeTextPrefab;

    private float containerWidth;
    private float containerHeight;
    private float itemWidth;
    private float itemHeight;
    private int numEmployees;

    private const float V_PADDING = 25;

    public void Start() {
        
    }

    GameObject createEmployeeUi(GameObject UiPrefab, GameObject employeeObject)
    {
        //Create the UI element representing this employee
        GameObject UIElement = Instantiate(UiPrefab);
        //Set Name display field
        UIElement.transform.GetChild(0).GetComponent<Text>().text = employeeObject.GetComponent<Employee>().fName 
            + " " + employeeObject.GetComponent<Employee>().lName;
        //Set Last Name display field, back when they were two separate lines
        //UIElement.transform.GetChild(1).GetComponent<Text>().text = employeeObject.GetComponent<Employee>().lName;
        //Set Capability display bar and number
        UIElement.transform.GetChild(2).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().capability;
        UIElement.transform.GetChild(3).GetComponent<Text>().text 
            = employeeObject.GetComponent<Employee>().capability.ToString();
        //Set Work Ethic display bar and number
        UIElement.transform.GetChild(4).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().ethic;
        UIElement.transform.GetChild(5).GetComponent<Text>().text 
            = employeeObject.GetComponent<Employee>().ethic.ToString();
        //Set Interpersonal Skills display bar and number
        UIElement.transform.GetChild(6).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().personal;
        UIElement.transform.GetChild(7).GetComponent<Text>().text 
            = employeeObject.GetComponent<Employee>().personal.ToString();
        //Set display image and color for the head. Needs to be retreived from the actual object first.
        UIElement.transform.GetChild(8).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(8).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color;
        //Set hat display
        UIElement.transform.GetChild(9).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(9).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
        //Torso display
        UIElement.transform.GetChild(10).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(10).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        for(int i = 0; i < employeeObject.GetComponent<Employee>().attributeNames.Count; i++)
        {
            // Create text object to display attribute
            GameObject newAttribute = Instantiate(attributeTextPrefab); 
            Text newAttributeText = newAttribute.GetComponent<Text>();

            // Add tooltip to attribute
            TooltipInterface newAttributeTooltip = newAttribute.AddComponent<TooltipInterface>();
            newAttributeTooltip.setTooltipHeaderText(employeeObject.GetComponent<Employee>().attributes[i].tooltipHeaderText);
            newAttributeTooltip.setTooltipDescriptionText(employeeObject.GetComponent<Employee>().attributes[i].tooltipDescriptionText);

            // Set text of attribute display
            newAttributeText.text = employeeObject.GetComponent<Employee>().attributeNames[i];
            
            // Add attribute to employee card
            newAttribute.transform.SetParent(UIElement.transform.Find("AttrHolder"));
        }
        return UIElement;
    }

    public void loadEmployees()
    {
        // Find the employee owner to use later
        employeeManagerInstance = GameObject.Find("employeeOwner");
        numEmployees = employeeManagerInstance.transform.childCount;

        //Calculate sizes for the UI
        containerWidth = UiContainerInstance.GetComponent<RectTransform>().rect.width;
        containerHeight = UiContainerInstance.GetComponent<RectTransform>().rect.height;
        itemWidth = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.width;
        itemHeight = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.height;

        // Check and update number of employees
        numEmployees = employeeManagerInstance.transform.childCount;

        // Clear pre-existing loaded employees
        // Note, indeces 0, 1 and 2 are intentionally skipped as that is the hiring interface
        for (int i = 3; i < UiContainerInstance.transform.childCount; i++)
        {
            GameObject.Destroy(UiContainerInstance.transform.GetChild(i).gameObject);
        }

        // Display all employees in list panel
        for (int i = 0; i < numEmployees; i++)
        {
            // Get the employee of the current idnex
            GameObject emp = employeeManagerInstance.transform.GetChild(i).gameObject;

            //create the UI element and attach it to the container
            GameObject ui = createEmployeeUi(UiDisplayItemPrefab, emp);
            ui.transform.SetParent(UiContainerInstance.transform, false);
        }
    }
}
