using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    ElevatorPanel panel;
    Animation animationController;

    private void Start()
    {
        panel = this.transform.parent.parent.GetComponent<ElevatorPanel>();
        animationController = this.GetComponent<Animation>();
    }

    public void Trigger()
    {
        animationController.Play();
        panel.CallFloor();
    }
}
