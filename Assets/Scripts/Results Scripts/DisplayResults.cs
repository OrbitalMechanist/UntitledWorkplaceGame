using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResults : MonoBehaviour
{
    // UI text elements
    public Text resultHeader;
    public Text blurb;
    public Text revenue;
    public Text happiness;
    public Text size;

    // The company object
    private Company company;
    
    // Cash value tiers
    private const int CASH_T1 = 10000;
    private const int CASH_T2 = 20000; // Not currently in use
    private const int CASH_T3 = 35000;

    // Happiness value tiers
    private const int HAPPINESS_T1 = 500;
    private const int HAPPINESS_T2 = 800; // Not currently in use
    private const int HAPPINESS_T3 = 1000;

    // Size value tiers
    private const int SIZE_T1 = 90;
    private const int SIZE_T2 = 100; // Not currently in use
    private const int SIZE_T3 = 120;

    void Start()
    {
        // Find the company object
        company = GameObject.Find("Company").GetComponent<Company>();
        
        // Get company stats
        int companyCash = company.cash;
        int companyHappiness = company.happiness;
        int companySize = company.companySize;

        // Display end of game stats
        revenue.text = "$" + companyCash.ToString();
        happiness.text = companyHappiness.ToString();
        size.text = companySize.ToString();

        // Determine result
        // Choice-based endings
        if (company.endState == 1) {
            resultHeader.text = "Wrong choice ending";
            blurb.text = "You made the wrong choice and went out of business...";
        } else if (company.endState == 2) {
            resultHeader.text = "Bankruptcy ending";
            blurb.text = "Your company ran out of money and went out of business...";
        } else if (company.endState == 3) {
            resultHeader.text = "Depression ending";
            blurb.text = "Your employees were so unhappy they all quit together, and your reputation has made finding new people impossible...";
        // Stat-based endings
        } else {
            // Determine company size result
            string sizeResult = "";
            if (companySize <= SIZE_T1) {
                sizeResult = "Small company, ";
            } else if (companySize >= SIZE_T1 && companySize < SIZE_T3) {
                sizeResult = "Medium company, ";
            } else if (companySize >= SIZE_T3) {
                sizeResult = "Large company, ";
            }

            // Determine money + happiness result, combine with size result and display
            if (companyCash <= CASH_T1) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = sizeResult + "bad ending";
                    blurb.text = "You lost a lot of money and your employees were unhappy...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "bittersweet ending";
                    blurb.text = "You lost a lot of money, but your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "people before profits ending";
                    blurb.text = "You lost a lot of money, but your employees loved working for you";
                }
            }   

            if (companyCash >= CASH_T1 && companyCash < CASH_T3) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = sizeResult + "middling ending";
                    blurb.text = "You made decent money, but your employees were unhappy...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "neutral ending";
                    blurb.text = "You made decent money, and your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "good ending";
                    blurb.text = "You made decent money and your employees loved working for you";
                }
            }

            if (companyCash >= CASH_T3) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = sizeResult + "profits before people ending";
                    blurb.text = "You made a lot of money, but at the expense of your employees...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "success story ending";
                    blurb.text = "You made a lot of money, and your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = sizeResult + "best ending";
                    blurb.text = "You made a lot of money and your employees loved working for you";
                }
            }
        }
    }
}
