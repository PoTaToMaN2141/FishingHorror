using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private float time;
    private float cycleDuration = 86400; //Seconds in a day
    [SerializeField, Tooltip("The time of day to start at as a percentage from 0 to 1. (0 is noon)")]
    private float startTime;
    [SerializeField, Tooltip("The speed of the day night cycle as a multiple of normal time.")]
    private float timeSpeed;
    [SerializeField, Tooltip("Gradient representing the light color based on time of day.")]
    private Gradient lightColor;
    [SerializeField, Tooltip("Gradient representing the light intensity based on time of day, should only be in grayscale.")]
    private Gradient lightIntensity;
    [SerializeField, Tooltip("The light representing the sun/moon.")]
    private Light directionalLight;

    #region Properties

    /// <summary>
    /// The current time in seconds
    /// </summary>
    public float CurrentTime
    {
        get { return time; }
        set
        {
            //Ensure that the time is within one cycle;
            time = value;

            if(value > cycleDuration)
            {
                value = cycleDuration;
            }
        }
    }

    /// <summary>
    /// The current time as a percentage between 0 and 1;
    /// </summary>
    public float PercentTime
    {
        get { return time / cycleDuration; }
        set { time = value * cycleDuration; }
    }

    /// <summary>
    /// The time speed is how many seconds time moves per second
    /// </summary>
    public float TimeSpeed
    {
        get { return timeSpeed; }
        set { timeSpeed = value; }
    }

    /// <summary>
    /// Boolean used to pause the day night cycle
    /// </summary>
    public static bool Paused { get; set; }

    /// <summary>
    /// The current hour of day
    /// </summary>
    public int Hour
    {
        get
        {
            int hour = (int)(PercentTime * 24 % 12);

            if (hour == 0)
            {
                hour = 12;
            }

            return hour;
        }
    }

    /// <summary>
    /// The current minute of the current hour
    /// </summary>
    public int Minute
    {
        get
        {
            float percent = PercentTime * 24 + 12;
            percent -= (int)percent;

            return Mathf.FloorToInt(percent * 60);
        }
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        time = startTime;
    }

    void Update()
    {
        if (!Paused)
        {
            time += Time.deltaTime * timeSpeed;

            if (time > cycleDuration)
            {
                time -= cycleDuration;
            }
        }
        
        //Debug.Log("Current Time: " + GetTime() + ", " + CurrentTime);
        directionalLight.color = lightColor.Evaluate(PercentTime);
        directionalLight.intensity = lightIntensity.Evaluate(PercentTime).r;

        //Set Rotation

    }

    #endregion

    #region DayNight

    /// <summary>
    /// Returns the current time as a string
    /// </summary>
    /// <returns>The time</returns>
    public string GetTime()
    {
        string minute = Minute.ToString();

        if (Minute < 10)
        {
            minute = "0" + minute;
        }

        string output = Hour + ":" + minute;

        if(PercentTime >= 0.5)
        {
            output += "am";
        }
        else
        {
            output += "pm";
        }

        return output;
    }

    #endregion
}