using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    public Text resultHeader;
    public Text blurb;
    public Text revenue;
    public Text happiness;

    private Company company;

    // Start is called before the first frame update
    void Start()
    {
        // Find the company object
        company = GameObject.Find("Company").GetComponent<Company>();;

        // Display end of game stats
        revenue.text = company.cash.ToString();; 
        happiness.text = company.happiness.ToString();;

        // Determine result
        resultHeader.text = "Result 1";
        blurb.text = "You got the best ending!";
    }
}
