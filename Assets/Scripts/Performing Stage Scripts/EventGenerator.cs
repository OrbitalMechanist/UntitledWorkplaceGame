using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventGenerator : MonoBehaviour
{
    public GameObject soundObject;
    public TextAsset eventJsonFile;
    public TextAsset buttonsFile;
    public TextAsset indicesFile;
    public TextAsset valuesFile;
    public TextAsset empIndicesFile;
    public TextAsset probChecksFile;
    public TextAsset followUpsFile;
    public int count;
    public GameObject Event;
    public Button button;
    public GameObject canvas;
    string eventJson;
    public class EventObject {
        //Contains all of our events, as well as details for said events
        public string[] Investors;
        public string[] Events;
        public string[] Results;
        public string[] FollowUps;
        public List<List<int>> FollowUpButtons = new List<List<int>>();
        public List<List<int>> EventButtons = new List<List<int>>();
        public string[] ButtonTexts;
        public List<List<int>> ProbChecks = new List<List<int>>();
        public List<List<int>> EmployeeIndices = new List<List<int>>();
        public List<List<int>> ButtonIndices = new List<List<int>>();
        public List<List<int>> ButtonValues = new List<List<int>>();
    }
    public EventObject myEvents;
    // Start is called before the first frame update
    void Start()
    {
        //Read JSON as string
        eventJson = eventJsonFile.text;
        eventJson = eventJson.Replace("\n", "").Replace("\r", "").Replace("    ", "");
        //Parse JSON from string, and implement it into an EventObject
        myEvents = JsonUtility.FromJson<EventObject>(eventJson);
        count = 0;
        //Read buttons as string, containing indices to strings
        string buttons = buttonsFile.text;
        int i = 0;
        //Append indices to a list in the EventObject
        foreach (var row in buttons.Split('\n')) {
            myEvents.EventButtons.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.EventButtons[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        //Read indices file, containing indices to the functions, as string
        string indices = indicesFile.text;
        //Append indices to a list in the EventObject
        foreach (var row in indices.Split('\n')) {
            myEvents.ButtonIndices.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonIndices[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        //Read value file, containing the values that we pass through to the functions, as string
        string values = valuesFile.text;
        //Append values to a list in the EventObject
        foreach (var row in values.Split('\n')) {
            myEvents.ButtonValues.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ButtonValues[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        //Read employee index file, containing each employee's index number referring to the random employee list, as a string
        string emp = empIndicesFile.text;
        //Append employee indices to a list in the EventObject
        foreach (var row in emp.Split('\n')) {
            myEvents.EmployeeIndices.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.EmployeeIndices[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        //Read probability checks file, containing an index that points to a probability check function, as well as contains the values for each function, as a string
        string prob = probChecksFile.text;
        //Append probability checks to a list in the EventObject
        foreach (var row in prob.Split('\n')) {
            myEvents.ProbChecks.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.ProbChecks[i].Add(int.Parse(index));
            }
            i++;
        }
        i = 0;
        //Read follow-up file, containing indices that point to functions for the follow-up events
        string follow = followUpsFile.text;
        //Append follow-up button indices to a list in the EventObject
        foreach (var row in follow.Split('\n')) {
            myEvents.FollowUpButtons.Add(new List<int>());
            foreach (var index in row.Split(' ')) {
                myEvents.FollowUpButtons[i].Add(int.Parse(index));
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void specifiedEvent(int ind) {
        //Places employees in a list in a randomized order
        this.GetComponentInParent<EventFunctions>().randomEmployees();
        //Creates event object
        GameObject newEvent = Instantiate(Event);
        //Gets string for current event
        string desc = myEvents.FollowUps[ind];
        //Parses names of employees into description, if applicable
        for (int i = 0; i < this.GetComponentInParent<EventFunctions>().randEmploy.Length; i++) {
            desc = System.String.Format(desc, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().fName, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().lName, "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}", "{7}");
        }
        newEvent.GetComponentInChildren<Text>().text = desc;
        //Grabs the sizes of each the event window, the description box, and the area at the bottom
        RectTransform eventWindow = newEvent.transform.GetComponent<RectTransform>();
        RectTransform descRT = newEvent.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        //Creates a button and places it based on the size of the window
        for (int i = 0; i < myEvents.FollowUpButtons[ind].Count; i++) {
            int temp  = myEvents.FollowUpButtons[ind][i];
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
            Button newButton = Instantiate(button);
            //Adds button to window
            newButton.transform.SetParent(rt.transform, false);
            //newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            //Loads text as string
            string btext = myEvents.ButtonTexts[temp];
            //Parses names of employees into buttons, if applicable
            for (int k = 0; k < myEvents.EmployeeIndices[temp].Count; k++) {
                btext = System.String.Format(btext, this.GetComponentInParent<EventFunctions>().randEmploy[myEvents.EmployeeIndices[temp][k]].GetComponent<Employee>().fName, "{0}", "{1}", "{2}", "{3}", "{4}");
            }
            AudioSource click = soundObject.transform.Find("Click").gameObject.GetComponent<AudioSource>();
            //Places function calls into button on click, along with playing a click sound effect
            newButton.onClick.AddListener(delegate{
                int suc = myEvents.ProbChecks[temp][3];
                int fail = myEvents.ProbChecks[temp][4];
                bool state = this.GetComponentInParent<EventFunctions>().checkList[myEvents.ProbChecks[temp][0]](myEvents.ProbChecks[temp][1], myEvents.ProbChecks[temp][2], myEvents.ProbChecks[temp][3], myEvents.ProbChecks[temp][4]);
                if (state) {
                    generateResult(myEvents.ButtonValues[suc][myEvents.ButtonValues[suc].Count-1], myEvents.EmployeeIndices[suc][myEvents.EmployeeIndices[suc].Count-1]);
                } else {
                    generateResult(myEvents.ButtonValues[fail][myEvents.ButtonValues[fail].Count-1], myEvents.EmployeeIndices[fail][myEvents.EmployeeIndices[fail].Count-1]);
                }
                click.Play();
                closeEvent(newEvent);
            });
            //Adds text to button
            newButton.GetComponentInChildren<Text>().text = btext;
        }
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
        newEvent.transform.SetParent(canvas.transform, false);
    }
    public void randomizeEvents() {
        //Places employees in a list in a randomized order
        this.GetComponentInParent<EventFunctions>().randomEmployees();
        //Creates event object
        GameObject newEvent = Instantiate(Event);
        //Gets string for current event
        string desc = myEvents.Events[count];
        //Parses names of employees into description, if applicable
        for (int i = 0; i < this.GetComponentInParent<EventFunctions>().randEmploy.Length; i++) {
            desc = System.String.Format(desc, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().fName, this.GetComponentInParent<EventFunctions>().randEmploy[i].GetComponent<Employee>().lName, "{0}", "{1}", "{2}", "{3}", "{4}", "{5}", "{6}", "{7}");
        }
        newEvent.GetComponentInChildren<Text>().text = desc;
        //Grabs the sizes of each the event window, the description box, and the area at the bottom
        RectTransform eventWindow = newEvent.transform.GetComponent<RectTransform>();
        RectTransform descRT = newEvent.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rt = newEvent.transform.GetChild(1).GetComponent<RectTransform>();
        //Creates a button and places it based on the size of the window
        for (int i = 0; i < myEvents.EventButtons[count].Count; i++) {
            int temp  = myEvents.EventButtons[count][i];
            rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
            descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
            Button newButton = Instantiate(button);
            //Adds button to window
            newButton.transform.SetParent(rt.transform, false);
            //newButton.transform.localPosition = new Vector3(0, -160+(i*(float)37.5));
            //Loads text as string
            string btext = myEvents.ButtonTexts[temp];
            //Parses names of employees into buttons, if applicable
            for (int k = 0; k < myEvents.EmployeeIndices[temp].Count; k++) {
                btext = System.String.Format(btext, this.GetComponentInParent<EventFunctions>().randEmploy[myEvents.EmployeeIndices[temp][k]].GetComponent<Employee>().fName, "{0}", "{1}", "{2}", "{3}", "{4}");
            }
            AudioSource click = soundObject.transform.Find("Click").gameObject.GetComponent<AudioSource>();
            //Places function calls into button on click, along with playing a click sound effect
            newButton.onClick.AddListener(delegate{
                int suc = myEvents.ProbChecks[temp][3];
                int fail = myEvents.ProbChecks[temp][4];
                bool state = this.GetComponentInParent<EventFunctions>().checkList[myEvents.ProbChecks[temp][0]](myEvents.ProbChecks[temp][1], myEvents.ProbChecks[temp][2], myEvents.ProbChecks[temp][3], myEvents.ProbChecks[temp][4]);
                if (state) {
                    generateResult(myEvents.ButtonValues[suc][myEvents.ButtonValues[suc].Count-1], myEvents.EmployeeIndices[suc][myEvents.EmployeeIndices[suc].Count-1]);
                } else {
                    generateResult(myEvents.ButtonValues[fail][myEvents.ButtonValues[fail].Count-1], myEvents.EmployeeIndices[fail][myEvents.EmployeeIndices[fail].Count-1]);
                }
                click.Play();
                closeEvent(newEvent);
            });
            //Adds text to button
            newButton.GetComponentInChildren<Text>().text = btext;
        }
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+(float)37.5);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+(float)37.5);
        newEvent.transform.SetParent(canvas.transform, false);
    }
    public void generateResult(int resultIndex, int gamestateIndex) {
        //Loads sound effects
        AudioSource click = soundObject.transform.Find("Click").gameObject.GetComponent<AudioSource>();
        AudioSource cheer = soundObject.transform.Find("Cheer").gameObject.GetComponent<AudioSource>();
        AudioSource jeer = soundObject.transform.Find("Jeer").gameObject.GetComponent<AudioSource>();
        Debug.Log(resultIndex + ", " + gamestateIndex);
        //Creates event object
        GameObject result = Instantiate(Event);
        string desc = myEvents.Results[resultIndex];
        result.GetComponentInChildren<Text>().text = desc;
        //Grabs the sizes of each the event window, the description box, and the area at the bottom
        RectTransform descRT = result.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rt = result.transform.GetChild(1).GetComponent<RectTransform>();
        rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+75);
        descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+75);
        //Creates button object and adds it to event window
        Button resultButton = Instantiate(button);
        resultButton.transform.SetParent(rt.transform, false);
        resultButton.transform.localPosition = new Vector3(0, -160);
        //Sets button's functions on click based on the gamestate, as well as the company's money and happiness
        if (gamestateIndex==1) {
            jeer.Play();
            resultButton.GetComponentInChildren<Text>().text = "Game Over";
            resultButton.onClick.AddListener(delegate{click.Play();EndGame(1);});
        } else if (this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().cash<0) {
            jeer.Play();
            resultButton.GetComponentInChildren<Text>().text = "Bankrupt!";
            resultButton.onClick.AddListener(delegate{click.Play();EndGame(2);});
        } else if (this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().happiness<0) {
            jeer.Play();
            resultButton.GetComponentInChildren<Text>().text = "Depression...";
            resultButton.onClick.AddListener(delegate{click.Play();EndGame(3);});
        } else if (count>=19) {
            cheer.Play();
            resultButton.GetComponentInChildren<Text>().text = "Congratulations!";
            resultButton.onClick.AddListener(delegate{click.Play();EndGame(0);});
        } else {
            resultButton.GetComponentInChildren<Text>().text = "Continue";
            resultButton.onClick.AddListener(delegate{click.Play();closeResult(result);});
        }
        result.transform.SetParent(canvas.transform, false);
    }
    public void closeEvent(GameObject thisEvent) {
        //Destroys the event object, and increments the counter of events that have passed
        count+=1;
        if (count>20) {
            count = 20;
        }
        Destroy(thisEvent);
    }
    public void closeResult(GameObject result) {
        //Destroys result object
        this.GetComponentInParent<EventHandler>().hasEvent = false;
        Time.timeScale = 1;
        Destroy(result);
    }
    public void EndGame(int state) {
        //Time.timeScale = 1;
        this.GetComponentInParent<EventFunctions>().company.GetComponent<Company>().endState = state;
        //Switches scene to result
        SceneManager.LoadScene("results");
        //Saves company object
        DontDestroyOnLoad(this.GetComponentInParent<EventFunctions>().company);
    }
}
