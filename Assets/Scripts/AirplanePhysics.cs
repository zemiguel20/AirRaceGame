using System;
using UnityEngine;

namespace AirRace
{
    // NASA webpage with topics about aerodynamics
    // https://www.grc.nasa.gov/www/k-12/airplane/short.html

    public class AirplanePhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody airplaneRigidbody;

        [SerializeField] [Min(0)] [Tooltip("in (m/s)2")] private float throttle;

        [SerializeField] [Range(0, 90)] [Tooltip("in degrees")] private float stallAngle;
        [SerializeField] [Min(0)] private float maxLiftCoefficient;
        [SerializeField] [Min(0)] private float minDragCoefficient;
        [SerializeField] [Min(0)] private float dragCoefficientAtStallAngle;

        [SerializeField] [Min(0)] private float rollSpeed;
        [SerializeField] [Min(0)] private float pitchSpeed;
        [SerializeField] [Min(0)] private float yawSpeed;

        [NonSerialized] public float rollInput;
        [NonSerialized] public float yawInput;
        [NonSerialized] public float pitchInput;

        private void FixedUpdate()
        {
            AirplaneRotation();
            AirplaneForces();
        }

        private void AirplaneRotation()
        {
            //Simple calculation of angular velocity on each axis, using input variables that can be set by a controller object/component

            Vector3 rollRotation = rollInput * rollSpeed * transform.forward;
            Vector3 pitchRotation = pitchInput * pitchSpeed * transform.right;
            Vector3 yawRotation = yawInput * yawSpeed * transform.up;

            airplaneRigidbody.angularVelocity = Vector3.Lerp(airplaneRigidbody.angularVelocity, rollRotation + pitchRotation + yawRotation, 0.5f);
        }

        private void AirplaneForces()
        {
            //Thrust
            airplaneRigidbody.AddForce(airplaneRigidbody.mass * throttle * transform.forward);

            // Angle of attack relative to the planes velocity
            // Cross product vector between X and Z axis is used as reference, which is the world Down vector
            float angleOfAttack = Vector3.SignedAngle(airplaneRigidbody.velocity, transform.forward, transform.right * -1);

            float liftCoefficient = CalculateLiftCoefficient(angleOfAttack);
            float dragCoefficient = CalculateDragCoefficient(angleOfAttack);


            //Lift (original equation https://www.grc.nasa.gov/www/k-12/airplane/lifteq.html)
            //Direction relative to the velocity and wings
            float magnitude = liftCoefficient * Mathf.Pow(airplaneRigidbody.velocity.magnitude, 2) / 2;
            Vector3 direction = Vector3.Cross(transform.right * -1, airplaneRigidbody.velocity).normalized;
            airplaneRigidbody.AddForce(magnitude * direction);

            //Drag (original equation https://www.grc.nasa.gov/www/k-12/airplane/drageq.html)
            float magnitude2 = dragCoefficient * Mathf.Pow(airplaneRigidbody.velocity.magnitude, 2) / 2;
            Vector3 direction2 = airplaneRigidbody.velocity.normalized * -1;
            airplaneRigidbody.AddForce(magnitude2 * direction2);
        }

        private float CalculateLiftCoefficient(float angleOfAttack)
        {
            //Between [-stallAngle, stallAngle], coefficient is determined by linear function
            //Race airplanes use symmetrical airfoils, so at 0 angle of attack lift should be 0
            // y = (y1-0)/(x1-0) * x + 0
            bool stallAngleExceeded = angleOfAttack < -stallAngle || stallAngle < angleOfAttack;
            if (stallAngleExceeded)
                return 0;
            else
                return (maxLiftCoefficient / stallAngle) * angleOfAttack;
        }

        private float CalculateDragCoefficient(float angleOfAttack)
        {
            //Positive quadratic function a(x-h)^2 + k
            //h is 0 since we have minimum drag at 0 stall angle
            //k is min drag coefficient
            //a = (y-k) / x^2 , where (x,y) is (stall angle, drag coefficient at stall angle)
            float a = ((dragCoefficientAtStallAngle - minDragCoefficient) / Mathf.Pow(stallAngle, 2));
            return a * Mathf.Pow(angleOfAttack, 2) + minDragCoefficient;
        }

        public void SetEnabled(bool v)
        {
            airplaneRigidbody.isKinematic = !v;
            enabled = v;
        }
    }
}
