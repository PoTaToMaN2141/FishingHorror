using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    //field for the next threshold of fish the player has to catch to advance to the next sequence
    private int nextFishThreshold = 0;

    //list of fish thresholds based on events the player experiences
    public List<int> fishThresholdList;

    //list of time thresholds for game stages
    [Tooltip("Note: When setting time thresholds, try to keep increments in hours! There are 3600 seconds in an hour.")]
    public List<int> timeThresholdList;

    //field for the object that holds the daynight script and the script itself
    [SerializeField]
    private GameObject timeObject;
    private DayNight timeCycle;

    //field for the scriptable object that contains the library of the games' fish
    [SerializeField]
    private FishLibrary fishLibrary;
    private Dictionary<int, List<GameObject>> library = new Dictionary<int, List<GameObject>>();

    //field for the radio
    [SerializeField]
    private GameObject radio;

    // Start is called before the first frame update
    void Start()
    {
        //save reference to time cycle
        timeCycle = timeObject.GetComponent<DayNight>();

        //set up fish library
        library[0] = fishLibrary.stageOneFish;
        library[1] = fishLibrary.stageTwoFish;
        library[2] = fishLibrary.stageThreeFish;
        library[3] = fishLibrary.stageFourFish;
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check fish count to determine whether or not to start next stage of the game
        if(CountFish.instance.fishCount == nextFishThreshold)
        {
            //TODO: allow time to advance to next stage
            //TODO: If time hasn't completely passed from the previous stage, quickly advance it forward
            if(DayNight.Paused != true)
            {
                //increase the speed of time
                timeCycle.TimeSpeed = 4000f;
                Debug.Log("Speeding up time to match the player");
            }
            else
            {
                DayNight.Paused = false;
            }

            //increase fish threshold
            nextFishThreshold += fishThresholdList[0];
            Debug.Log("Next Threshold for Fish: " + nextFishThreshold + "\nFish Count: " + CountFish.instance.fishCount);

            //remove first member of the fish threshold list
            fishThresholdList.RemoveAt(0);

            //TODO: change the kinds of fish the player can catch

            //TODO: play an audio clip based on the sequence the player is in
            radio.GetComponent<Radio>().PlaySequenceClip(0);
            radio.GetComponent<Radio>().sequenceClips.RemoveAt(0);
        }

        //check if the current time has reached the stopping point for the stage
        if(timeCycle.CurrentTime >= timeThresholdList[0])
        {
            //Debug for stopped time
            Debug.Log("Time has stopped. Catch more fish to resume time.");

            //pause time and set new time threshold
            DayNight.Paused = true;
            timeThresholdList.RemoveAt(0);

            //reset time cycle scale
            //TODO: change this to match actual scale instead of test scale
            timeCycle.TimeSpeed = 3600f;
        }
    }
}
