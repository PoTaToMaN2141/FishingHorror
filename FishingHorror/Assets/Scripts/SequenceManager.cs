using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    //field for the next threshold of fish the player has to catch to advance to the next sequence
    private int nextThreshold = 0;

    //list of fish thresholds based on events the player experiences
    public List<int> fishThresholdList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check fish count to determine whether or not to start next stage of the game
        if(CountFish.instance.fishCount == nextThreshold)
        {
            //TODO: allow time to advance to next stage
            

            //increase fish threshold
            nextThreshold += fishThresholdList[0];

            //remove first member of the fish threshold list
            fishThresholdList.RemoveAt(0);
        }
    }
}
