using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeSelectionHandler : MonoBehaviour
{
    //WARNING: This script is hardcoded to the extreme and relies on a certain structure.
    //Intended to be used exclusively in the ForminStage scene.

    //The employeeOwner must contain the employees up for selection as its first children.
    public GameObject employeeOwnerInstance;

    //The employeeItemContainer contains nothing but EmployeePanel items,
    //in the order that they are stored in the employeeOwner.
    //The EmployeePanel must have a Toggle as its child with the index 8.
    public GameObject employeeItemContainerInstance;

    //This function serves to strip out unselected employees before
    //their container is handed over to SwitchToScenePreservingItem.
    public void cleanEmployeeObjectsBySelection()
    {
        LinkedList<int> selectedIndeces = new LinkedList<int>();
        int numEmployees = employeeItemContainerInstance.transform.childCount;
        for(int i = 0; i < numEmployees; i++)
        {
            if(employeeItemContainerInstance.transform.GetChild(i).GetChild(8).gameObject.GetComponent<Toggle>().isOn)
            {
                selectedIndeces.AddFirst(i);
            }
        }

        //Doing this in reverse is necessary so that deleting objects doesn't affect the index
        //of the ones we deal with later on
        for (int i = numEmployees - 1; i >= 0; i--)
        {
            if(selectedIndeces.First.Value != i)
            {
                Destroy(employeeOwnerInstance.transform.GetChild(i).gameObject);
            } else
            {
                selectedIndeces.RemoveFirst();
            }
        }

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
