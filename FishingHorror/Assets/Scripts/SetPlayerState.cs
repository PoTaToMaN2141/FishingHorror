using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Walking,
    Fishing
}

public class SetPlayerState : MonoBehaviour
{
    //field for a static instance of this class
    public static SetPlayerState instance;

    //field for player state, set to walking by default
    public PlayerState playerState = PlayerState.Walking;

    // Start is called before the first frame update
    void Start()
    {
        //set static instance
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug for player state changes
        if (Input.GetKeyDown(KeyCode.E))
        {
            //switch statement to rotate through player states
            switch (playerState)
            {
                case PlayerState.Walking:
                    //switch to the next playerstate, fishing
                    playerState = PlayerState.Fishing;
                    break;
                case PlayerState.Fishing:
                    //switch to the next playerstate, walking
                    playerState = PlayerState.Walking;
                    break;
            }
        }
    }
}
