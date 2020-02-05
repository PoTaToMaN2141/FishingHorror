using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    

    //values for clamping the camera's x and y values
    private float xClampMin = -85f;
    private float xClampMax = 55f;
    private float yClampMin = -140f;
    private float yClampMax = 140f;

    //float for mouse sensitivity
    [SerializeField]
    private float mouseSensitivity;

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

        //calculate vertical mouse movement
        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, xClampMin, xClampMax);

        //calculate horizontal mouse movement if camera is restricted
        if(SetPlayerState.instance.playerState != PlayerState.Walking)
        {
            //clamp horizontal camera rotation
            yRot -= mouseX;
            yRot = Mathf.Clamp(yRot, yClampMin, yClampMax);
        }
        else
        {
            //allow full camera and body rotation on the horizontal axis
            WorldManager.instance.player.transform.Rotate(Vector3.up * mouseX);
        }

        //apply rotation
        transform.localRotation = Quaternion.Euler(xRot, -yRot, 0f);
    }
}
