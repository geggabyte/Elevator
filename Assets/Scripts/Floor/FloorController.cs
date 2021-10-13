using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField]
    private int FloorNumber = 3;

    [SerializeField]
    private float FloorSpawnDistance = 4.55f;

    [SerializeField]
    private GameObject FloorPrefab, Elevator;

    private GameObject _createdFloor;

    private List<GameObject> floors = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < FloorNumber; i++)
        {
            InstanstiateFloor(i);
        }
        FloorNumber--;
    }

    public void ChangeFloorNumber(int value)
    {
        if(FloorNumber == 0 && value < 0)
        {
            Debug.Log("FloorController: 0 floors");
            return;
        }

        Debug.LogFormat("FloorController: change floors number by {0}", value);
        if(value < 0)
        {
            Destroy(floors[FloorNumber]);
            floors.RemoveAt(FloorNumber);
            FloorNumber--;
            ChangeNumbers();
        }
        else
        {
            FloorNumber++;
            InstanstiateFloor(FloorNumber);
        }
    }

    private void InstanstiateFloor(int number)
    {
        _createdFloor = GameObject.Instantiate(FloorPrefab, new Vector3(0, FloorSpawnDistance * number, 0), Quaternion.identity);
        _createdFloor.transform.SetParent(this.gameObject.transform, true);

        _createdFloor.GetComponent<FloorScript>().SetUp(number, Elevator);
        _createdFloor.name = number.ToString();

        floors.Add(_createdFloor.gameObject);

        ChangeNumbers();
    }

    private void ChangeNumbers()
    {
        foreach (GameObject a in floors)
        {
            a.GetComponent<FloorScript>().TextSetUp(FloorNumber + 1);
        }
    }
}
