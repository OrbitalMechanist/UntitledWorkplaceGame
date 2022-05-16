using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireButtonFunc : MonoBehaviour
{
    [SerializeField] GameObject canvasInstance;
    [SerializeField] GameObject hireScreenPrefab;

    public void CreateHireScreen()
    {
        Instantiate(hireScreenPrefab).transform.SetParent(canvasInstance.transform, false);
        Time.timeScale = 0;
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
