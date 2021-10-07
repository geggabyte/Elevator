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

    private void Start()
    {
        for(int i = 0; i < FloorNumber; i++)
        {
            _createdFloor = GameObject.Instantiate(FloorPrefab, new Vector3(0, FloorSpawnDistance * i, 0), Quaternion.identity);
            _createdFloor.GetComponent<FloorScript>().SetUp(i, Elevator);
        }
    }
}
