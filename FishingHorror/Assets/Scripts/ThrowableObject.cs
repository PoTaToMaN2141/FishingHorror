using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : InteractableObject
{
    //field for a bool to check if the object is throwable
    private bool isThrowable = false;

    //field for the object's rigidbody
    private Rigidbody rigidbody;

    //field for the angle to the left or right at which the object will be held by the player, in degrees
    [SerializeField]
    private float holdAngle;

    //field for the distance away from the player the object will be held
    [SerializeField]
    private float holdDistance;

    //fields for input delay timers to prevent the plyaer from immediately dropping the fish
    [SerializeField]
    private float inputWaitTime;
    private float inputWaitTick;

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
            //show object that player is holding in front of them
            Vector3 holdVector = WorldManager.instance.player.transform.forward.normalized;
            holdVector = Quaternion.AngleAxis(holdAngle, Vector3.up) * holdVector;
            holdVector *= holdDistance;
            transform.position = WorldManager.instance.player.transform.position + holdVector;
            transform.forward = Vector3.up;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //TODO: throw object
            }
            
            //drop object if the "E" key is pressed again
            if(inputWaitTick <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Drop();
                }
            }

            //decrement input wait tick
            inputWaitTick -= Time.deltaTime / inputWaitTime;
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

        //start input wait
        inputWaitTick = 1;
    }

    public void Throw()
    {
        //drop the object before applying force
        Drop();

        //get a vector for applying force based on the player's forward angle
        Vector3 throwVector = WorldManager.instance.playerCamera.transform.forward;

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
