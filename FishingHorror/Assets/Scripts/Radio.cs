using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractableObject
{
    // Start is called before the first frame update
    void Start()
    {
        //set name to radio
        name = "Radio";
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check if the radio is on and play sound if it is
    }

    protected override void Activate()
    {
        //TODO: turn radio on/off when activated
    }
}
