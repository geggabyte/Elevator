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
    Animation anim;

    private void Start()
    {
        elevatorScript = elevator.GetComponent<ElevatorScript>();
        anim = this.GetComponent<Animation>();
    }

    public void TriggerElevator()
    {
        anim.Play();
        elevatorScript.setFloor(floor);
    }
}
