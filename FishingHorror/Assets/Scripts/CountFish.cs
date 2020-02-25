using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum CountMode
{
    Bucket,
    Fog
}

public class CountFish : MonoBehaviour
{
    //field for static instance of this object
    public static CountFish instance;

    //field for the number of fish the player has caught
    public int fishCount = 0;

    //list of caught fish
    private List<GameObject> caughtFish;

    //field for the current counting mode being used by the game
    private CountMode countMode = CountMode.Bucket;

    //TODO: Incorporate Fog counting mode into this script

    //reference to gameobject that holds the fog
    [SerializeField]
    private GameObject fogObject;

    //field for the volume component on the fog object
    private Volume fog;

    // Start is called before the first frame update
    void Start()
    {
        //initialize static instance
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        //intialize fish list
        caughtFish = new List<GameObject>();

        //initialize fog
        fog = fogObject.GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        //debug input to add fish to fish count
        if(Input.GetKeyDown(KeyCode.Insert))
        {
            fishCount++;
        }

        //check fish counts for events
        {
            if(FishingRod.instance.spawnableFish == 0 && SequenceManager.instance.eventIndex != 3)
            {
                //increase event index
                SequenceManager.instance.eventIndex++;
            }
        }
    }

    /// <summary>
    /// used for counting fish in bucket mode
    /// </summary>
    /// <param name="other"> the gameobject that entered the trigger </param>
    private void OnTriggerEnter(Collider other)
    {
        //check if the object that just entered the bucket was a fish that hasn't been caught
        //TODO: replace tag usage once fish are being tracked in a script
        if(other.gameObject.tag == "Fish" && !caughtFish.Contains(other.gameObject))
        {
            //increase fish count
            fishCount++;

            //Debug for fish count
            Debug.Log("Fish dropped into bucket. \nFish Count: " + fishCount);

            //TODO: add fish to a list of caught fish so it won't count more than once as a new fish 
            caughtFish.Add(other.gameObject);
        }
    }
}
