using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FloorScript : MonoBehaviour
{
    [SerializeField]
    private Text FloorNumberText;

    [SerializeField]
    private Text FloorsNumberText;

    public int FloorNumber = -1;
    public GameObject Elevator;

    public void SetUp(int floor, GameObject elevator)
    {
        FloorNumber = floor;
        Elevator = elevator;
        FloorNumberText.text = floor.ToString();
    }

    public void TextSetUp(int floorsNumber)
    {
        if (FloorsNumberText == null) return;
        else
        {
            FloorsNumberText.text = floorsNumber.ToString();
        }
    }

}
