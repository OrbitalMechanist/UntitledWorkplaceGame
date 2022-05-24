using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DisplayMinigameResults : MonoBehaviour
{
    /** The text object to display the result text. */
    public Text resultHeader;

    /** The text object to display the blurb about winning the minigame. */
    public Text blurb;

    /** The text object to display the rewards for completing the minigame. */
    public Text rewards;

    /** The object that holds the employees. */
    public GameObject employeeManagerInstance; 

    /** The minimum stat for an employee. */
    private int MIN_EMP_STAT = 0;

    /** The maximum stat for an employee. */
    private int MAX_EMP_STAT = 256;

    /** The text to display as the reward. */
    private string rewardText;

    public void BoostEmployeeWorkEthic(int statIncrease, string resultHeaderText, string blurbText) {
        // Find the employee manager
        employeeManagerInstance = GameObject.Find("employeeOwner");

        // Initialize reward text for use
        rewardText = "";

        // Get number of employees
        int numEmployees = employeeManagerInstance.transform.childCount;

        // Display all employees in list panel
        for (int i = 0; i < numEmployees; i++)
        {
            // Get the employee of the current idnex
            GameObject emp = employeeManagerInstance.transform.GetChild(i).gameObject;

            // Add stat increase to employee, clamped between 0 and 255
            emp.GetComponent<Employee>().ethic = Mathf.Clamp(statIncrease + emp.GetComponent<Employee>().ethic, MIN_EMP_STAT, MAX_EMP_STAT);

            // Add the employee to the reward text for the results screen
            rewardText += emp.GetComponent<Employee>().fName + " " + emp.GetComponent<Employee>().lName + ": <color=green>+" + statIncrease + "</color> Work Ethic\n";
        }

        DisplayResults(resultHeaderText, blurbText);
    }

    private void DisplayResults(string resultHeaderText, string blurbText) {
        resultHeader.text = resultHeaderText;
        blurb.text = blurbText;
        rewards.text = rewardText;
    }
}
