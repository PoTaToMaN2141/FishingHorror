using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reeling : MonoBehaviour
{
    //Take in the bobber/fish object and get the fish script from it
    public GameObject bobber;
    private Fish fishScript;
    public int curPosition;

    //Vectors for the fish and the rod's positions
    Vector3 fishPos;
    Vector3 polePos;

    //variables for the current status of the game; displayed through GUI
    float stress = 0;
    bool snapped = false;
    bool caught = false;

    //Variables used for breaking the sections of the screen;
    //Used for seperating the screen into 3 quadrants of (left|center|right)
    public float dirSections = 2f;
    //Borders of the screen
    public float dirBorders = 5f;

    //Timer used for counting down until fish tries to escape
    public float escapeTimer = 0f;

    //the max distance the fish can be before the line starts to break
    public float breakOffRange = 40f;

    //Values for reeling to deduct the fish's distance by
    public float goodReel = .5f;
    public float badReel = .1f;

    void Start()
    {
        //Set the positions and grab the fish script
        polePos = gameObject.transform.position;
        fishScript = bobber.GetComponent<Fish>();
    }

    void Update()
    {
        //If the player hans't broken the line, allow for inputs
        if(!snapped)
        {
            Inputs();
        }
    }

    //Read in player inputs
    void Inputs()
    {
        //Get the fish's current position
        fishPos = fishScript.fishPos;

        //Reset the fishing rod's position
        curPosition = 0;


        //LEFT CLICK
        if (Input.GetMouseButtonDown(0))
        {
            //Move the rod to the left
            polePos.x -= 2;
            //Update it's position
            curPosition = -1;
        }

        if (Input.GetMouseButton(0))
        {
            //Update it's position
            curPosition = -1;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Move the rod back to center
            polePos.x += 2;
            //Reset the rod's position to center
            curPosition = 0;
        }


        //RIGHT CLICk
        if (Input.GetMouseButtonDown(1))
        {
            //Move the rod to the right
            polePos.x += 2;
            //Update it's position
            curPosition = 1;
        }

        if (Input.GetMouseButton(1))
        {
            //Update it's position
            curPosition = 1;
        }

        if (Input.GetMouseButtonUp(1))
        {
            //Move the rod back to center
            polePos.x -= 2;
            //Reset the rod's position to center
            curPosition = 0;
        }

        //If the fish isn't caught, allow the player to keep reeling in the fish
        if (!caught)
        {
            Reel();
        }

        //MIDDLE MOUSE BUTTON
        //if (Input.GetMouseButton(2))
        //  Debug.Log("Pressed middle click.");


        //update the bobbers' position
        bobber.transform.position = fishPos;
        //update the fish's new location
        fishScript.fishPos = fishPos;
        //update the rod's position
        gameObject.transform.position = polePos;


        //If the fish is close enough to the player, they caught it! (wowie!)
        if(Mathf.Abs(gameObject.transform.position.z - bobber.transform.position.z) <= 1f 
        && (Vector3.Distance(gameObject.transform.position, bobber.transform.position) <= 1f))
        {
            //Update the caught statuses
            caught = true;
            fishScript.caught = true;
        }
    }

    //Function used for reeling in the fish
    void Reel()
    {
        //If the player scrolls up or down, they reel in the fish
        //TODO: GET HEURISTIC ON REELING
        if (Input.mouseScrollDelta.y > 0 || Input.mouseScrollDelta.y < 0)
        {
            //===FOUR WAYS TO GO ABOUT FISHING===
            
            //The player matches the fishes' direction consistently
            //If the fish is going left, the player left clicks. if it's staying still, the player goes center
            FollowDirection();

            //The player matches the fishes' direction consistently
            //If the fish is going left, the player right clicks. if it's staying still, the player goes center
            //FightDirection();

            //The player matches the current position of the rod to the fish
            //So if the fish is on the right of the screen, the player leans to the right.
            //If it's in the center, they lean to the center
            //MatchDirection();

            //The player goes to the opposite side of the screen of the fish
            //So if the fish is on the left, the player leans right
            //If the fish is in the center, the player goes center.
            //OppositeDirection();

            //Timer used for seeing if the fish is escaping; gets reset every time the player reels the fish closer
            escapeTimer = 0f;
            //Update the fish's escaping status to false
            fishScript.escaping = false;
        }
        else
        {
            //The player isn't reeling, so add to the timer
            escapeTimer += Time.deltaTime;
            //The player has't reeled in x amount of seconds, so the fish begins to escape
            //Currently this value is hard-coded at 3 seconds
            if (escapeTimer >= 3f)
            {
               fishScript.escaping = true;
            }

            //If the fish gets far enough away, the line starts to break
            if (bobber.transform.position.z > breakOffRange)
            {
                stress += 1;
            }
        }
    }

    //The player matches the fishes' direction consistently
    //If the fish is going left, the player left clicks. if it's staying still, the player goes center
    void FollowDirection()
    {
        //If the direction the player is in is the same as the fish, they are properly reeling
        if (curPosition == fishScript.curDirection)
        {
            //The distance they reel is much higher
            fishPos.z -= goodReel;
            //The line's stress decrases
            if (stress > 0) stress -= 2;
            //The fish moves to the pole
            MoveToPole();
        }
        //The player hasn't properly reeled
        else
        {
            //The fish draws closer, but at a much smaller amount
            fishPos.z -= badReel;
            //The line's stress incrases by the fish's strength
            //TO DO?: MAKE THIS A HEURISITC?
            stress += fishScript.strength;

            //The line's stress has gotten to high and it snapped
            if (stress >= 100) snapped = true;
        }
    }

    //The player fights against the fishes' direction consistently
    //If the fish is going left, the player right clicks. if it's staying still, the player goes center
    void FightDirection()
    {
        //If the direction the player is in is the same as the fish, they are properly reeling
        if (curPosition == fishScript.curDirection*-1)
        {
            //The distance they reel is much higher
            fishPos.z -= goodReel;
            //The line's stress decrases
            if (stress > 0) stress -= 2;
            //The fish moves to the pole
            MoveToPole();
        }
        //The player hasn't properly reeled
        else
        {
            //The fish draws closer, but at a much smaller amount
            fishPos.z -= badReel;
            //The line's stress incrases by the fish's strength
            //TO DO?: MAKE THIS A HEURISITC?
            stress += fishScript.strength;

            //The line's stress has gotten to high and it snapped
            if (stress >= 100) snapped = true;
        }
    }

    //The player matches the current position of the rod to the fish
    //So if the fish is on the right of the screen, the player leans to the right.
    //If it's in the center, they lean to the center
    //Ugly and horribly written out, but works :'(
    void MatchDirection()
    {
        //The fish is on the left side of the screen and the player is leaning left
        if((fishPos.x < -dirSections && curPosition == -1)
        //The fish is on the right side of the screen and is leaning right
        || (fishPos.x > dirSections && curPosition == 1)
        //The fish is in the center of the screen and the player is centered
        || ((fishPos.x > -dirSections && curPosition == 0) 
        && (fishPos.x < dirSections && curPosition == 0)))
        {
            //The distance they reel is much higher
            fishPos.z -= goodReel;
            //The line's stress decrases
            if (stress > 0) stress -= 2;
            //The fish moves to the pole
            MoveToPole();
        }
        //The player hasn't properly reeled
        else
        {
            //The fish draws closer, but at a much smaller amount
            fishPos.z -= badReel;
            //The line's stress incrases by the fish's strength
            //TO DO?: MAKE THIS A HEURISITC?
            stress += fishScript.strength;

            //The line's stress has gotten to high and it snapped
            if (stress >= 100) snapped = true;
        }
    }


    //The player goes to the opposite side of the screen of the fish
    //So if the fish is on the left, the player leans right
    //If the fish is in the center, the player goes center.
    //Also ugly and horribly written out, but works :'(
    void OppositeDirection()
    {
        //The fish is on the left side of the screen and the player is leaning right
        if ((fishPos.x < -dirSections && curPosition == 1)
        //The fish is on the right side of the screen and is leaning left
        || (fishPos.x > dirSections && curPosition == -1)
        //The fish is in the center of the screen and the player is centered
        || ((fishPos.x > -dirSections && curPosition == 0)
        && (fishPos.x < dirSections && curPosition == 0)))
        {
            //The distance they reel is much higher
            fishPos.z -= goodReel;
            //The line's stress decrases
            if (stress > 0) stress -= 2;
            //The fish moves to the pole
            MoveToPole();
        }
        //The player hasn't properly reeled
        else
        {
            //The fish draws closer, but at a much smaller amount
            fishPos.z -= badReel;
            //The line's stress incrases by the fish's strength
            //TO DO?: MAKE THIS A HEURISITC?
            stress += fishScript.strength;

            //The line's stress has gotten to high and it snapped
            if (stress >= 100) snapped = true;
        }
    }

    //The player has properly reeled in, so the fish moves closer to the pole
    void MoveToPole()
    {
        //The fish draws closer from the left
        if (fishPos.x < polePos.x)
        {
            fishPos.x += .25f;
        }

        //The fish draws closer from the right
        if (fishPos.x > polePos.x)
        {
            fishPos.x -= .25f;
        }
    }


    //Display text to the screen
    void OnGUI()
    {
        //Display the line's stress
        string text = "Line Stress: " + stress.ToString();
        text = GUI.TextField(new Rect(10, 30, 210, 30), text, 100);

        //Show the current state of the  game
        //If the line snaps, game's over
        text = (!snapped ? "Safe!" :  "SNAPPED!");
        text = GUI.TextField(new Rect(10, 60, 210, 30), text, 100);

        //If the fish is caught, the game's won
        text = (!caught ? "Fishing!" : "CAUGHT!");
        text = GUI.TextField(new Rect(10, 90, 210, 30), text, 100);

        //Explain rules
        text = "Left + Right click to move";
        text = GUI.TextField(new Rect(950, 30, 210, 30), text, 100);

        text = "Change the reeling type";
        text = GUI.TextField(new Rect(950, 60, 210, 30), text, 100);

        text = "in the 'Reel()' function";
        text = GUI.TextField(new Rect(950, 90, 210, 30), text, 100);
    }
}
