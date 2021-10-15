using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    Text text;

    int floor = 0;
    ElevatorScript elevator;


     public void Instantiate()
    {
        elevator = this.GetComponent<FollowParent>().parent.GetComponent<ElevatorScript>();
        Debug.Log(elevator);    
    }

    private void LateUpdate()
    {
        text.text = floor.ToString();
    }

    public void ChangeFloor(int value)
    {
        if(floor == 0 && value < 0)
        {
            Debug.Log("This is lowes floor");
            return;
        }
        floor += value;
    }

    public void CallFloor()
    {
        elevator.FloorCall(floor);
    }
}
