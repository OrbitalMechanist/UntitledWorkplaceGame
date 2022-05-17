using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalarySliderToGeneratorPassing : MonoBehaviour
{

    //This script is for use within the performing stage.
    //It will find a slider called salarySlider and pass its to
    //an EmployeeGenerator component in the same gameobject as itself
    //to serve as the base salary.

    //This will fire before any starts will. It is important because we want the base salary
    //to be there by the time EmployeeGenerator fires its start.
    private void Awake()
    {
        gameObject.GetComponent<EmployeeGenerator>().baseSalary = (int)GameObject.Find("salarySlider").GetComponent<Slider>().value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
