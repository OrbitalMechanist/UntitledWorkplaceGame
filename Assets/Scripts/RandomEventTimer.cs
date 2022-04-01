using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class RandomEventTimer : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 0;
    bool hasEvent = false;
    public int count;
    int randEvent;
    UnityAction buttonCallBack;
    GameObject[] randEmploy;
    float time;
    public int empCount;
    public GameObject employee;
    public Button button;
    public GameObject canvas;
    public GameObject Event;
    public GameObject company;
    public class EventObject {
        public string[] Events;
        public string[] Results;
        public List<List<int>> EventButtons = new List<List<int>>();
        public string[] ButtonTexts;
        public List<List<int>> EmployeeIndices = new List<List<int>>();
        public List<List<int>> ButtonIndices = new List<List<int>>();
        public List<List<int>> ButtonValues = new List<List<int>>();
    }
    public delegate void MethodDelegate (int delta, int emp);
    List<MethodDelegate> delList;
    EventObject myEvents;
    string eventJson;
    // Start is called before the first frame update
    void Start()
    {
        time = Random.Range(5.0f, 10.0f);
        eventJson = File.ReadAllText("./Assets/Data/Event Lists/Events.json");
        eventJson = eventJson.Replace("\n", "").Replace("\r", "").Replace("    ", "");
        myEvents = JsonUtility.FromJson<EventObject>(eventJson);
        count = 0;
        delList = new List<MethodDelegate> {RaiseMoney, ChangeHappiness, ChangePersonality, ChangeCapability, ChangeEthic, MassChangeHappiness, EndGame, Fire, generateResult};
        string buttons = File.ReadAllText("./Assets/Data/Event Lists/EventButtons.txt");
        int i = 0;
        foreach (var row in buttons.Split('\n')) {
            myEvents.EventButtons.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.EventButtons[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string indices = File.ReadAllText("./Assets/Data/Event Lists/ButtonIndices.txt");
        foreach (var row in indices.Split('\n')) {
            myEvents.ButtonIndices.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonIndices[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string values = File.ReadAllText("./Assets/Data/Event Lists/ButtonValues.txt");
        foreach (var row in values.Split('\n')) {
            myEvents.ButtonValues.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonValues[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string emp = File.ReadAllText("./Assets/Data/Event Lists/EmployeeIndices.txt");
        foreach (var row in emp.Split('\n')) {
            myEvents.EmployeeIndices.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.EmployeeIndices[i].Add(int.Parse(index));
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEvent) {
            if (time<=0.0f) {
                time = Random.Range(5.0f, 10.0f);
                Event.SetActive(true);
                GameObject test = randomizeEvents();
                test.transform.SetParent(canvas.transform, false);
                Time.timeScale = 0;
                hasEvent = true;
            } else {
                time-=Time.deltaTime;
            }
        }
    }
    public GameObject[] randomEmployees() {
        GameObject employees = company.transform.GetChild(0).gameObject;
        int empCount = company.transform.GetChild(0).childCount;
        GameObject[] empList = new GameObject[empCount];
        int[] empInd = new int[empCount];
        int[] delta = new int[empCount];
        for (int i = 0; i < empCount; i++) {
            empInd[i] = (int)Random.Range(i, empCount);
            delta[i] = 0;
            for (int j = 0; j < i; j++) {
                if (empInd[i]<=(empInd[j]+delta[j])) {
                    delta[i]++;
                }
            }
            empInd[i]-=delta[i];
            empList[i] = employees.transform.GetChild(empInd[i]).gameObject;
        }
        return empList;
    }

// GameObject generateEmployee()
    // {

    //     string lName = lNames[Random.Range(0, lNames.Length)];
    //     string fName = fNames[Random.Range(0, fNames.Length)];

    //     int personal = Random.Range(MIN_STAT, MAX_STAT + 1);
    //     int capability = Random.Range(MIN_STAT, MAX_STAT + 1);
    //     int ethic = Random.Range(MIN_STAT, MAX_STAT + 1);

    //     GameObject emp = Instantiate(employeePrefab);

    //     //Body image + color
    //     emp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Random.ColorHSV(0, 1, 0.33f, 0.35f, 0.95f, 0.97f);
    //     emp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = shirts[Random.Range(0, shirts.Length)];

    //     //Head image + color
    //     emp.transform.GetChild(1).GetComponent<SpriteRenderer>().color = skinColors[Random.Range(0, skinColors.Length)];
    //     // Random.ColorHSV(0, 1, 0.8f, 1f, 0.4f, 0.8f);
    //     emp.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = heads[Random.Range(0, heads.Length)];

    //     //Hat image + color. Hair counts as a hat.
    //     emp.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = hats[Random.Range(0, hats.Length)];
    //     emp.transform.GetChild(2).GetComponent<SpriteRenderer>().color = hatColors[Random.Range(0, hatColors.Length)];
    //     //Random.ColorHSV(0, 1, 0f, 1f, 0.2f, 1f);

    //     //Give name and ability parameters to the actual employee object
    //     emp.GetComponent<Employee>().Create(fName, lName, personal, capability, ethic);

    //     return emp;
    // }
    public GameObject randomizeEvents() {
        randEmploy = randomEmployees();
        GameObject newEvent = Instantiate(Event);
        string desc = myEvents.Events[count];
        for (int i = 0; i < randEmploy.Length; i++) {
            desc = System.String.Format(desc, randEmploy[i].GetComponent<Employee>().fName, randEmploy[i].GetComponent<Employee>().lName, "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}", "{7}");
        }
        newEvent.GetComponentInChildren<Text>().text = desc;
        RectTransform eventWindow = newEvent.transform.GetComponent<RectTransform>();
        RectTransform descRT = newEvent.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        int buttonCount = Random.Range(0, 3);
        for (int i = 0; i < myEvents.EventButtons[count].Count; i++) {
            int temp  = myEvents.EventButtons[count][i];
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
            Button newButton = Instantiate(button);
            newButton.transform.SetParent(newEvent.transform, false);
            newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            string btext = myEvents.ButtonTexts[temp];
            for (int k = 0; k < myEvents.EmployeeIndices[temp].Count; k++) {
                btext = System.String.Format(btext, randEmploy[myEvents.EmployeeIndices[temp][k]].GetComponent<Employee>().fName, "{0}", "{1}", "{2}", "{3}", "{4}");
            }
            newButton.onClick.AddListener(delegate{
                for (int k = 0; k < myEvents.ButtonIndices[temp].Count; k++) {
                    int bTemp = k;
                    delList[myEvents.ButtonIndices[temp][bTemp]](myEvents.ButtonValues[temp][bTemp], myEvents.EmployeeIndices[temp][bTemp]);
                }
                closeEvent(newEvent);});
            newButton.GetComponentInChildren<Text>().text = btext;
        }
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
        return newEvent;
    }
    public void generateResult(int resultIndex, int gamestateIndex) {
        Debug.Log(resultIndex + ", " + gamestateIndex);
        GameObject result = Instantiate(Event);
        string desc = myEvents.Results[resultIndex];
        result.GetComponentInChildren<Text>().text = desc;
        RectTransform descRT = result.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rt = result.transform.GetChild(1).GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+75);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+75);
        Button resultButton = Instantiate(button);
        resultButton.transform.SetParent(result.transform, false);
        resultButton.transform.localPosition = new Vector3(0, -160);
        if (gamestateIndex==1) {
            resultButton.GetComponentInChildren<Text>().text = "Game Over";
            resultButton.onClick.AddListener(delegate{EndGame(1);});
        } else if (company.GetComponent<Company>().cash<0) {
            resultButton.GetComponentInChildren<Text>().text = "Bankrupt!";
            resultButton.onClick.AddListener(delegate{EndGame(1);});
        } else if (company.GetComponent<Company>().happiness<0) {
            resultButton.GetComponentInChildren<Text>().text = "Depression...";
            resultButton.onClick.AddListener(delegate{EndGame(1);});
        } else if (count>=19) {
            resultButton.GetComponentInChildren<Text>().text = "Congratulations!";
            resultButton.onClick.AddListener(delegate{EndGame(0);});
        } else {
            resultButton.GetComponentInChildren<Text>().text = "Continue";
            resultButton.onClick.AddListener(delegate{closeResult(result);});
        }
        result.transform.SetParent(canvas.transform, false);
    }
    public void closeEvent(GameObject thisEvent) {
        count++;
        Destroy(thisEvent);
    }
    public void closeResult(GameObject result) {
        hasEvent = false;
        Time.timeScale = 1;
        Destroy(result);
    }
    public void RaiseMoney(int delta, int emp)
    {
        company.GetComponent<Company>().cash+=delta;
    }
    public void ChangeHappiness(int delta, int emp) {
        randEmploy[emp].GetComponent<Employee>().happiness+=delta;
        if (randEmploy[emp].GetComponent<Employee>().happiness > MAX_STAT) {
            randEmploy[emp].GetComponent<Employee>().happiness = MAX_STAT;
        } if (randEmploy[emp].GetComponent<Employee>().happiness < MIN_STAT) {
            randEmploy[emp].GetComponent<Employee>().happiness = MIN_STAT;
        }
    }
    public void ChangePersonality(int delta, int emp) {
        randEmploy[emp].GetComponent<Employee>().personal+=delta;
        if (randEmploy[emp].GetComponent<Employee>().personal > MAX_STAT) {
            randEmploy[emp].GetComponent<Employee>().personal = MAX_STAT;
        } if (randEmploy[emp].GetComponent<Employee>().personal < MIN_STAT) {
            randEmploy[emp].GetComponent<Employee>().personal = MIN_STAT;
        }
    }
    public void ChangeCapability(int delta, int emp) {
        randEmploy[emp].GetComponent<Employee>().capability+=delta;
        if (randEmploy[emp].GetComponent<Employee>().capability > MAX_STAT) {
            randEmploy[emp].GetComponent<Employee>().capability = MAX_STAT;
        } if (randEmploy[emp].GetComponent<Employee>().capability < MIN_STAT) {
            randEmploy[emp].GetComponent<Employee>().capability = MIN_STAT;
        }
    }
    public void ChangeEthic(int delta, int emp) {
        randEmploy[emp].GetComponent<Employee>().ethic+=delta;
        if (randEmploy[emp].GetComponent<Employee>().ethic > MAX_STAT) {
            randEmploy[emp].GetComponent<Employee>().ethic = MAX_STAT;
        } if (randEmploy[emp].GetComponent<Employee>().ethic < MIN_STAT) {
            randEmploy[emp].GetComponent<Employee>().ethic = MIN_STAT;
        }
    }
    public void Fire(int delta, int emp) {
        Destroy(randEmploy[emp]);
    }
    public void MassChangeHappiness(int delta, int emp) {
        company.GetComponent<Company>().happiness+=delta;
    }
    public void EndGame(int state, int ph) {
        company.GetComponent<Company>().endState = state;
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(company);
    }
    public void EndGame(int state) {
        company.GetComponent<Company>().endState = state;
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(company);
    }
}
