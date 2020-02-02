using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    //Variable for the fish's strength; will affect how much distance the fish gets reeled in + how much stress gets added to the string
    public short strength;
    //Variable for it's speed; affects how fast it can swim from side to side and how fast it can swim away
    public float speed;
    //Timer variables for how aggressive the fish is. The shorter it's time variables, the more aggressive it is at switching it's swimming directions
    public float minTime;
    public float maxTime;

    //Timer for swapping the fish's swimming direction
    public float directionTimer = -1f;
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

    //OPTION - ADD WEIGHTS TO DIRECTIONS
    //FISH COULD BE WEIGHTED TO MOVE RIGHT OR LEFT, BUT NEVER STAY STILL
    //OR ADD TWO TYPES OF FISH

    void Update()
    {
        //If the fish is not caught yet...
       if(!caught)
        {
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
                fishPos.z += speed;
            }
        }
    }

    //Get a new direction after x amount of time
    void NewDirection()
    {
        //Randomly choose a length of time to change direction. 
        //The shorter this is, the more aggressive + difficult the fish is
        //The higher this is, the easier the fish is to catch
        directionTimer = Random.Range(minTime, maxTime + 1f);
        //Get a random direction of -1 (left), 0 (stay still / swim forward?), or 1 (right)
        curDirection = Random.Range(-1, 2);

        //If the fish is on one of the borders and was told to swim to it, swim in the opposite direction
        //Used for the left border
        if (fishPos.x <= -4.5f && curDirection == 1) curDirection = 1;
        //For the right
        if (fishPos.x >= 4.5f && curDirection == -1) curDirection = 1;
    }

    //Function for making the fish fight againt the player
    void Struggle()
    {
        //Get the fish's position
        fishPos = gameObject.transform.position;

        //Make sure the fish is swimming
        if (!resting)
        {
            //Make sure the fish stays within the borders; currently this is hard-coded as 5f
            //CAN MAKE THE 5F A HEURISTIC; WOULD MAKE FAN SHAPE. USE DISTANCE FOR HEAURISTIC
            if (Mathf.Abs(fishPos.x) + speed >= 5f)
            {
                //The fish is at one of the borders, so decide if it will swim or rest
                //randomly decide if the fish will rest; currently hardcoded as a 1/5 chance.
                //CAN UPDATE IN THE FUTURE TO NOT BE HARDCODED
                if (Random.Range(0, 6) >= 4)
                {
                    //Set the resting state to true and update it's direction to 0
                    resting = true;
                    curDirection = 0;
                }
                else
                {
                    //The fish is going out of bounds, so reverse it's current direction
                    curDirection *= -1;
                    fishPos.x += curDirection * speed;
                }
            }
            //The fish is within the borders
            else
            {
                fishPos.x += curDirection * speed;
            }
        }

        //Update the fish's position
        gameObject.transform.position = fishPos;
    }
}
