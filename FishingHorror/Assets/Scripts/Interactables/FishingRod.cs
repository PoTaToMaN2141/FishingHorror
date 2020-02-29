using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : InteractableObject
{
    //static instance of this script
    public static FishingRod instance;

    //transform for where the fish will spawn
    [SerializeField]
    private Transform fishSpawn;

    //fish prefab
    [SerializeField]
    private GameObject fish;

    //number of fish you can spawn
    private int spawnableFish;
    public int eventFish;

    //list of fOsh numbers for events
    [SerializeField]
    public List<int> fishNumList;

    // Start is called before the first frame update
    void Start()
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

        //make sure not to skip the first event
        SetEventFish(0);

        name = "Fishing Rod";
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(spawnableFish);
    }

    public override void Activate()
    {
        //spawn a fish for the player if there are more than 0 spawnable fish
        if(spawnableFish > 0)
        {
            Instantiate(fish, fishSpawn.position, Quaternion.identity);
            spawnableFish--;
        }
    }

    public void SetEventFish(int eventIndex)
    {
        //give the player spawnable fish based on the current event
        spawnableFish = fishNumList[eventIndex];
        eventFish = spawnableFish;
    }
}
