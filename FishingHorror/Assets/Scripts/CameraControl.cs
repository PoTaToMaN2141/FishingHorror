using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //values for clamping the camera's x and y values
    private float xClampMin = 45f;
    private float xClampMax = 270f;
    private float yClampMin = 140f;
    private float yClampMax = 220f;

    //float for mouse sensitivity
    [SerializeField]
    private float mouseSensitivity;

    //field for player's transform
    [SerializeField]
    private Transform player;

    //fields to store final x and y rotation values
    private float xRot = 0f;
    private float yRot = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Move cursor lock to a game state manager later
        //lock cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //store mouse x and y axis movement and adjust for mouse sensitivity and framerate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //calculate mouse movement
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -85f, 55f);
        yRot -= mouseX;
        yRot = Mathf.Clamp(yRot, -140f, 130f);
        transform.localRotation = Quaternion.Euler(xRot, -yRot, 0f);

        //debug x rotation and y rotation
        //Debug.Log("Y rotation: " + xRot + "\nX rotation: " + yRot);
    }
}
