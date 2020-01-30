using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    //field for interaction distance
    [SerializeField]
    private float interactDistance;

    //field for the last interactable object the player looked at
    private GameObject lastInteractable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create raycast hit
        RaycastHit hit;

        //debug raycast
        //Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow);

        //send out a raycast to check for interactable objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            //store the gameobject that the raycast hit
            GameObject hitObject = hit.transform.gameObject;

            //check if the raycast is hitting an interactable object
            if (hitObject.GetComponent<InteractableObject>())
            {
                //run the hover method to show the name of the object and the action that can be performed by hitting the interact key
                ObjectHover(hitObject.GetComponent<InteractableObject>());

                //store the hit object as the last interactable the player looked at
                lastInteractable = hitObject;

                //debug hitting the object with a raycast
                Debug.Log("looking at the " + hitObject.GetComponent<InteractableObject>().name);
            }
            else
            {
                //TODO: clear text from the screen
                if (lastInteractable != null && lastInteractable.GetComponentInChildren<FadeText>().fadeDirection == true)
                {
                    lastInteractable.GetComponentInChildren<FadeText>().TextFade(false);
                }
            }
        }
        else
        {
            //TODO: clear text from the screen
            //check if the player has looked at an interactable object yet and if it's still active
            if(lastInteractable != null && lastInteractable.GetComponentInChildren<FadeText>().fadeDirection == true)
            {
                lastInteractable.GetComponentInChildren<FadeText>().TextFade(false);
            }
        }
    }

    private void ObjectHover(InteractableObject interactable)
    {
        //TODO: display the name of the object on screen
        if(interactable.gameObject.GetComponentInChildren<FadeText>().fadeDirection == false)
        {
            interactable.gameObject.GetComponentInChildren<FadeText>().TextFade(true);
        }
    }
}
