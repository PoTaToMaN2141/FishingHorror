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
    public int spawnableFish;

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

        name = "Fishing Rod";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        //spawn a fish for the player if there are more than 0 spawnable fish
        if(spawnableFish > 0)
        {
            Instantiate(fish, fishSpawn, true);
            spawnableFish--;
        }
    }

    public void SetEventFish(int eventIndex)
    {
        //give the player spawnable fish based on the current event
        spawnableFish = fishNumList[eventIndex];
    }
}
