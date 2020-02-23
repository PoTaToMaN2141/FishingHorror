using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRock : MonoBehaviour
{
    public float moveAmplifier; //A multiplier for the water swell so we can line it up with the boat
    public float sinDifference; //A delay for our second sin wave so we can set up a rotational difference
    public float totalSinOut;
    private float sinTime; //A reference to the sin of our current time
    private float sinTime2; //A reference to the sin of our time - the sinDifference

    [SerializeField]
    private GameObject boat; //A reference to our boat
    private Vector3 boatTransform; //A placeholder for the boat's euler angles

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AdjustSinTime();
        AdjustBoat();
        //moveScript.Move();
    }

    /// <summary>
    /// Sets the sinTime and sinTime2 variables
    /// </summary>
    void AdjustSinTime()
    {
        sinTime = Mathf.Sin(Time.time);
        sinTime2 = Mathf.Sin(Time.time + sinDifference);
        totalSinOut = (sinTime - sinTime2) * moveAmplifier;
    }

    /// <summary>
    /// Adjusts the transform of the boat to be equal to sinTime-sinTime2 * the moveAmplifier
    /// </summary>
    void AdjustBoat()
    {
        boatTransform = boat.transform.rotation.eulerAngles;
        boatTransform = new Vector3(boatTransform.x, boatTransform.y, totalSinOut);
        boat.transform.rotation = Quaternion.Euler(boatTransform);
    }
}
