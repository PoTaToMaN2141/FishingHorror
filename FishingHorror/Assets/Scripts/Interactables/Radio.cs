using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : InteractableObject
{
    //field for the audio source attached to the radio
    private AudioSource objectAudio;

    //list of sequence-based voice lines and audio clips
    public List<AudioClip> sequenceClips;

    //list of random positive lines for catching fish
    public List<AudioClip> fishCatchingClips;


    // Start is called before the first frame update
    void Start()
    {
        //set name to radio
        name = "Radio";

        //initialize audio component
        objectAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: check if the radio is on and play sound if it is
    }

    public override void Activate()
    {
        //TODO: turn radio on/off when activated
    }

    /// <summary>
    /// plays a clip from the sequence list
    /// </summary>
    /// <param name="clipIndex"> the index of the sequence clip to play</param>
    public void PlaySequenceClip(int clipIndex)
    {
        //play the clip at the index
        objectAudio.PlayOneShot(sequenceClips[clipIndex]);
    }

    /// <summary>
    /// plays a random audio clip from a passed-in list
    /// </summary>
    /// <param name="clipList"> the list of potential audio clips to pull from</param>
    public void PlayRandomClip(List<AudioClip> clipList)
    {
        //get a random number based on the number of clips in the passed-in list
        int clipIndex = Random.Range(0, clipList.Count);

        //play the clip
        objectAudio.PlayOneShot(clipList[clipIndex]);
    }
}
