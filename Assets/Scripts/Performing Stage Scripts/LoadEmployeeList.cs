using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadEmployeeList : MonoBehaviour
{
    GameObject[] employeeList;
    public GameObject employeeManagerInstance; // ?
    public GameObject UiContainerInstance;
    public GameObject UiDisplayItemPrefab;

    private float containerWidth;
    private float itemWidth;
    private float itemHeight;
    private const float vPadding = 20;

    public void Start() {
        //Calculate sizes for the UI
        containerWidth = UiContainerInstance.GetComponent<RectTransform>().rect.width;
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

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < employeeList.Length; i++)
        {
            GameObject emp = employeeList[i]; // probably needs adjusting

            //Add employee object to manager object and keep it off the screen
            emp.transform.parent = employeeManagerInstance.transform;
            emp.transform.localPosition = Vector3.zero;

            //create the UI element and attach it to the container
            GameObject ui = createEmployeeUi(UiDisplayItemPrefab, emp);
            ui.transform.SetParent(UiContainerInstance.transform, false);

            //set UI element position in its container
            ui.transform.localPosition = new Vector3(0, -1 * ((itemHeight + vPadding) * (i + 1)) + itemHeight/2);
        }
    }
}
