using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    int floor;

    ElevatorScript elevatorScript;
    Animation anim;

    private void Start()
    {
        floor = gameObject.transform.parent.parent.GetComponent<FloorScript>().FloorNumber;
        elevatorScript = gameObject.transform.parent.parent.GetComponent<FloorScript>().Elevator.GetComponent<ElevatorScript>();
        anim = this.GetComponent<Animation>();
    }

    public void Trigger()
    {
        anim.Play();
        elevatorScript.FloorCall(floor);
    }
}
