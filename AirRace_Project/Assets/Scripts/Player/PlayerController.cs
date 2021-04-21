using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody plane;
    public int maxAcceleretion = 30;

    private float acceleretion;

    private void Start()
    {
        acceleretion = 0;
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
        return Vector3.zero;

    }

    private Vector3 GetLiftVector()
    {
        //TODO - implement calculation of Lift
        return Physics.gravity * -1;
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        acceleretion = value * maxAcceleretion;
    }

}
