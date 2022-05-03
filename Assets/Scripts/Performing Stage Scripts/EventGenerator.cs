using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventGenerator : MonoBehaviour
{
    public TextAsset eventJsonFile;
    public TextAsset buttonsFile;
    public TextAsset indicesFile;
    public TextAsset valuesFile;
    public TextAsset empIndicesFile;
    public int count;
    public GameObject Event;
    public Button button;
    public GameObject canvas;
    string eventJson;
    public class EventObject {
        public string[] Events;
        public string[] Results;
        public List<List<int>> EventButtons = new List<List<int>>();
        public string[] ButtonTexts;
        public List<List<int>> EmployeeIndices = new List<List<int>>();
        public List<List<int>> ButtonIndices = new List<List<int>>();
        public List<List<int>> ButtonValues = new List<List<int>>();
    }
    EventObject myEvents;
    // Start is called before the first frame update
    void Start()
    {
        eventJson = eventJsonFile.text;
        eventJson = eventJson.Replace("\n", "").Replace("\r", "").Replace("    ", "");
        myEvents = JsonUtility.FromJson<EventObject>(eventJson);
        count = 0;
        string buttons = buttonsFile.text;
        int i = 0;
        foreach (var row in buttons.Split('\n')) {
            myEvents.EventButtons.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.EventButtons[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string indices = indicesFile.text;
        foreach (var row in indices.Split('\n')) {
            myEvents.ButtonIndices.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonIndices[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string values = valuesFile.text;
        foreach (var row in values.Split('\n')) {
            myEvents.ButtonValues.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonValues[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        string emp = empIndicesFile.text;
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
        
    }
    public void randomizeEvents() {
        this.GetComponentInParent<EventFunctions>().randomEmployees();
        GameObject newEvent = Instantiate(Event);
        string desc = myEvents.Events[count];
        for (int i = 0; i < this.GetComponentInParent<EventFunctions>().randEmploy.Length; i++) {
            desc = System.String.Format(desc, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().fName, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().lName, "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}", "{7}");
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
            newButton.transform.SetParent(rt.transform, false);
            //newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            string btext = myEvents.ButtonTexts[temp];
            for (int k = 0; k < myEvents.EmployeeIndices[temp].Count; k++) {
                btext = System.String.Format(btext, this.GetComponentInParent<EventFunctions>().randEmploy[myEvents.EmployeeIndices[temp][k]].GetComponent<Employee>().fName, "{0}", "{1}", "{2}", "{3}", "{4}");
            }
            newButton.onClick.AddListener(delegate{
                for (int k = 0; k < myEvents.ButtonIndices[temp].Count-1; k++) {
                    int bTemp = k;
                    this.GetComponentInParent<EventFunctions>().delList[myEvents.ButtonIndices[temp][bTemp]](myEvents.ButtonValues[temp][bTemp], myEvents.EmployeeIndices[temp][bTemp]);
                }
                generateResult(myEvents.ButtonValues[temp][myEvents.ButtonValues[temp].Count-1], myEvents.EmployeeIndices[temp][myEvents.EmployeeIndices[temp].Count-1]);
                closeEvent(newEvent);});
            newButton.GetComponentInChildren<Text>().text = btext;
        }
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
        newEvent.transform.SetParent(canvas.transform, false);;
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
        resultButton.transform.SetParent(rt.transform, false);
        resultButton.transform.localPosition = new Vector3(0, -160);
        if (gamestateIndex==1) {
            resultButton.GetComponentInChildren<Text>().text = "Game Over";
            resultButton.onClick.AddListener(delegate{EndGame(1);});
        } else if (this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().cash<0) {
            resultButton.GetComponentInChildren<Text>().text = "Bankrupt!";
            resultButton.onClick.AddListener(delegate{EndGame(2);});
        } else if (this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().happiness<0) {
            resultButton.GetComponentInChildren<Text>().text = "Depression...";
            resultButton.onClick.AddListener(delegate{EndGame(3);});
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
        count+=1;
        if (count>20) {
            count = 20;
        }
        Destroy(thisEvent);
    }
    public void closeResult(GameObject result) {
        this.GetComponentInParent<EventHandler>().hasEvent = false;
        Time.timeScale = 1;
        Destroy(result);
    }
    public void EndGame(int state) {
        Time.timeScale = 1;
        this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().endState = state;
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(this.GetComponentInParent<EventFunctions>().company);
    }
}
