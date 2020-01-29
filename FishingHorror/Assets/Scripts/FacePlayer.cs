using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    //field for reference to player camera
    //TODO: replace with global reference to player if created later
    [SerializeField]
    private GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get desired vector from object to player 
        Vector3 desiredVector = transform.position - playerCamera.transform.position;

        //set forward vector to desired vector
        transform.forward = desiredVector;
    }
}
