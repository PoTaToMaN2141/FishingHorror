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

    //bool to check if the player is looking at an interactable object
    private bool canInteract = false;

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

            //check if the raycast is hitting 3D text
            if (hitObject.GetComponent<TextMesh>())
            {
                //run the hover method to show the name of the object and the action that can be performed by hitting the interact key
                ObjectHover(hitObject);

                //store the hit object's parent as the last interactable the player looked at
                lastInteractable = hitObject.GetComponentInParent<InteractableObject>().gameObject;

                //set interaction bool to true
                canInteract = true;

                //debug hitting the object with a raycast
                //Debug.Log("looking at the " + hitObject.GetComponentInParent<InteractableObject>().name);
            }
            else if(hitObject.GetComponent<InteractableObject>())
            {
                //run object hover on child 3d text
                ObjectHover(hitObject.GetComponentInChildren<FadeText>(true).gameObject);

                //store the hit object's parent as the last interactable the player looked at
                lastInteractable = hitObject;

                //set interaction bool to true
                canInteract = true;
            }
            else
            {
                //fade text out
                if (lastInteractable != null && lastInteractable.GetComponentInChildren<FadeText>(true).fadeDirection == true)
                {
                    lastInteractable.GetComponentInChildren<FadeText>(true).TextFade(false);
                }

                //set interaction bool to false
                canInteract = false;
            }
        }
        else
        {
            //fade text out
            //check if the player has looked at an interactable object yet and if it's still active
            if(lastInteractable != null && lastInteractable.GetComponentInChildren<FadeText>(true).fadeDirection == true)
            {
                lastInteractable.GetComponentInChildren<FadeText>(true).TextFade(false);
            }

            //set interaction bool to false
            canInteract = false;
        }

        //check if the player can interact with an object, then check for player input
        if(canInteract == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //activate the interactable
                lastInteractable.GetComponent<InteractableObject>().Activate();
            }
        }
    }

    private void ObjectHover(GameObject text)
    {
        //fade object text in
        if(text.GetComponent<FadeText>().fadeDirection == false)
        {
            text.GetComponent<FadeText>().TextFade(true);
        }
    }
}
