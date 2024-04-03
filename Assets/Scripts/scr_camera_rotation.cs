using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 10000f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;
    public float inputX = 0f;
    public float inputY = 0f;

    bool lockedCursor = true;

    // Start is called before the first frame update
    void Start()
    {
        //Lock and hide Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Collect Mouse Input
        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        //Rotate Camera around its local x-axis
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        //rotate the player object and the camera around its Y axis
        player.Rotate(Vector3.up * inputX);

    }
}