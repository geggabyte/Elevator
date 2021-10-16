using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Doors = new GameObject[4];


    [SerializeField]
    private float ElevatorDefaultSpeed = 1f, CurrentSpeed;


    private bool IsDoorsClosed = true, IsDoorLocked = false, IsGoing = false, isWaiting;
    private int DoorsNumber = 1, CurrentFlor = -1, CalledFloor = 0, TrigersCount = 0;
    private float time = 0, waitingTime = 3;


    private PlayerController playerController;
    private Transform ElevatorTransform;


    private void Start()
    {
        ElevatorTransform = this.GetComponent<Transform>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (isWaiting)
        {
            time += Time.deltaTime;
            if(time >= waitingTime)
            {
                isWaiting = false;
                CloseDoors();
            }
        }

        if (IsGoing && !IsDoorLocked)
        {
            if (CalledFloor < CurrentFlor) 
                CurrentSpeed = ElevatorDefaultSpeed * -1f;
            else CurrentSpeed = ElevatorDefaultSpeed;
        }
        if (IsDoorsClosed && IsGoing)
        {
            if (!IsDoorLocked) IsDoorLocked = true;

            ElevatorTransform.position = 
                new Vector3(ElevatorTransform.position.x, ElevatorTransform.position.y + CurrentSpeed * Time.deltaTime, ElevatorTransform.position.z);
        }
    }


    public void DoorStatusSet(bool status)
    {
        IsDoorsClosed = status;
    }


    public void FloorCall(int floor)
    {
        playerController.Log("Elevator: going to " + floor + " floor");

        if (floor == CurrentFlor)
            OpenDoors();

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
        isWaiting = true;
    }


    private void CloseDoors()
    {
        time = 0;
        foreach (GameObject a in Doors)
            if (a != null) a.GetComponent<DoorScript>().close();
    }


    private void OpenDoors()
    {
        if (IsDoorLocked) return;

        isWaiting = true;
        foreach(GameObject a in Doors)
            if(a != null)a.GetComponent<DoorScript>().open();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            if (DoorsNumber > 3 || other.gameObject.name == Doors[DoorsNumber].name) return;
            DoorsNumber++;

            if (DoorsNumber > 3) return;
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
                FinishTravel();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Door" && IsGoing)
            DoorsNumber = 1;

        if (other.tag == "Floor")
            TrigersCount = 0;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
            OpenDoors();
    }
}
