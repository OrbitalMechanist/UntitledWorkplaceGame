using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SalarySlider : MonoBehaviour
{

    public Slider slider;

    public GameObject textInstance;

    public GameObject empGeneratorContainerInstance;

    public void SalaryUpdate()
    {
        textInstance.GetComponent<Text>().text = "Base Salary: $" + (int)slider.value;
        empGeneratorContainerInstance.GetComponent<EmployeeGenerator>().baseSalary = (int)slider.value;
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
