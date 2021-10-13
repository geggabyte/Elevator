using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    // 0 FLOOR IS 1.3 BY Y

    [SerializeField]
    private GameObject[] Doors = new GameObject[4];

    [SerializeField]
    private int DoorsNumber = 1, CurrentFlor = 0, CalledFloor = 0, TrigersCount = 0;

    [SerializeField]
    private float ElevatorDefaultSpeed = 1f, CurrentSpeed;

    [SerializeField]
    bool IsDoorsClosed = true, IsDoorLocked = false, IsGoing = false;

    private Transform ElevatorTransform;

    private void Start()
    {
        ElevatorTransform = this.GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsGoing && !IsDoorLocked)
        {
            if (CalledFloor < CurrentFlor)
            {
                CurrentSpeed = ElevatorDefaultSpeed * -1f;
            }
            else CurrentSpeed = ElevatorDefaultSpeed;
        }
        if (IsDoorsClosed && IsGoing)
        {
            if (!IsDoorLocked) IsDoorLocked = true;
            ElevatorTransform.position = new Vector3(ElevatorTransform.position.x, ElevatorTransform.position.y + CurrentSpeed * Time.deltaTime, ElevatorTransform.position.z);
        }
    }

    public void DoorStatusSet(bool status)
    {
        IsDoorsClosed = status;
    }

    public void FloorCall(int floor)
    {
        Debug.LogFormat("Elevator: {0} floor call recieved", floor);
        if (floor == CurrentFlor)
        {
            OpenDoors();
        }
        else
        {
            CalledFloor = floor;
            CloseDoors();
            IsGoing = true;
        }
    }

    private void FinishTravel()
    {
        IsGoing = false;
        IsDoorLocked = false;
        OpenDoors();
    }

    private void CloseDoors()
    {
        foreach (GameObject a in Doors)
        {
            if (a != null) a.GetComponent<DoorScript>().close();
        }
    }

    private void OpenDoors()
    {
        if (IsDoorLocked) return;
        foreach(GameObject a in Doors)
        {
            if(a != null)a.GetComponent<DoorScript>().open();
        }
    }

    #region OnTrigger

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            if (other.gameObject.name == Doors[DoorsNumber].name) return;
            Debug.LogFormat("Elevator: door {0} founded", other.gameObject.name);
            DoorsNumber++;
            Doors[DoorsNumber] = other.gameObject;
        }
        if(other.tag == "Floor")
        {
            int floor = other.gameObject.transform.parent.GetComponent<FloorScript>().FloorNumber;
            if (floor != CurrentFlor && TrigersCount == 0)
            {
                TrigersCount = 1;
                CurrentFlor = floor;
            }

            else if(floor == CalledFloor && CalledFloor == CurrentFlor)
            {
                FinishTravel();
            }
            Debug.LogFormat("Elevator: touched {0} floor", floor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Door" && IsGoing)
        {
            Debug.Log("Elevator: doors exited");
            DoorsNumber = 1;
        }
        if (other.tag == "Floor")
        {
            TrigersCount = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            OpenDoors();
            Debug.Log("Elevator: Player in doors");
        }
    }

    #endregion
}
