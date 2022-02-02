using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 16;

    string[] lNames;

    string[] femNames;
    string[] mNames;

    public Color[] skinColors = {new Color(0.956f, 0.909f, 0.824f), new Color(0.878f, 0.780f, 0.616f), new Color(0.368f, 0.270f, 0.102f) };
    public Color[] hairColors = {new Color(0.93f, 0.93f, 0.83f), new Color(0.666f, 0.545f, 0.278f), new Color(0.1f, 0.1f, 0.1f) };

    public Sprite[] mShirts;
    public Sprite[] mHeads;
    public Sprite[] mHats;

    public TextAsset lNamelist;
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

        if (isMale)
        {
            fName = mNames[Random.Range(0, mNames.Length)];
        } else
        {
            fName = femNames[Random.Range(0, femNames.Length)];
        }

        int personal = Random.Range(MIN_STAT, MAX_STAT + 1);
        int capability = Random.Range(MIN_STAT, MAX_STAT + 1);
        int ethic = Random.Range(MIN_STAT, MAX_STAT + 1);

        GameObject emp = Instantiate(employeePrefab);

        //Body
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.33f, 0.35f, 0.95f, 0.97f);
        emp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mShirts[Random.Range(0, mShirts.Length)];

        //Head
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = skinColors[Random.Range(0, skinColors.Length)];
        emp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = mHeads[Random.Range(0, mHeads.Length)];

        //Hat
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = mHats[Random.Range(0, mHats.Length)];
        emp.transform.GetChild(2).GetComponent<SpriteRenderer>().color = hairColors[Random.Range(0, hairColors.Length)];

        emp.GetComponent<Employee>().Create(fName, lName, isMale, personal, capability, ethic);

        return emp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
