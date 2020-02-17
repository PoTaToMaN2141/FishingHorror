using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    //field for the next threshold of fish the player has to catch to advance to the next sequence
    private int nextThreshold = 0;

    //list of fish thresholds based on events the player experiences
    public List<int> fishThresholdList;

    //list of time thresholds for game stages
    [Tooltip("Note: When setting time thresholds, try to keep increments in hours! There are 3600 seconds in an hour.")]
    public List<int> timeThresholdList;

    //field for the object that holds the daynight script and the script itself
    [SerializeField]
    private GameObject timeObject;
    private DayNight timeCycle;

    // Start is called before the first frame update
    void Start()
    {
        //save reference to time cycle
        timeCycle = timeObject.GetComponent<DayNight>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check fish count to determine whether or not to start next stage of the game
        if(CountFish.instance.fishCount == nextThreshold)
        {
            //TODO: allow time to advance to next stage
            //TODO: If time hasn't completely passed from the previous stage, quickly advance it forward
            DayNight.Paused = false;

            //increase fish threshold
            nextThreshold += fishThresholdList[0];
            Debug.Log("Next Threshold for Fish: " + nextThreshold + "\nFish Count: " + CountFish.instance.fishCount);

            //remove first member of the fish threshold list
            fishThresholdList.RemoveAt(0);

            //TODO: change the kinds of fish the player can catch
        }

        //check if the current time has reached the stopping point for the stage
        if(timeCycle.CurrentTime >= timeThresholdList[0])
        {
            //Debug for stopped time
            Debug.Log("Time has stopped. Catch more fish to resume time.");

            //pause time and set new time threshold
            DayNight.Paused = true;
            timeThresholdList.RemoveAt(0);
        }
    }
}
