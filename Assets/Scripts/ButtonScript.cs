using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    int floor;

    [SerializeField]
    GameObject elevator;

    ElevatorScript elevatorScript;

    private void Start()
    {
        elevatorScript = elevator.GetComponent<ElevatorScript>();
    }

    public void TriggerElevator()
    {
        elevatorScript.setFloor(floor);
    }
}
