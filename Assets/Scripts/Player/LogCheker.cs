using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCheker : MonoBehaviour
{
    Text text;

    float time = 0, waitingTime = 3;
    bool isWaiting = false;

    private void Start()
    {
        text = this.GetComponent<Text>();
    }

    private void Update()
    {
        if (!isWaiting && text.text != null) isWaiting = true;
        if (isWaiting)
        {
            time += Time.deltaTime;
        }
        if (time >= waitingTime)
        {
            isWaiting = false;
            time = 0;
            text.text = null;
        }
    }
}
