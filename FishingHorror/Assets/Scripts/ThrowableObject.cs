using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : InteractableObject
{
    //field for a bool to check if the object is throwable
    private bool isThrowable = false;

    //field for the object's rigidbody
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        //save reference to object's rigidbody
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check if the player is already holding something

        //check if the object is throwable and then check for input
        if(isThrowable == true)
        {
            //match object's transform to the player's "hand" in the item space and parent it to the camera (or the player depending on how the viewmodel will work)
            transform.position = WorldManager.instance.playerCamera.transform.position;
            transform.rotation = WorldManager.instance.playerCamera.transform.rotation;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //TODO: throw object
            }
            
            //drop object if the "E" key is pressed again
            if(Input.GetKeyUp(KeyCode.E))
            {
                //Drop();
            }
        }
    }

    /// <summary>
    /// picks up the throwable object, placing it in the player's item space
    /// </summary>
    public override void Activate()
    {
        //turn off gravity on the throwable object
        rigidbody.useGravity = false;

        //set throwability to true
        isThrowable = true;
    }

    public void Throw()
    {

    }

    /// <summary>
    /// drops the object by reactivating gravity and setting throwable bool to false
    /// </summary>
    public void Drop()
    {
        //turn gravity back on for the object
        rigidbody.useGravity = true;

        //set throwability to false
        isThrowable = false;
    }
}
