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

    private const int CASH_T1 = 10000;
    private const int CASH_T2 = 17500;
    private const int CASH_T3 = 25000;
    private const int HAPPINESS_T1 = 275;
    private const int HAPPINESS_T2 = 450;
    private const int HAPPINESS_T3 = 625;

    // Start is called before the first frame update
    void Start()
    {
        // Find the company object
        company = GameObject.Find("Company").GetComponent<Company>();

        int companyCash = company.cash;
        int companyHappiness = company.happiness;

        // Display end of game stats
        revenue.text = "$" + companyCash.ToString();
        happiness.text = companyHappiness.ToString();

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
        } else {
            // Stat-based endings
            if (companyCash <= CASH_T1) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = "Bad ending";
                    blurb.text = "You lost a lot of money and your employees were unhappy...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = "Bittersweet ending";
                    blurb.text = "You lost a lot of money, but your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = "People before profits ending";
                    blurb.text = "You lost a lot of money, but your employees loved working for you";
                }
            }   

            if (companyCash >= CASH_T1 && companyCash < CASH_T3) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = "Middling ending";
                    blurb.text = "You made decent money, but your employees were unhappy...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = "Neutral ending";
                    blurb.text = "You made decent money, and your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = "Good ending";
                    blurb.text = "You made decent money and your employees loved working for you";
                }
            }

            if (companyCash >= CASH_T3) {
                if (companyHappiness <= HAPPINESS_T1) {
                    resultHeader.text = "Profits before people ending";
                    blurb.text = "You made a lot of money, but at the expense of your employees...";
                }

                if (companyHappiness >= HAPPINESS_T1 && companyHappiness < HAPPINESS_T3) {
                    resultHeader.text = "Success story ending";
                    blurb.text = "You made a lot of money, and your employees were fairly happy";
                }

                if (companyHappiness >= HAPPINESS_T3) {
                    resultHeader.text = "Best ending";
                    blurb.text = "You made a lot of money and your employees loved working for you";
                }
            }
        }
    }
}
