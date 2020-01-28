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

    //field to hold a reference to the UI Text
    [SerializeField]
    private TextMeshProUGUI screenText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //debug to list out the interactables list
        //foreach(InteractableObject interactable in interactables.interactableList)
        //{
            //Debug.Log("Interactables: " + interactable.name);
        //}

        //create raycast hit
        RaycastHit hit;

        //debug raycast
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow);

        //send out a raycast to check for interactable objects
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
        {
            //debug raycast
            Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow);
            Debug.Log("hitting an object");

            //store the gameobject that the raycast hit
            GameObject hitObject = hit.transform.gameObject;

            //check if the raycast is hitting an interactable object
            if (hitObject.GetComponent<InteractableObject>())
            {
                //run the hover method to show the name of the object and the action that can be performed by hitting the interact key
                ObjectHover(hitObject.GetComponent<InteractableObject>());

                //debug hitting the object with a raycast
                Debug.Log("looking at the " + hitObject.GetComponent<InteractableObject>().name);
            }
        }
        else
        {
            //TODO: clear text from the screen
            screenText.text = "";
        }
    }

    private void ObjectHover(InteractableObject interactable)
    {
        //TODO: display the name of the object on screen
        screenText.text = interactable.name + "\nPress 'E' to interact";
    }
}
