using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    FloorController floorController;


    ElevatorScript elevator;
    PlayerController player;


    [SerializeField]
    GameObject parent;


    float YOffset = 0.2f, YOffsetForControllPanel = 1.495f;
    int floor = 0, maxFloor = 3;


    private void Start()
    {
        parent = this.transform.parent.gameObject;
        this.transform.SetParent(null);

        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        elevator = parent.GetComponent<ElevatorScript>();
    }


    private void LateUpdate()
    {
        text.text = floor.ToString();
        maxFloor = floorController.FloorNumber;
        this.transform.position = new Vector3(this.transform.position.x, parent.transform.position.y + YOffset - YOffsetForControllPanel, this.transform.position.z);
    }


    public void ChangeFloor(int value)
    {
        if(floor == 0 && value < 0)
        {
            player.Log("This is lowes floor");
            return;
        }
        if(floor+1 == maxFloor && value > 0)
        {
            player.Log("This is highest floor");
            return;
        }
        floor += value;
    }


    public void CallFloor()
    {
        elevator.FloorCall(floor);
    }
}
