using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JustinsPlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform origin;
    [SerializeField]
    private Transform right;
    [SerializeField]
    private Transform forward;
    [SerializeField]
    private Transform up;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveForce;
    [SerializeField]
    private Vector2 lookSensetivity;
    [SerializeField]
    private Transform boatTransform;
    [SerializeField]
    private Transform lookTarget;

    private Transform cameraTransform;

    private Quaternion orientation;
    private Vector2 boatPosition;
    private Vector2 acceleration;
    private Vector2 velocity;

    private float angle = 0;

    private Vector2 moveInput;
    private Vector2 lookInput;

    #region Unity Functions

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        //Update Camera
        //cameraTransform.rotation *= Quaternion.Euler(0 * lookInput.y * lookSensetivity.y * -1, lookInput.x * lookSensetivity.x, 0);
        cameraTransform.LookAt(lookTarget, up.position - origin.position);

        //Update Position
        angle = cameraTransform.rotation.eulerAngles.y * Mathf.Deg2Rad * -1;
        Vector2 moveDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * moveInput.x + new Vector2(Mathf.Cos(angle + Mathf.PI / 2), Mathf.Sin(angle + Mathf.PI / 2)) * moveInput.y;
        Move(moveDirection);

        //Update Physics
        velocity += acceleration * Time.deltaTime;
        boatPosition += velocity * Time.deltaTime;
        acceleration = Vector2.zero;

        transform.rotation = orientation * boatTransform.rotation;
        transform.position = origin.position + (right.position * boatPosition.x) + origin.position + (forward.position * boatPosition.y);
    }

    #endregion

    #region Justins Player Controller

    private void Move(Vector2 moveDirection)
    {
        Vector2 targetVelocity = moveDirection * moveSpeed;
        Vector2 force = targetVelocity - velocity;
        float forceMagnitude = force.magnitude;

        if(forceMagnitude > 0)
        {
            force /= forceMagnitude;
        }

        force = force * moveForce * (forceMagnitude / (moveSpeed * 2));
        acceleration += force;
    }

    #endregion

    #region Input Management

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    #endregion
}
