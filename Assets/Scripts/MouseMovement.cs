 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float XRotation = 0f;
    float YRotation = 0f;

    public float TopClamp = -90f;
    public float BottmClamp = 90f;

    void Start()
    {
        // lock the cursor to the middle of the screen and making it invisible
        Cursor.lockState = CursorLockMode.Locked;     
    }

    void Update()
    {
        // get the mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;// * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;// * Time.deltaTime;

        //rotation around the X axis (look up and down)
        XRotation -= mouseY;

        // clamp the rotation
        XRotation = Mathf.Clamp(XRotation, TopClamp, BottmClamp);

        //rotation around the Y axis (look left and right)
        YRotation += mouseX;

        // apply rotations to our transform
        transform.localRotation =Quaternion.Euler(XRotation, YRotation, 0f);




    }
}
