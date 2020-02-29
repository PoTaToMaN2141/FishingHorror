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

    //field for the object that holds the daynight script and the amount of time it takes to change time
    private DayNight timeCycle;
    public int phaseSeconds;

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
        timeCycle = GetComponent<DayNight>();

        //subscribe certain methods to fish event at the start of the game
        fishEvent += radio.GetComponent<Radio>().PlaySequenceClip;
        fishEvent += FishingRod.instance.SetEventFish;

        //make sure event index is at 0
        eventIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //start next event if the index has changed
        if(eventIndex != prevIndex)
        {
            //activate next event
            fishEvent(eventIndex);

            //move time forward
            timeCycle.SetHour(timeThresholdList[eventIndex], phaseSeconds);

            //radio stuff
        }

        //set previous index to current index
        prevIndex = eventIndex;
    }
}
