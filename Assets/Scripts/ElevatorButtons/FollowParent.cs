using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    [SerializeField]
    bool IsControllPanel = false;

    public GameObject parent;

    float  YOffset = 0.2f, YOffsetForControllPanel = 1.495f;

    private void Start()
    {
        parent = this.transform.parent.gameObject;
        this.transform.SetParent(null);
    }

    private void LateUpdate()
    {
        if (parent.transform.position.y != this.transform.position.y)
            if(IsControllPanel) this.transform.position = new Vector3(this.transform.position.x, parent.transform.position.y + YOffset - YOffsetForControllPanel, this.transform.position.z);
            else this.transform.position = new Vector3(this.transform.position.x, parent.transform.position.y + YOffset, this.transform.position.z);
    }
}
