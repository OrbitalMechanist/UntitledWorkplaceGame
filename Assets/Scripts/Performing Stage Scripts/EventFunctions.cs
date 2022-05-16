using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventFunctions : MonoBehaviour
{
    private static int MAX_STAT = 256;
    private static int MIN_STAT = 0;
    public GameObject[] randEmploy;
    public GameObject company;
    public delegate void MethodDelegate (int delta, int emp);
    public delegate bool CheckDelegate (int emp, int prob, int sucInd, int failInd);
    public List<MethodDelegate> delList;
    public List<CheckDelegate> checkList;
    // Start is called before the first frame update
    void Start()
    {
        //Assigns each function to a list to be called by the event generator
        delList = new List<MethodDelegate> {RaiseMoney, ChangeHappiness, ChangePersonality, ChangeCapability, ChangeEthic, MassChangeHappiness, EndGame, Fire};
        checkList = new List<CheckDelegate> {alwaysFalse, alwaysTrue, randomCheck, ethicCheck, happinessCheck, capabilityCheck, personalityCheck, moneyCheck, masshappinessCheck};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseMoney(int delta, int emp) {
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
        Time.timeScale = 1;
        company.GetComponent<Company>().endState = state;
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(company);
    }
    public void EndGame(int state) {
        Time.timeScale = 1;
        company.GetComponent<Company>().endState = state;
        SceneManager.LoadScene("results");
        DontDestroyOnLoad(company);
    }
    public void addInvestor(int invInd, int plan) {

    }
    public void futureEvent(int ind, int count) {
        this.GetComponentInParent<EventHandler>().followUpStack.Add(ind);
        this.GetComponentInParent<EventHandler>().followUpTimer.Add(count);
    }
    public bool ethicCheck(int emp, int prob, int sucInd, int failInd) {
        int check = Random.Range(0, 256);
        int modProb = (int)(randEmploy[emp].GetComponent<Employee>().ethic-128-prob);
        bool pass = check<=modProb;
        return (check<=modProb);
    }
    public bool happinessCheck(int emp, int prob, int sucInd, int failInd) {
        int check = Random.Range(0, 256);
        int modProb = (int)(randEmploy[emp].GetComponent<Employee>().happiness-128+prob);
        return (check<=modProb);
    }
    public bool capabilityCheck(int emp, int prob, int sucInd, int failInd) {
        int check = Random.Range(0, 256);
        int modProb = (int)(randEmploy[emp].GetComponent<Employee>().capability-128+prob);
        return (check<=modProb);
    }
    public bool personalityCheck(int emp, int prob, int sucInd, int failInd) {
        int check = Random.Range(0, 256);
        int modProb = (int)(randEmploy[emp].GetComponent<Employee>().personal-128+prob);
        return (check<=modProb);
    }
    public bool moneyCheck(int emp, int prob, int sucInd, int failInd) {
        return (company.GetComponent<Company>().cash>prob);
    }
    public bool masshappinessCheck(int emp, int prob, int sucInd, int failInd) {
        return (company.GetComponent<Company>().happiness>prob);
    }
    public bool randomCheck(int emp, int prob, int sucInd, int failInd) {
        int check = Random.Range(0, 256);
        return (check<=prob);
    }
    public bool alwaysTrue(int emp, int prob, int sucInd, int failInd) {
        for (int i = 0; i < this.GetComponentInParent<EventGenerator>().myEvents.ButtonIndices[sucInd].Count-1; i++) {
            delList[this.GetComponentInParent<EventGenerator>().myEvents.ButtonIndices[sucInd][i]](this.GetComponentInParent<EventGenerator>().myEvents.ButtonValues[sucInd][i], this.GetComponentInParent<EventGenerator>().myEvents.EmployeeIndices[sucInd][i]);
        }
        return true;
    }
    public bool alwaysFalse(int emp, int prob, int sucInd, int failInd) {
        for (int i = 0; i < this.GetComponentInParent<EventGenerator>().myEvents.ButtonIndices[failInd].Count-1; i++) {
            delList[this.GetComponentInParent<EventGenerator>().myEvents.ButtonIndices[failInd][i]](this.GetComponentInParent<EventGenerator>().myEvents.ButtonValues[failInd][i], this.GetComponentInParent<EventGenerator>().myEvents.EmployeeIndices[failInd][i]);
        }
        return false;
    }
    // public void generateResult(int resultIndex, int gamestateIndex) {
    //     Debug.Log(resultIndex + ", " + gamestateIndex);
    //     GameObject result = Instantiate(Event);
    //     string desc = myEvents.Results[resultIndex];
    //     result.GetComponentInChildren<Text>().text = desc;
    //     RectTransform descRT = result.transform.GetChild(0).GetComponent<RectTransform>();
    //     RectTransform rt = result.transform.GetChild(1).GetComponent<RectTransform>();
    //     rt.offsetMax = new Vector2(rt.offsetMax.x, rt.offsetMax.y+75);
    //     descRT.offsetMin = new Vector2(descRT.offsetMin.x, descRT.offsetMin.y+75);
    //     Button resultButton = Instantiate(button);
    //     resultButton.transform.SetParent(rt.transform, false);
    //     resultButton.transform.localPosition = new Vector3(0, -160);
    //     if (gamestateIndex==1) {
    //         resultButton.GetComponentInChildren<Text>().text = "Game Over";
    //         resultButton.onClick.AddListener(delegate{EndGame(1);});
    //     } else if (company.GetComponent<Company>().cash<0) {
    //         resultButton.GetComponentInChildren<Text>().text = "Bankrupt!";
    //         resultButton.onClick.AddListener(delegate{EndGame(2);});
    //     } else if (company.GetComponent<Company>().happiness<0) {
    //         resultButton.GetComponentInChildren<Text>().text = "Depression...";
    //         resultButton.onClick.AddListener(delegate{EndGame(3);});
    //     } else if (count>=19) {
    //         resultButton.GetComponentInChildren<Text>().text = "Congratulations!";
    //         resultButton.onClick.AddListener(delegate{EndGame(0);});
    //     } else {
    //         resultButton.GetComponentInChildren<Text>().text = "Continue";
    //         resultButton.onClick.AddListener(delegate{closeResult(result);});
    //     }
    //     result.transform.SetParent(canvas.transform, false);
    // }

    public void randomEmployees() {
        GameObject employees = company.transform.GetChild(0).gameObject;
        int empCount = company.transform.GetChild(0).childCount;
        randEmploy = new GameObject[empCount];
        int[] empInd = new int[empCount];
        int[] delta = new int[empCount];
        //Assigns each employee to a random index
        for (int i = 0; i < empCount; i++) {
            //In order to ensure we don't get duplicates, we need to always go from i, which is the value of the current index of the employee
            empInd[i] = (int)Random.Range(i, empCount);
            delta[i] = 0;
            //Compares our number against every other generated number, and lowers it by 1 for every number that's greater than or equal to itself
            for (int j = 0; j < i; j++) {
                if (empInd[i]<=(empInd[j]+delta[j])) {
                    delta[i]++;
                }
            }
            empInd[i]-=delta[i];
            //Assigns the employee at that index to an index in the random employee list
            randEmploy[i] = employees.transform.GetChild(empInd[i]).gameObject;
        }
    }
}
