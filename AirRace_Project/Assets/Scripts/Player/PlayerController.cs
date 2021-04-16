using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody plane;
    public int maxAcceleretion = 30;

    private Vector2 inputVector;
    private bool turning;
    private float acceleretion;

    private void Start()
    {
        inputVector = Vector2.zero;
        turning = false;
        acceleretion = 0;
        plane.drag = 0.5f;
    }

    /*
     * PHYSICS SIMULATION IDEA CAME FROM THIS VIDEOS
     * 
     * https://youtu.be/p3jDJ9FtTyM
     * https://youtu.be/Gg0TXNXgz-w
     * 
     */
    private void FixedUpdate()
    {
        //Debug.Log(plane.velocity.magnitude);

        //// apply plane turning movement modifiers
        //if (turning)
        //{
        //    Debug.Log("Turning to be defined");

        //    /*
        //     * This conversion is for correct the input vector to
        //     * correctly apply to the corresponding axis.
        //     * 
        //     * W/S -> input Y rotate plane on X axis, upwards and downwards
        //     * A/D -> input X rotate plane on Z axis, turning to the sides
        //     *
        //     */
        //    Vector3 rotation = new Vector3(-inputVector.y, 0, -inputVector.x);

        //    plane.AddRelativeTorque(rotation, ForceMode.Impulse);
        //}

        Vector3 thrust = GetThrustVector();
        plane.AddRelativeForce(thrust, ForceMode.Impulse);

        Vector3 lift = Physics.gravity * -1 * plane.mass;
        plane.AddForce(lift, ForceMode.Force);

    }

    private Vector3 GetThrustVector()
    {
        float force = plane.mass * acceleretion;

        Vector3 direction = Vector3.forward;

        // Used to transform Velocity/physicsFrame into Velocity/second
        float deltatime = Time.fixedDeltaTime;

        return direction * force * deltatime;

    }

    private Vector3 GetLiftVector()
    {
        //Not Implemented
        return Vector3.zero;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();

        if (inputVector.Equals(Vector2.zero))
            turning = false;
        else
            turning = true;
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        acceleretion = value * maxAcceleretion;
    }
}
