using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//delegate for game events
public delegate void FishEvent(int eventIndex);

public class SequenceManager : MonoBehaviour
{
    //static instance of this class
    public static SequenceManager instance;

    //the index for events occurring in the game
    public int eventIndex;
    private int prevIndex;

    //list of time thresholds for game stages
    [Tooltip("Note: When setting time thresholds, try to keep increments in hours! There are 3600 seconds in an hour.")]
    public List<int> timeThresholdList;

    //field for the object that holds the daynight script and the script itself
    [SerializeField]
    private GameObject timeObject;
    private DayNight timeCycle;

    //field for the radio
    [SerializeField]
    private GameObject radio;

    //event handler
    public event FishEvent fishEvent;

    private void Awake()
    {
        //set up static instance
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //save reference to time cycle
        timeCycle = timeObject.GetComponent<DayNight>();

        //subscribe certain methods to fish event at the start of the game
        fishEvent += radio.GetComponent<Radio>().PlaySequenceClip;
    }

    // Update is called once per frame
    void Update()
    {
        //start next event if the index has changed
        if(eventIndex != prevIndex)
        {
            //activate next event
            fishEvent(eventIndex);
        }

        //check if the current time has reached the stopping point for the stage
        if(timeCycle.CurrentTime >= timeThresholdList[eventIndex])
        {
            //Debug for stopped time
            Debug.Log("Time has stopped. Catch more fish to resume time.");

            //pause time and set new time threshold
            DayNight.Paused = true;

            //reset time cycle scale
            //TODO: change this to match actual scale instead of test scale
            timeCycle.TimeSpeed = 3600f;
        }

        //set previous index to current index
        prevIndex = eventIndex;
    }
}
