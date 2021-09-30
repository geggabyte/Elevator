using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlle : MonoBehaviour
{
    Transform cameraTransform;

    [SerializeField]
    float mouseSensitivity = 0.2f, movementSpeed = 2, minY = -45, maxY = 45, distance = 1f;
    
    [SerializeField]
    Texture defaultCursor, interactCursor;

    [SerializeField]
    RawImage cursor;


    float horizontal, vertical, mouseX, mouseY;
    bool isButton = false;
    Vector3 forward, right, cameraRayVector;
    Camera cameraObject;
    RaycastHit hit;
    Ray ray;


    void Start()
    {
        cameraTransform = gameObject.transform.GetChild(0);
        cameraObject = cameraTransform.GetComponent<Camera>();
        cameraRayVector = new Vector3(Screen.width/2, Screen.height/2, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        Raycasting();
        if (isButton && Input.GetKeyDown(KeyCode.Mouse0)) hit.transform.gameObject.GetComponent<ButtonScript>().TriggerElevator();
    }

    private void Raycasting()
    {
        cursor.texture = defaultCursor;
        isButton = false;
        ray = cameraObject.ScreenPointToRay(cameraRayVector);
        if(Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Button")
            {
                if ((transform.position - hit.transform.position).magnitude <= distance)
                {
                    cursor.texture = interactCursor;
                    isButton = true;
                }
            }
        }
    }

    private void Movement()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mouseY = Mathf.Clamp(mouseY, minY, maxY);

        vertical = Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;

        forward = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z) * vertical;
        right = new Vector3(cameraTransform.right.x, 0, cameraTransform.right.z) * horizontal;

        transform.position += forward;
        transform.position += right;

        transform.Rotate(Vector3.up, mouseX);
        cameraTransform.localRotation = Quaternion.Euler(mouseY, 0, 0);
    }
}
