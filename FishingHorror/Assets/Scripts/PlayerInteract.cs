using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //reference to the scriptable object that contains all interactable objects
    [SerializeField]
    private Interactables interactables;

    //field for interaction distance
    [SerializeField]
    private float interactDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //create raycast hit
        RaycastHit hit;

        //send out a raycast to check for interactable objects
        Physics.Raycast(transform.position, transform.forward, out hit, interactDistance);
        //debug raycast
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.green);

        //check if the raycast is hitting an interactable object
        if(interactables.interactableList.Contains(hit.transform.gameObject))
        {
            //run the hover method to show the name of the object and the action that can be performed by hitting the interact key
        }
    }

    private void ObjectHover(GameObject interactable)
    {

    }
}
