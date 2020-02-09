using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : ThrowableObject
{
    public bool isSacrificial = true;

    [SerializeField]
    short baitLevel = 0;

    void Start()
    {
    //save reference to object's rigidbody
    rigidbody = GetComponent<Rigidbody>();

        if (!isSacrificial)
        {
            holdAngle = 20;
            holdDistance = 1;
            throwAngle = 0;
            throwForce = 0;
            inputWaitTime = 0;
        }
    }

    public bool getIsThrowable()
    {
        Debug.Log(isThrowable);
        return isThrowable;
    }
}
