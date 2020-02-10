using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : InteractableObject
{
    //field for a bool to check if the object is throwable
    [SerializeField]
    protected bool isThrowable = false;

    //field for the object's rigidbody
    protected Rigidbody rigidbody;

    //field for the angle to the left or right at which the object will be held by the player, in degrees
    [SerializeField]
    protected float holdAngle;

    //field for the distance away from the player the object will be held
    [SerializeField]
    protected float holdDistance;

    //field for the angle upwards at which the object will be thrown when held by the player
    [SerializeField]
    protected float throwAngle;

    //field for the force the object should be thrown with
    [SerializeField]
    protected float throwForce;

    //fields for input delay timers to prevent the plyaer from immediately dropping the fish
    [SerializeField]
    protected float inputWaitTime;
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
                Throw();
            }
            

            //OLD: drop object if the "E" key is pressed again
            //CURRENT: drop object if the user right clicked
            if(inputWaitTick <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
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
        //disable 3D text on throwable/holdable object
        gameObject.GetComponentInChildren<FadeText>().gameObject.SetActive(false);

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
        Vector3 throwVector = WorldManager.instance.playerCamera.transform.forward.normalized;
        throwVector = Quaternion.AngleAxis(throwAngle, WorldManager.instance.playerCamera.transform.right) * throwVector;
        throwVector *= throwForce;

        //apply the vector as a force on the object
        rigidbody.AddForce(throwVector, ForceMode.Impulse);
    }

    /// <summary>
    /// drops the object by reactivating gravity and setting throwable bool to false
    /// </summary>
    public void Drop()
    {
        //enable 3D text on throwable/holdable object
        gameObject.GetComponentInChildren<FadeText>().gameObject.SetActive(true);

        //turn gravity back on for the object
        rigidbody.useGravity = true;

        //set throwability to false
        isThrowable = false;
    }
}
