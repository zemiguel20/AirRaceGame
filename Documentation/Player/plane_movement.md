# Plane Movement

The plane movement will be based on a simplified model of the [physics of a plane](plane_physics.md). <br>
There is no representation of air on the game. <br>
The air density will be a constant with value [1.225 kg/m^3][2]. <br>
There are no wind zones, so windspeed is always 0. The relative velocity (V) will be equal to plane velocity. <br>
Both lift and drag coefficients will be calculated based only on the angle of attack. <br>
Thrust will be a percentage of a manually defined value.
No representation of control surfaces, so torque is applied

Also, the plane should expose an interface methods for the input system to interact with the plane movement.

---

AirplaneController class is a script that controls the movement of the plane and provides an interface to set input multipliers.

It uses the AirplanePhysics class, responsible for the physics calculations and 
Physics calculations take into account multiple parameters, which come in a PlaneProperties data container, as well as input multipliers passed on by the AirplaneController.

![class diagram](MovementImages/plane_movement_class_diagram.png)

List of plane properties:
- Max acceleration
- Roll force multiplier
- Pitch force multiplier
- Yaw force multiplier
- Stall angle
- Max lift coefficient
- Min drag coefficient
- Max drag coefficient
- Wing area

Thrust force will be defined by `Force = mass * max acceleration * throttle percentage`, which is applied directly on the plane´s forward direction.
The throttle percentage is changed with input by AirplaneController, which passes this percentage to AirplanePhysics when forces are updated.


The drag and lift coefficient curves will be based on [symmetrical airfloils][3].

To calculate the angle of attack, we calculate the angle between the planes forward vector and the velocity vector.


For the drag, we can use a sine function model. We will have the parameters: minimum drag coefficient (MinDC) and maximum drag coefficient (MaxDC). <br>
The function can be expressed as `DragCf = (MaxDC - MinDC) * sin(angle of attack) ^2`

![drag curve](MovementImages/functionsDrag.png)

For the lift coefficient, we can use different linear functions, depending on the angle.
We have the parameters: maximum lift coefficient, and stall angle, at which lift coefficient is max.

We create a table with the peak points. Then we can find between what 2 X values our angle of attack is, get the Y values and find our angle of attack's Y value by interpolation.

![lift curve](MovementImages/functionsLift.png)


For the rotations, each of the 3 torques have a direction depending on the corresponding axis, and magnitude depends on the force multipler, the input multiplier and the plane velocity.

---
AirplanePhysics applies the forces to a Rigidbody component through its API.
This is done every physics step as explained in the [Rigidbody documentation][1]. So AirplaneController calls AirplanePhysics update forces every FixedUpdate.

PlaneProperties is a ScriptableObject. This way multiple configurations can be created for different planes.

Both the plane Rigidbody and the PlaneProperties are injected in the AirplaneController using the inspector, which then it passes to the constructor of the AirplanePhysics.

Unity's physics system allows gravity to be simulated on any Rigidbody, as long as they have *Use Gravity* flag set to true. <br>
![use_gravity](MovementImages/use_gravity.png)


[1]: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Rigidbody.html
[2]: https://en.wikipedia.org/wiki/Density_of_air
[3]: http://airfoiltools.com/search/index?m%5BmaxCamber%5D=0&m%5Bsort%5D=5