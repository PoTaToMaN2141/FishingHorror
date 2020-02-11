using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector2 addVector;

    [SerializeField]
    private Transform parentTransform;

    [SerializeField]
    private Transform boatHolder;

    [SerializeField]
    private BoatRock boatRock;

    private Vector2 forwardAxes;
    private Vector2 rightAxes;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        forwardAxes = new Vector2();
        addVector = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (SetPlayerState.instance.playerState != PlayerState.Fishing)
        {
            //parentTransform.up = boatHolder.up;
            forwardAxes.x = -parentTransform.right.z;
            forwardAxes.y = parentTransform.forward.z;
            rightAxes.x = -parentTransform.right.x;
            rightAxes.y = parentTransform.forward.x;

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

            distance = (parentTransform.position.x - boatHolder.position.x) + (parentTransform.position.z - boatHolder.position.z);

            parentTransform.position = new Vector3(parentTransform.position.x + addVector.x, boatRock.totalSinOut/2 * distance + boatHolder.position.y, parentTransform.position.z + addVector.y);
            addVector = Vector2.zero;
        }
    }
}