using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody plane;
    public int MAX_ACCELERATION;

    private float inputAcceleretion;
    private float inputAilerons;
    private float inputRudder;
    private float inputElevators;

    private void Start()
    {
        inputAcceleretion = 0;
        inputAilerons = 0;
        inputRudder = 0;
        inputElevators = 0;
    }

    private void FixedUpdate()
    {
        //Debug.Log(plane.velocity.magnitude);

        RotatePlane();

        UpdateDrag();

        Vector3 thrust = GetThrustVector();
        Vector3 lift = GetLiftVector();

        plane.AddRelativeForce(thrust, ForceMode.Acceleration);
        plane.AddForce(lift, ForceMode.Acceleration);

    }

    private void RotatePlane()
    {

        //TODO - implement rotation calculation

    }

    private void UpdateDrag()
    {
        //TODO - Implement drag update
    }

    private Vector3 GetThrustVector()
    {
        return Vector3.forward * inputAcceleretion;
    }

    private Vector3 GetLiftVector()
    {
        //TODO - implement calculation of Lift
        return Physics.gravity * -1;
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        inputAcceleretion = value * MAX_ACCELERATION;
    }

    public void OnAileronsMove(InputAction.CallbackContext context)
    {
        inputAilerons = context.ReadValue<float>();
    }

    public void OnRudderMove(InputAction.CallbackContext context)
    {
        inputRudder = context.ReadValue<float>();
    }

    public void OnElevatorsMove(InputAction.CallbackContext context)
    {
        inputElevators = context.ReadValue<float>();
    }

}
