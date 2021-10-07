using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    // 0 FLOOR IS 1.3 BY Y
    //TRIGERS DOESN"T WORK WITHOUT RIGIDBODY

    [SerializeField]
    private GameObject[] Doors = new GameObject[4];

    [SerializeField]
    private int DoorsNumber = 2, CurrentFlor = 0, CalledFloor = 0, TrigersCount = 0;

    [SerializeField]
    private float ElevatorSpeed = 1f;

    [SerializeField]
    bool IsDoorsClosed = true, IsDoorLocked = false;

    private Transform ElevatorTransform;

    private void Start()
    {
        ElevatorTransform = this.GetComponent<Transform>();
    }

    private void Update()
    {
        if(TrigersCount >= 2 && CurrentFlor == CalledFloor)
        {
            FinishTravel();
        }
        if (IsDoorsClosed && CurrentFlor != CalledFloor)
        {
            if (!IsDoorLocked) IsDoorLocked = true;
            ElevatorTransform.position = new Vector3(ElevatorTransform.position.x, ElevatorTransform.position.y + ElevatorSpeed * Time.deltaTime, ElevatorTransform.position.z);
        }
    }

    public void DoorStatusSet(bool status)
    {
        IsDoorsClosed = status;
    }

    public void FloorCall(int floor)
    {
        Debug.LogFormat("Elevator: floor called {0}", floor);
        if (floor == CurrentFlor)
        {
            OpenDoors();
        }
        else
        {
            CalledFloor = floor;
            CloseDoors();
        }
    }

    private void FinishTravel()
    {
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
            Doors[DoorsNumber] = other.gameObject;
            DoorsNumber++;
        }
        if(other.tag == "Floor")
        {
            TrigersCount++;
            CurrentFlor = other.gameObject.transform.parent.GetComponent<FloorScript>().FloorNumber;
            Debug.LogFormat("Elevator: touched {0}", CurrentFlor);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Door")
        {
            Doors[DoorsNumber-1] = null;
            DoorsNumber--;
        }
        if (other.tag == "Floor")
        {
            TrigersCount--;
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
