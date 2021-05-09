using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody plane;
    public int MAX_ACCELERATION;
    public float rotationSpeed;

    private float inputAcceleretion;
    private float inputAilerons;
    private float inputElevators;

    private void Start()
    {
        inputAcceleretion = 0;
        inputAilerons = 0;
        inputElevators = 0;
    }

    private void FixedUpdate()
    {
        RotatePlane();

        Vector3 thrust = GetThrustVector();
        Vector3 lift = GetLiftVector();

        plane.AddRelativeForce(thrust, ForceMode.Acceleration);
        plane.AddForce(lift, ForceMode.Acceleration);
    }

    private void RotatePlane()
    {

        Vector3 direction = new Vector3(inputElevators, 0, -inputAilerons);

        float velocityGoal = 70; //the velocity at which rotation speed is max
        float velocityFactor = Mathf.Lerp(0, 1, plane.velocity.magnitude / velocityGoal);

        Vector3 force = direction * velocityFactor * rotationSpeed;

        plane.AddRelativeTorque(force, ForceMode.Acceleration);
    }

    private Vector3 GetThrustVector()
    {
        return Vector3.forward * (inputAcceleretion * MAX_ACCELERATION);
    }

    private Vector3 GetLiftVector()
    {
        //TODO - implement calculation of Lift
        return Physics.gravity * -1;
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        inputAcceleretion = context.ReadValue<float>();
    }

    public void OnAileronsMove(InputAction.CallbackContext context)
    {
        inputAilerons = context.ReadValue<float>();
    }

    public void OnElevatorsMove(InputAction.CallbackContext context)
    {
        inputElevators = context.ReadValue<float>();
    }

}
