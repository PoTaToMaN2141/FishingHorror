using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    //field for a static instance of this object
    public static WorldManager instance;

    //field for a reference to the player
    public GameObject player;

    //field for a reference to the player camera
    public GameObject playerCamera;

    //called before start and update
    private void Awake()
    {
        //set static instance of this object
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
