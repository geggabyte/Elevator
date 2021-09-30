using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    

    [SerializeField]
    AudioSource audioPlayer;

    [SerializeField]
    AudioClip elevatorStartSound, elevatorRideSound, elevatorStopSound;

    [SerializeField]
    float flootHeight = 4, elevatorSpeed = 1f, timeOut = 7;

    [SerializeField]
    GameObject[] elevatorDors = new GameObject[4];

    GameObject player = null;
    bool isDorsOpened = false, call = false, isWaiting = false, isWay = false, goingUp = true, isRiding = false, isStoping = false, wasRiding = false, wasStoping = false, isPlayerInDoors = false;
    int calledFloor = 1, currentFloor = 1, doors = 2;
    float timer = 0, way, wayEnded;


    
    void LateUpdate()
    {
        if (calledFloor != currentFloor) call = true;
        if (calledFloor == currentFloor && !isDorsOpened  && !call && timer < timeOut) openDoors();
        if (isDorsOpened) isWaiting = true;
        if (timer >= timeOut && isDorsOpened) {
            closeDoors();
            isWaiting = false;
        }

        if (isPlayerInDoors && !call && isDorsOpened) timer = 0;

        if (call && !isPlayerInDoors) closeDoors();

        if (call)
        {
           if (isPlayerInDoors && timer <= 1)
           {
                isWay = false;
                timer = 0;
           }
           if (!isWay){
                isWay = true;
                isWaiting = true;
                way = calledFloor * flootHeight - transform.position.y;
                wayEnded = 0;
                if (way > 0) goingUp = true;
                else goingUp = false;
                audioPlayer.clip = elevatorStartSound;
                audioPlayer.PlayDelayed(2);
            }
            if (timer >= 2)
            {
                if (goingUp && way > wayEnded)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + elevatorSpeed * Time.deltaTime, transform.position.z);
                    if (player) player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + elevatorSpeed * Time.deltaTime, player.transform.position.z);
                    wayEnded += elevatorSpeed * Time.deltaTime;

                    if (way - wayEnded + 2 <= way && !wasRiding)
                    {
                        isRiding = true;
                    }
                    else if (way - wayEnded <= 2 && !wasStoping)
                    {
                        isStoping = true;
                    }
                }
                else if (goingUp && way <= wayEnded)
                    wayEnd();

                if (!goingUp && way < wayEnded)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - elevatorSpeed * Time.deltaTime, transform.position.z);
                    if (player) player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - elevatorSpeed * Time.deltaTime, player.transform.position.z);
                    wayEnded -= elevatorSpeed * Time.deltaTime;
                    if (way - wayEnded > way + 2 && !wasRiding)
                    {
                        isRiding = true;
                    }
                    else if (way - wayEnded >= -2 && !wasStoping)
                    {
                        isStoping = true;
                    }
                }
                else if (!goingUp && way >= wayEnded)
                    wayEnd();

                if (isStoping)
                {
                    audioPlayer.clip = elevatorStopSound;
                    audioPlayer.Play();
                    isStoping = false;
                    wasStoping = true;
                }
                else if (isRiding)
                {
                    audioPlayer.clip = elevatorRideSound;
                    audioPlayer.Play();
                    isRiding = false;
                    wasRiding = true;
                }
                currentFloor = (int)Mathf.Floor(transform.position.y / 4) + 1;
            }
            
        }

        if (isWaiting)
            timer += Time.deltaTime;

    }

    private void wayEnd()
    {
        isWaiting = true;
        wasStoping = false;
        wasRiding = false;
        isWay = false;
        call = false;
        currentFloor = calledFloor;
        wayEnded = 0;
        timer = 0;
        openDoors();
    }

    public void setFloor(int x)
    {
        if (x != currentFloor)
        {
            calledFloor = x;
            timer = 0;
            isDorsOpened = false;
            call = false;
            Debug.Log("floor call: " + x);
        }
        else timer = 0;
    }

    private void openDoors()
    {
        foreach (GameObject door in elevatorDors)
        {
            door.GetComponent<DoorScript>().open();
        }
        isDorsOpened = true;
        audioPlayer.Stop();
    }

    private void closeDoors()
    {
        foreach(GameObject door in elevatorDors)
        {
            door.GetComponent<DoorScript>().close();
        }
        isDorsOpened = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Door")
        {
            elevatorDors[doors] = other.gameObject;
            doors++;
        }

        if (other.tag == "Player")
        {
            player = other.gameObject;
            Debug.Log("Player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Door") doors--;

        if (other.tag == "Player")
        {
            player = null;
            Debug.Log("Player undetected");
        }
    }


    public void PlayerInDoors(bool a)
    {
        isPlayerInDoors = a;
    }

}
