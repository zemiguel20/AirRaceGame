using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody plane;

    private Vector2 inputVector;
    private bool moving;

    private void Start()
    {
        inputVector = Vector2.zero;
        moving = false;
    }

    private void FixedUpdate()
    {
        plane.velocity = Vector3.zero; //Reset velocity

        // apply plane turning movement modifiers
        if (moving)
        {
            Debug.Log("Turning to be defined");
        }

        // apply forward velocity
        plane.AddRelativeForce(Vector3.forward * 10, ForceMode.VelocityChange);

    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();

        if (inputVector.Equals(Vector2.zero))
            moving = false;
        else
            moving = true;
    }
}
