using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : InteractableObject
{
    private Bait baitScript;

    [SerializeField]
    private bool isFishing = false;

    // Start is called before the first frame update
    void Start()
    {
        name = "Fishing Rod";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate()
    {
        GameObject[] baitObjs;
        baitObjs = GameObject.FindGameObjectsWithTag("Bait");

        foreach (GameObject bait in baitObjs)
        {
            baitScript = bait.GetComponent<Bait>();
            if(bait.GetComponent<Bait>().getIsThrowable())
            {
                isFishing = true;
                Destroy(bait);
            }
        }
    }
}
