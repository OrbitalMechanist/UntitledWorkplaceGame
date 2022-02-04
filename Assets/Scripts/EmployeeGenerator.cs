using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 16;

    string[] lNames;
    string[] fNames;

    string[] femNames;
    string[] mNames;

    public Color[] skinColors = {new Color(0.956f, 0.909f, 0.824f), new Color(0.878f, 0.780f, 0.616f), new Color(0.368f, 0.270f, 0.102f) };
    public Color[] hairColors = {new Color(0.93f, 0.93f, 0.83f), new Color(0.666f, 0.545f, 0.278f), new Color(0.1f, 0.1f, 0.1f) };

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

    // Start is called before the first frame update
    void Start()
    {

        lNames = lNamelist.text.Split('\n');
        fNames = fNamelist.text.Split('\n');
        femNames = femNamelist.text.Split('\n');
        mNames = mNamelist.text.Split('\n');

        Debug.Log(lNames[Random.Range(0, lNames.Length)]);

        generateEmployee();
    }

    GameObject generateEmployee()
    {
        bool isMale = true; //Random.value < 0.5;

        string lName = lNames[Random.Range(0, lNames.Length)];
        string fName;

        fName = fNames[Random.Range(0, fNames.Length)];

        int personal = Random.Range(MIN_STAT, MAX_STAT + 1);
        int capability = Random.Range(MIN_STAT, MAX_STAT + 1);
        int ethic = Random.Range(MIN_STAT, MAX_STAT + 1);

        GameObject emp = Instantiate(employeePrefab);

        //Body
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.33f, 0.35f, 0.95f, 0.97f);
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shirts[Random.Range(0, shirts.Length)];

        //Head
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.8f, 1f, 0.4f, 0.8f);
        //skinColors[Random.Range(0, skinColors.Length)];
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = heads[Random.Range(0, heads.Length)];

        //Hat
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hats[Random.Range(0, hats.Length)];
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0f, 1f, 0.2f, 1f);
        //hatColors[Random.Range(0, hairColors.Length)];

        emp.GetComponent<Employee>().Create(fName, lName, personal, capability, ethic);

        return emp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
