using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FacePlayer : MonoBehaviour
{
    //field for reference to player camera
    //TODO: replace with global reference to player if created later
    [SerializeField]
    private GameObject playerCamera;

    //field for a reference to the object the text is attached to
    private GameObject interactable;

    //field for text distance from its parent object
    [Range(0.0f, 100f)]
    public float textDistancePercentage;

    // Start is called before the first frame update
    void Start()
    {
        //save reference to interactable
        interactable = GetComponentInParent<InteractableObject>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //get the vector between the parent interactable and the camera's near clip plane, normalize it, and multiply it by text distance
        Vector3 posVector = playerCamera.transform.position - interactable.transform.position;

        //calculate distance to multiply based on distance percentage
        float percentageCalc = textDistancePercentage * posVector.magnitude;
        float textDistance = percentageCalc / 100f;

        //normalize psoition vector and multiply by text distance
        posVector = posVector.normalized;
        posVector *= textDistance;

        //set text postion based on position vector
        transform.position = interactable.transform.position + posVector;

        //get desired vector from object to player 
        Vector3 faceVector = transform.position - playerCamera.transform.position;

        //set forward vector to desired vector
        transform.forward = faceVector;
    }
}
