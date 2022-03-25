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
    int count;
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
        delList = new List<MethodDelegate> {RaiseMoney, ChangeHappiness, ChangePersonality, ChangeCapability, ChangeEthic, MassChangeHappiness, EndGame};
        count = 0;
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
        string emp = File.ReadAllText("./Assets/Data/Event Lists/ButtonValues.txt");
        foreach (var row in emp.Split('\n')) {
            myEvents.ButtonValues.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonValues[i].Add(int.Parse(index));
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasEvent) {
            if (time<=0.0f) {
                count++;
                time = Random.Range(5.0f, 10.0f);
                Event.SetActive(true);
                GameObject test = randomizeEvents();
                test.transform.SetParent(canvas.transform, false);
                hasEvent = true;
            } else {
                time-=Time.deltaTime;
            }
        }
    }
    public GameObject[] randomEmployees() {
        GameObject employees = company.transform.GetChild(0).gameObject;
        empCount = company.transform.GetChild(0).childCount;
        GameObject[] empList = new GameObject[empCount];
        int[] empInd = new int[empCount];
        for (int i = 0; i < empCount; i++) {
            empInd[i] = (int)Random.Range(i, empCount);
            int delta = 0;
            for (int j = 0; j < i; j++) {
                if (empInd[i]<=empInd[j]) {
                    delta++;
                }
                empInd[i]-=delta;
            }
            empList[i] = employees.transform.GetChild(empInd[i]).gameObject;
        }
        return empList;
    }
    public GameObject randomizeEvents() {
        randEmploy = randomEmployees();
        GameObject newEvent = Instantiate(Event);
        newEvent.GetComponentInChildren<Text>().text = myEvents.Events[0];
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, -350);
        int buttonCount = Random.Range(0, 3);
        for (int i = 0; i < myEvents.EventButtons[0].Count; i++) {
            int temp  = myEvents.EventButtons[0][i];
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            Button newButton = Instantiate(button);
            newButton.transform.SetParent(newEvent.transform, false);
            newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            newButton.onClick.AddListener(delegate{
                for (int k = 0; k < myEvents.EventButtons.Count; k++) {
                    delList[myEvents.ButtonIndices[temp][k]](myEvents.ButtonValues[temp][k], myEvents.EmployeeIndices[temp][k]);
                }
                closeEvent(newEvent);});
            newButton.GetComponentInChildren<Text>().text = myEvents.ButtonTexts[temp];
        }
        return newEvent;
    }
    public void closeEvent(GameObject thisEvent) {
        Destroy(thisEvent);
        hasEvent = false;
    }
    public void RaiseMoney(int delta, int emp)
    {
        company.GetComponent<Company>().cash+=delta;
    }
    public void ChangeHappiness(int delta, int emp) {
        randEmploy[0].GetComponent<Employee>().happiness+=delta;
        if (randEmploy[0].GetComponent<Employee>().happiness > MAX_STAT) {
            randEmploy[0].GetComponent<Employee>().happiness = MAX_STAT;
        } if (randEmploy[0].GetComponent<Employee>().happiness < MIN_STAT) {
            randEmploy[0].GetComponent<Employee>().happiness = MIN_STAT;
        }
    }
    public void ChangePersonality(int delta, int emp) {
        randEmploy[0].GetComponent<Employee>().personal+=delta;
        if (randEmploy[0].GetComponent<Employee>().personal > MAX_STAT) {
            randEmploy[0].GetComponent<Employee>().personal = MAX_STAT;
        } if (randEmploy[0].GetComponent<Employee>().personal < MIN_STAT) {
            randEmploy[0].GetComponent<Employee>().personal = MIN_STAT;
        }
    }
    public void ChangeCapability(int delta, int emp) {
        randEmploy[0].GetComponent<Employee>().capability+=delta;
        if (randEmploy[0].GetComponent<Employee>().capability > MAX_STAT) {
            randEmploy[0].GetComponent<Employee>().capability = MAX_STAT;
        } if (randEmploy[0].GetComponent<Employee>().capability < MIN_STAT) {
            randEmploy[0].GetComponent<Employee>().capability = MIN_STAT;
        }
    }
    public void ChangeEthic(int delta, int emp) {
        randEmploy[0].GetComponent<Employee>().ethic+=delta;
        if (randEmploy[0].GetComponent<Employee>().ethic > MAX_STAT) {
            randEmploy[0].GetComponent<Employee>().ethic = MAX_STAT;
        } if (randEmploy[0].GetComponent<Employee>().ethic < MIN_STAT) {
            randEmploy[0].GetComponent<Employee>().ethic = MIN_STAT;
        }
    }
    public void MassChangeHappiness(int delta, int emp) {

    }
    public void EndGame(int state, int ph) {
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(company);
    }
}
