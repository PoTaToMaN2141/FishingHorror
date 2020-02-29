using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FacePlayer : MonoBehaviour
{

    //field for a reference to the object the text is attached to
    private GameObject interactable;

    // Start is called before the first frame update
    void Start()
    {
        //save reference to interactable
        interactable = GetComponentInParent<InteractableObject>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //get desired vector from object to player 
        Vector3 faceVector = transform.position - WorldManager.instance.playerCamera.transform.position;

        //set forward vector to desired vector
        transform.forward = faceVector;
    }
}
