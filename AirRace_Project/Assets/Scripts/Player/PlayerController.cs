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

        RotatePlane();

        UpdateDrag();

        Vector3 thrust = GetThrustVector();
        Vector3 lift = GetLiftVector();

        plane.AddRelativeForce(thrust, ForceMode.Impulse);
        plane.AddForce(lift, ForceMode.Force);

    }

    private void RotatePlane()
    {

        //TODO - implement rotation calculation


        float increment = 0;

        Vector3 rotation = Vector3.zero;

        rotation.z += increment;

        rotation.x += inputVector.y;
        plane.transform.Rotate(rotation);


    }




    private void UpdateDrag()
    {
        //TODO - Implement drag update
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
        //TODO - implement calculation of Lift
        return Physics.gravity * -1 * plane.mass; ;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        // Plane axis are inverted, input vector needs to invert too
        inputVector = context.ReadValue<Vector2>() * -1;

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
