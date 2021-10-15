using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonScript : MonoBehaviour
{
    [SerializeField]
    int value = 1;

    ElevatorPanel panel;
    Animation animationController;

    private void Start()
    {
        animationController = this.gameObject.GetComponent<Animation>();
        panel = this.transform.parent.GetComponent<ElevatorPanel>();
    }

    public void Trigger()
    {
        animationController.Play();
        panel.ChangeFloor(value);
    }
}
