using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    private float OpenPosition = 1.05f, ClosePosition = 0.4f, DoorSpeed = 1f;

    [SerializeField]
    private bool isLeft = true, isOpen = false, isClose = true;

    private ElevatorScript elevator;
    private Transform door;

    private void Start()
    {
        elevator = gameObject.transform.parent.GetComponent<ElevatorScript>();
        if (elevator == null) elevator = gameObject.transform.parent.GetComponent<FloorScript>().Elevator.GetComponent<ElevatorScript>();

        door = this.GetComponent<Transform>();
        if (!isLeft)
        {
            OpenPosition *= -1;
            ClosePosition *= -1;
            DoorSpeed *= -1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) open();
        if (Input.GetKeyDown(KeyCode.E)) close();

        if (isOpen)
        {
            if ((door.position.x < OpenPosition && isLeft) || (door.position.x > OpenPosition && !isLeft))
            {
                door.position = new Vector3(
                    door.position.x + DoorSpeed * Time.deltaTime, door.position.y, door.position.z);
            }
            else
            {
                isOpen = false;
            }
        }
        if(isClose)
        {
            if ((door.position.x > ClosePosition && isLeft) || (door.position.x < ClosePosition && !isLeft))
            {
                door.position = new Vector3(
                    door.position.x - DoorSpeed * Time.deltaTime, door.position.y, door.position.z);
            }
            else
            {
                isClose = false;
                elevator.DoorStatusSet(true);
            }
        }
    }

    public void open()
    {
        Debug.Log("Door: opening");
        elevator.DoorStatusSet(false);
        isClose = false;
        isOpen = true;
    }

    public void close()
    {
        Debug.Log("Door: closing");
        isOpen = false;
        isClose = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(elevator != null && other.tag == "Player")
        {

        }
    }
}
