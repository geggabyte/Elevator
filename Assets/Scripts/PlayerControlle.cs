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

    [SerializeField]
    GameObject LookingAtObject;

    float horizontal, vertical, mouseX, mouseY;
    bool isButton = false, getInput = true;
    int layer_mask;
    Vector3 forward, right, cameraRayVector;
    Camera cameraObject;
    RaycastHit hit;
    Ray ray;


    void Start()
    {
        cameraTransform = gameObject.transform.GetChild(0);
        cameraObject = cameraTransform.GetComponent<Camera>();
        cameraRayVector = new Vector3(Screen.width/2, Screen.height/2, 0);
        layer_mask = LayerMask.GetMask("Elevator");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (getInput)
        {
            Movement();
        }

        Raycasting();
        if (isButton && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hit.transform.gameObject.GetComponent<ButtonScript>() != null) { hit.transform.gameObject.GetComponent<ButtonScript>().Trigger(); }
            else if (hit.transform.gameObject.GetComponent<FloorPanelButtonScript>() != null) hit.transform.gameObject.GetComponent<FloorPanelButtonScript>().Trigger();
            else if (hit.transform.gameObject.GetComponent<PanelButtonScript>() != null) hit.transform.gameObject.GetComponent<PanelButtonScript>().Trigger();
            else hit.transform.gameObject.GetComponent<ElevatorButton>().Trigger();
        }
        if (Input.GetKeyDown(KeyCode.Q)) { transform.position = new Vector3(transform.position.x, transform.position.y - 4f, transform.position.z); }
        if (Input.GetKeyDown(KeyCode.E)) { transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z); }
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            getInput = !getInput;
        }
    }

    private void Raycasting()
    {
        cursor.texture = defaultCursor;
        isButton = false;
        ray = cameraObject.ScreenPointToRay(cameraRayVector);
        if(Physics.Raycast(ray, out hit, distance, ~layer_mask))
        {
            //Debug.LogFormat("PlayerController: hited {0}", hit.transform.tag);
            if (hit.transform.tag == "Button")
            {
                cursor.texture = interactCursor;
                isButton = true;
            }

            LookingAtObject = hit.transform.gameObject;
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
