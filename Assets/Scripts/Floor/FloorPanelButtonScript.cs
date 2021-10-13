using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPanelButtonScript : MonoBehaviour
{
    [SerializeField]
    int value = 1;

    [SerializeField]
    Text text;

    FloorController floorController;
    Animation animationController;

    private void Start()
    {
        animationController = this.gameObject.GetComponent<Animation>();
        floorController = this.transform.parent.parent.parent.GetComponent<FloorController>();
    }

    public void Trigger()
    {
        animationController.Play();
        floorController.ChangeFloorNumber(value);
    }
}
