using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CountMode
{
    Bucket,
    Fog
}

public class CountFish : MonoBehaviour
{
    //field for the number of fish the player has caught
    public int fishCount = 0;

    //field for the current counting mode being used by the game
    private CountMode countMode = CountMode.Bucket;

    //TODO: Incorporate Fog counting mode into this script

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check fish count to determine whether or not to start next stage of the game
    }

    /// <summary>
    /// used for counting fish in bucket mode
    /// </summary>
    /// <param name="other"> the gameobject that entered the trigger </param>
    private void OnTriggerEnter(Collider other)
    {
        //check if the object that just entered the bucket was a fish
        //TODO: replace tag usage once fish are being tracked in a script
        if(other.gameObject.tag == "Fish")
        {
            //increase fish count
            fishCount++;

            //Debug for fish count
            Debug.Log("Fish dropped into bucket. \nFish Count: " + fishCount);

            //TODO: add fish to a list of caught fish so it won't count more than once as a new fish 
        }
    }
}
