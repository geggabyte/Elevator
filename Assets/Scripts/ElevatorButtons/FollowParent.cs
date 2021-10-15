using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    public GameObject parent;

    float  YOffset = 0.2f, YOffsetForControllPanel = 1.495f;

    private void Start()
    {
        parent = this.transform.parent.gameObject;
        this.transform.SetParent(null);
        this.GetComponent<ElevatorPanel>().Instantiate();
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, parent.transform.position.y + YOffset - YOffsetForControllPanel, this.transform.position.z);
    }
}
