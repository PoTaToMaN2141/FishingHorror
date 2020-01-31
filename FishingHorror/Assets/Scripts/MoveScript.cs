using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector2 addVector;

    private Vector2 forwardAxes;
    private Vector2 rightAxes;

    // Start is called before the first frame update
    void Start()
    {
        forwardAxes = new Vector2();
        addVector = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        forwardAxes.x = -transform.right.z;
        forwardAxes.y = transform.forward.z;
        rightAxes.x = -transform.right.x;
        rightAxes.y = transform.forward.x;

        if (Input.GetKey(KeyCode.W))
        {
            addVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            addVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            addVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            addVector.x += 1;
        }

        addVector = (addVector.x * -rightAxes) + (addVector.y * forwardAxes);

        addVector.Normalize();

        addVector = addVector * moveSpeed;

        transform.position = new Vector3(transform.position.x + addVector.x, transform.position.y, transform.position.z + addVector.y);
        addVector = Vector2.zero;
    }
}