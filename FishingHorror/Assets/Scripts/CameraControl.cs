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

    //value to get camera's transform
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        //initialize rotation
        cameraTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //set vertical mouse movement
        cameraTransform.Rotate((-Input.GetAxis("Mouse Y") + yClampMin) * mouseSensitivity, 0, 0);

        //set horizontal mouse movement
        cameraTransform.Rotate(0, (Input.GetAxis("Mouse X") + xClampMin) * mouseSensitivity, 0);

        //clamp rotation
        cameraTransform.rotation = Quaternion.Euler(Mathf.Clamp(cameraTransform.rotation.x, xClampMin, xClampMax), Mathf.Clamp(cameraTransform.rotation.y, yClampMin, yClampMax), 0f);

        //debug euler angles
        Debug.Log(cameraTransform.rotation);
        Debug.Log("mouse y:" + Input.GetAxis("Mouse Y"));
        Debug.Log("mouse x:" + Input.GetAxis("Mouse X"));
    }
}
