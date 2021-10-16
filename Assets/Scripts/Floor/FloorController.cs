using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    public int FloorNumber = 3;

    [SerializeField]
    private float FloorSpawnDistance = 4.55f;

    [SerializeField]
    private GameObject FloorPrefab, Elevator;

    private GameObject _createdFloor;

    private PlayerController player;

    private List<GameObject> floors = new List<GameObject>();

    private void Start()
    {
        for(int i = 0; i < FloorNumber; i++)
            InstanstiateFloor(i);

        FloorNumber--;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void ChangeFloorNumber(int value)
    {
        if(FloorNumber == 0 && value < 0)
        {
            player.Log("FloorController: 1 floor");
            return;
        }

        player.Log("FloorController: change floors number by " + value);
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
            a.GetComponent<FloorScript>().TextSetUp(FloorNumber + 1);
    }
}
