using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EmployeeGenerator : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 16;

    public int numToGenerate = 5;

    string[] lNames;
    string[] fNames;

    string[] femNames;
    string[] mNames;

    public Color[] skinColors; 
    //= {new Color(0.956f, 0.909f, 0.824f), new Color(0.878f, 0.780f, 0.616f), new Color(0.368f, 0.270f, 0.102f) };
    public Color[] hatColors; 
    //= {new Color(0.93f, 0.93f, 0.83f), new Color(0.666f, 0.545f, 0.278f), new Color(0.1f, 0.1f, 0.1f) };

    public Sprite[] shirts;
    public Sprite[] heads;
    public Sprite[] hats;

    public TextAsset lNamelist;
    public TextAsset fNamelist;
    public TextAsset femNamelist;
    public TextAsset mNamelist;

    public GameObject employeePrefab;

    public Sprite shirtSprite;
    public Sprite faceSprite;
    public Sprite hatSprite;

    public GameObject employeeManagerInstance;

    public GameObject UiContainerInstance;

    public GameObject UiDisplayItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Load namelists from files
        lNames = lNamelist.text.Split('\n');
        fNames = fNamelist.text.Split('\n');
        femNames = femNamelist.text.Split('\n');
        mNames = mNamelist.text.Split('\n');

        //Rebuild everything so that the sizes get initialized before the next step.
        Canvas.ForceUpdateCanvases();

        //Calculate sizes for the UI
        float containerWidth = UiContainerInstance.GetComponent<RectTransform>().rect.size.x; //.width;
        float itemWidth = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.width;
        float itemHeight = UiDisplayItemPrefab.GetComponent<RectTransform>().rect.height;

        float vPadding = 20; //could be calculated somehow if necessary but it can be a constant

        int numElementsPerRow = (int)containerWidth / (int)itemWidth;

        float spacePerItem = containerWidth / numElementsPerRow;
        float leftPaddingPerItem = (spacePerItem * numElementsPerRow - containerWidth) / 2;

        Debug.Log(numElementsPerRow + " " + containerWidth + " " + itemWidth);

        //Resize scrollable area to fit the necessary number of items
        UiContainerInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (itemHeight + vPadding) * 
            (numToGenerate / numElementsPerRow + (numToGenerate % numElementsPerRow > 0 ? 1 : 0)) + vPadding);

        //Generate employees and set up their UI display
        int y = 1;
        int i = 0;
        while (i < numToGenerate)
        {
            for (int x = 1; x <= numElementsPerRow; x++) {
                GameObject emp = generateEmployee();

                //Add employee object to manager object and keep it off the screen
                emp.transform.parent = employeeManagerInstance.transform;
                emp.transform.localPosition = Vector3.zero;

                //create the UI element and attach it to the container
                GameObject ui = createEmployeeUi(UiDisplayItemPrefab, emp);
                ui.transform.SetParent(UiContainerInstance.transform, false);

                //set UI element position in its container
                ui.transform.localPosition = new Vector3(x * spacePerItem + leftPaddingPerItem - spacePerItem/2,
                    -1 * ((itemHeight + vPadding) * y) + itemHeight/2);

                i++;
                if(i == numToGenerate)
                {
                    break;
                }
            }
            y++;
        }
    }

    GameObject generateEmployee()
    {

        string lName = lNames[Random.Range(0, lNames.Length)];
        string fName = fNames[Random.Range(0, fNames.Length)];

        int personal = Random.Range(MIN_STAT, MAX_STAT + 1);
        int capability = Random.Range(MIN_STAT, MAX_STAT + 1);
        int ethic = Random.Range(MIN_STAT, MAX_STAT + 1);

        GameObject emp = Instantiate(employeePrefab);

        //Body image + color
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.33f, 0.35f, 0.95f, 0.97f);
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shirts[Random.Range(0, shirts.Length)];

        //Head image + color
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = skinColors[Random.Range(0, skinColors.Length)];
        // Random.ColorHSV(0, 1, 0.8f, 1f, 0.4f, 0.8f);
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = heads[Random.Range(0, heads.Length)];

        //Hat image + color. Hair counts as a hat.
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hats[Random.Range(0, hats.Length)];
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().color = hatColors[Random.Range(0, hatColors.Length)];
        //Random.ColorHSV(0, 1, 0f, 1f, 0.2f, 1f);

        //Give name and ability parameters to the actual employee object
        emp.GetComponent<Employee>().Create(fName, lName, personal, capability, ethic);

        return emp;
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
        
    }
}
