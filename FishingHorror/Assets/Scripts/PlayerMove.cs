using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //field for player speed
    [SerializeField]
    private float playerSpeed;

    //field for character controller
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //save character controller
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //get horizontal and vertical input axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //get a movement vector based on saved movement axes
        Vector3 movementVector = transform.right.normalized * moveX + transform.forward.normalized * moveZ;

        //move the character with the character controller
        controller.Move(movementVector * playerSpeed * Time.deltaTime);
    }
}
