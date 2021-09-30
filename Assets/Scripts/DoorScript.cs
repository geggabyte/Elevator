using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField]
    bool left = false;

    [SerializeField]
    float openPos = 1.05f, closePos = 0.39f, animationSpeed = 0.01f;


    private bool opening = false, closing = false, isOpen = false;
    private AudioSource audioPlayer;
    private ElevatorScript elevator;
    
    private void Start()
    {
        elevator = transform.parent.GetComponent<ElevatorScript>();
        if(transform.childCount > 0)
            audioPlayer =  transform.GetChild(0).gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (opening)
        {
            if (left)
            {
                if (transform.position.x < openPos)
                    transform.position = new Vector3(transform.position.x + animationSpeed, transform.position.y, transform.position.z);
                else
                {
                    opening = false;
                    if (audioPlayer) audioPlayer.Stop();
                }
            }
            else
            {
                if (transform.position.x > openPos * -1)
                    transform.position = new Vector3(transform.position.x - animationSpeed, transform.position.y, transform.position.z);
                else opening = false;
            }
        }
        if (closing)
        {
            if (left)
            {
                if (transform.position.x > closePos)
                    transform.position = new Vector3(transform.position.x - animationSpeed, transform.position.y, transform.position.z);
                else 
                {
                    closing = false;
                    if (audioPlayer) audioPlayer.Stop();
                }
            }
            else
            {
                if (transform.position.x < closePos * -1)
                    transform.position = new Vector3(transform.position.x + animationSpeed, transform.position.y, transform.position.z);
                else closing = false;
            }
        }
    }

    public void open()
    {
        if (!opening && !isOpen)
        {
            opening = true;
            closing = false;
            if (audioPlayer) audioPlayer.Play();
            Debug.Log("Open");
            isOpen = true;
        }
    }

    public void close()
    {
        if (!closing && isOpen)
        {
            closing = true;
            opening = false;
            if (audioPlayer) audioPlayer.Play();
            Debug.Log("Close");
            isOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(elevator != null && other.tag == "Player")
        {
            elevator.PlayerInDoors(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (elevator != null && other.tag == "Player")
        {
            elevator.PlayerInDoors(false);
        }
    }
}
