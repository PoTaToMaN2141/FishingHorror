using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //Variable for the fish's strength; will affect how much distance the fish gets reeled in + how much stress gets added to the string
    public short strength;
    //Variable for it's speed; affects how fast it can swim from side to side and how fast it can swim away
    public float speed;
    //How fast the fish is swimming based off it's distance from the player
    [SerializeField]
    private float swimSpeed;

    //Timer variables for how aggressive the fish is. The shorter it's time variables, the more aggressive it is at switching it's swimming directions
    public float minTime;
    public float maxTime;

    //The percentage of how commonly the fish will rest or keep swimming
    public float restChance;

    //Timer for swapping the fish's swimming direction
    [SerializeField]
    private float directionTimer = -1f;

    //The current direction the fish is swimming in;
    //-1 is left,
    //0 is staying stil / possibly swimming backward (can add into the future)?
    //1 is right
    public int curDirection;

    //Bool variables for determining the state of the fish
    //If it is resting, it has hit one of the edges of the boat and has stopped moving
    public bool resting = false;
    //If it is escaping, the player has not reeled for a second or so and it is swimming away from the boat
    public bool escaping = false;
    //If it is caught, the game is over
    public bool caught = false;

    //Vector for getting the fish's position
    public Vector3 fishPos;

    //float used for getting the starting z-position of the bobber
    [SerializeField]
    private float startPos;

    //The side borders of the screen; Keeps the fish from swimming off screen
    [SerializeField]
    private float borderRange;

    //The current ranges of the borders; shrinks the closer the fish gets to the player
    public float curBorders;

    //Float used for holding the current distance heuristic
    [SerializeField]
    private float distanceHeuristic;

    //Grab the fishing rod object    
    public GameObject fishingRod;
    //Get the current position of it
    [SerializeField]
    private float playerDisplacement;

    void Start()
    {
        //Grab the player's current position
        playerDisplacement = Mathf.Abs(fishingRod.transform.position.z);
        //Combine the two to get the total range away from the player
        startPos = gameObject.transform.position.z + playerDisplacement;
        //Get the fish's position
        fishPos = gameObject.transform.position;
        //Create the ranges of the borders based off of the distance from the player
        borderRange = fishPos.z + playerDisplacement;
    }

    void Update()
    {
        //If the fish is not caught yet...
        if (!caught)
        {
            //Get the fish's position
            fishPos = gameObject.transform.position;

            //Get the distance heuristic
            distanceHeuristic = ((fishPos.z + playerDisplacement) / startPos);

            //Update the current borders
            //The closer the fish is, the smaller the borders get
            //Kind of visualize a fan, with the base of it starting at the player
            curBorders = 3f + (borderRange * distanceHeuristic);

            //Get the current fishs' swimming speed based off the heuristic
            float maxSpeed = speed + (speed / 3);
            swimSpeed = Mathf.Clamp(distanceHeuristic * maxSpeed, speed / 2, maxSpeed);

            //check to see if the fish needs a new direction
            if (directionTimer <= 0f)
            {
                //Method to get it's new direction
                NewDirection();
                //Reset the resting state
                resting = false;
            }
            else
            {
                //count down until the fish gets a new direction
                directionTimer -= Time.deltaTime;
            }

            //Fight against the player and attempt to get away
            Struggle();

            //If the fish is escaping, add to it's z-value by it's speed to flee from the player
            if (escaping)
            {
                fishPos.z += swimSpeed;
            }
        }
    }

    //Get a new direction after x amount of time
    //POSSIBLE TO-DO: Add a weight to have the fish stop and rest after swimming right or left

    void NewDirection()
    {
        //Randomly choose a length of time to change direction. 
        //The shorter this is, the more aggressive + difficult the fish is
        //The higher this is, the easier the fish is to catch
        directionTimer = Random.Range(minTime, maxTime + 1f);
        //Get a random direction of -1 (left), 0 (stay still / swim forward?), or 1 (right)
        float newDir = Random.Range(0f, 1f);

        //Determine which direction the fish will swim based off of the restChance percentage
        if(newDir <= (1f - restChance) / 2f)
        {
            curDirection = -1;
        }
        else if(newDir <= (1f - restChance))
        {
            curDirection = 1;
        }
        else
        {
            curDirection = 0;
        }

        //If the fish is on one of the borders and was told to swim to it, swim in the opposite direction
        //Used for the left border
        if (fishPos.x <= -4.5f && curDirection == 1) curDirection = 1;
        //For the right
        if (fishPos.x >= 4.5f && curDirection == -1) curDirection = 1;
    }

    //Function for making the fish fight againt the player
    void Struggle()
    {
        //Make sure the fish is swimming
        if (!resting)
        {
            //Make sure the fish stays within the borders; currently this is hard-coded as 5f
            if (Mathf.Abs(fishPos.x) + swimSpeed >= curBorders)
            {
                //The fish is at one of the borders, so decide if it will swim or rest
                //randomly decide if the fish will rest.
                if (Random.Range(0f, 1f) >= (1f - restChance))
                {
                    //Set the resting state to true and update it's direction to 0
                    resting = true;
                    curDirection = 0;
                }
                else
                {
                    //The fish is going out of bounds, so reverse it's current direction
                    curDirection *= -1;
                    fishPos.x += curDirection * swimSpeed;
                }
            }
            //The fish is within the borders
            else
            {
                fishPos.x += curDirection * swimSpeed;
            }
        }

        //Update the fish's position
        gameObject.transform.position = fishPos;
    }
}
