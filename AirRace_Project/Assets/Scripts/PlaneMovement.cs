using UnityEngine;
using UnityEngine.InputSystem;

public class PlaneMovement : MonoBehaviour
{
    private Rigidbody plane;

    [Tooltip("Value for max acceleration. Acceleration is given by Input * MaxAcceleration")]
    public int MAX_ACCELERATION;

    [Tooltip("Base rotation speed strength")]
    public float ROTATION_SPEED;

    [Tooltip("Velocity value of the plane at which rotation speed is MAX")]
    public float ROTATION_SPEED_VELOCITY_THRESHOLD;

    [Tooltip("Value of the velocity of the plane at which the lift is max")]
    public float LIFT_VELOCITY_THRESHOLD;

    private float inputAcceleretion;
    private float inputAilerons;
    private float inputElevators;

    private void Awake()
    {
        this.plane = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputAcceleretion = 0;
        inputAilerons = 0;
        inputElevators = 0;
    }

    private void FixedUpdate()
    {
        RotatePlane();
        UpdateThrust();
        UpdateLift();
    }

    private void RotatePlane()
    {

        Vector3 direction = new Vector3(inputElevators, 0, -inputAilerons);

        float velocityFactor = Mathf.Clamp01(plane.velocity.magnitude / ROTATION_SPEED_VELOCITY_THRESHOLD);

        Vector3 force = direction * velocityFactor * ROTATION_SPEED;

        plane.AddRelativeTorque(force, ForceMode.Acceleration);
    }

    private void UpdateThrust()
    {
        Vector3 force = Vector3.forward * (inputAcceleretion * MAX_ACCELERATION);

        plane.AddRelativeForce(force, ForceMode.Acceleration);
    }

    private void UpdateLift()
    {
        Vector3 baseForce = Physics.gravity * -1;

        float velocityFactor = Mathf.Clamp01(plane.velocity.magnitude / LIFT_VELOCITY_THRESHOLD);

        float inclinationFactor = CalculateInclinationFactor();

        Vector3 force = baseForce * velocityFactor * inclinationFactor;

        plane.AddForce(force, ForceMode.Acceleration);
    }

    private float CalculateInclinationFactor()
    {
        bool isRotating = inputAilerons != 0;

        if (isRotating)
            return 1;

        float angle = plane.transform.localEulerAngles.z;
        // y = 0.0000205761 * (x-180)^2 + 0.333333
        float factor = 0.0000205761f * Mathf.Pow(angle - 180, 2) + 0.333333f;

        return factor;
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
