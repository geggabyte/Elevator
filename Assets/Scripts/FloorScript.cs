using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloorScript : MonoBehaviour
{
    [SerializeField]
    private Text FloorNumberText;

    public int FloorNumber = -1;
    public GameObject Elevator;

    public void SetUp(int floor, GameObject elevator)
    {
        FloorNumber = floor;
        Elevator = elevator;
        FloorNumberText.text = floor.ToString();
    }

}
