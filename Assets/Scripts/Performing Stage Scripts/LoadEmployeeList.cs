using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadEmployeeList : MonoBehaviour
{
    public GameObject employeeManagerInstance; 
    public GameObject UiContainerInstance;
    public GameObject UiDisplayItemPrefab;

    private float containerWidth;
    private float containerHeight;
    private float itemWidth;
    private float itemHeight;
    private int numEmployees;

    private const float V_PADDING = 25;

    public void Start() {
        // Find the employee owner to use later
        employeeManagerInstance = GameObject.Find("employeeOwner");
        numEmployees = employeeManagerInstance.transform.childCount;

        //Calculate sizes for the UI
        containerWidth = UiContainerInstance.GetComponent<RectTransform>().rect.width;
        containerHeight = UiContainerInstance.GetComponent<RectTransform>().rect.height;
        itemWidth = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.width;
        itemHeight = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.height;
    }

    GameObject createEmployeeUi(GameObject UiPrefab, GameObject employeeObject)
    {
        //Create the UI element representing this employee
        GameObject UIElement = Instantiate(UiPrefab);
        //Set First Name display field
        UIElement.transform.GetChild(0).GetComponent<Text>().text = employeeObject.GetComponent<Employee>().fName;
        //Set Last Name display field
        UIElement.transform.GetChild(1).GetComponent<Text>().text = employeeObject.GetComponent<Employee>().lName;
        //Set Capability display bar
        UIElement.transform.GetChild(2).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().capability;
        //Set Work Ethic display bar
        UIElement.transform.GetChild(3).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().ethic;
        //Set Interpersonal Skills display bar
        UIElement.transform.GetChild(4).GetComponent<UnityEngine.UI.Slider>().value 
            = employeeObject.GetComponent<Employee>().personal;
        //Set display image and color for the head. Needs to be retreived from the actual object first.
        UIElement.transform.GetChild(5).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(5).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color;
        //Set hat display
        UIElement.transform.GetChild(6).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(6).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(2).GetComponent<SpriteRenderer>().color;
        //Torso display
        UIElement.transform.GetChild(7).GetComponent<UnityEngine.UI.Image>().sprite
            = employeeObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        UIElement.transform.GetChild(7).GetComponent<UnityEngine.UI.Image>().color
            = employeeObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        return UIElement;
    }

    public void loadEmployees()
    {
        // Clear pre-existing loaded employees
        foreach (Transform child in UiContainerInstance.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // Display all employees in list panel
        for (int i = 0; i < numEmployees; i++)
        {
            // Get the employee of the current idnex
            GameObject emp = employeeManagerInstance.transform.GetChild(i).gameObject;

            //create the UI element and attach it to the container
            GameObject ui = createEmployeeUi(UiDisplayItemPrefab, emp);
            ui.transform.SetParent(UiContainerInstance.transform, false);

            //set UI element position in its container

            // Special case to handle the very first employee
            if (i == 0) {
                ui.transform.localPosition = new Vector3(containerWidth / 2, (-1 * (V_PADDING + (itemHeight / 2))));
            }
            ui.transform.localPosition = new Vector3(containerWidth / 2, (-1 * ((itemHeight + V_PADDING) * i) + (itemHeight / 2)));
        }
    }
}
