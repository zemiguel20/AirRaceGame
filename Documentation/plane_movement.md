# Plane Movement

The plane movement will be based on a simplified model of the [physics of a plane](plane_physics.md). <br>
There is no representation of air on the game. <br>
The relative velocity (V) will be equal to plane velocity. <br>
Both lift and drag coefficients will be calculated based only on the angle of attack. <br>
No representation of control surfaces, each force is represented by only one respective vector.

Also, the plane movement is influenced by input.

The player should be able to use controllers like keyboard and gamepad for input actions.

---

`Airplane` class is a script that controls the movement of the plane.
It uses the `AirplanePhysics` class, responsible for the physics calculations.
Physics calculations take into account multiple parameters, which come in a `PlaneProperties` `ScriptableObject` data container, as well as input values passed on by the `Airplane`.
`Airplane` gets the input values from `InputController`.

For the player input, the Input System module from Unity is used.
The `InputController` gameobject has a `PlayerInput` component from the InputSystem, which using events defines the input values in `InputController`.
The `InputController` implements the `IPlayerInput` interface which defines getter methods for the input values.
The `PlayerInput` component uses a `InputAction` asset, in which are defined a set of Actions.
In the editor the Actions are defined, as well as their return value and the keybind schemes.

`AirplanePhysics` controls a `Rigidbody`, a Unity class that represents a physics gameobject.
The airplane gameobject will have a `Rigidbody` component.

List of plane properties:
- Max acceleration
- Throttle increase per second
- torque multiplier
- Stall angle
- Max lift coefficient
- Min drag coefficient
- Max drag coefficient

Thrust force will be defined by `Force = plane mass * max acceleration * throttle percentage`, which is applied directly on the plane´s forward direction.
The throttle percentage is changed with input by AirplaneController, which passes this percentage to AirplanePhysics when forces are updated.


The drag and lift coefficient curves will be based on [symmetrical airfloils][3].

To calculate the angle of attack, we calculate the angle between the planes forward vector and the velocity vector.


For the drag, we can use a sine function model. We will have the parameters: minimum drag coefficient (MinDC) and maximum drag coefficient (MaxDC). <br>
The function can be expressed as `DragCf = (MaxDC - MinDC) * sin(angle of attack) ^2  + MinDC`

![drag curve](MovementImages/functionsDrag.png)

For the lift coefficient, we can use different linear functions, depending on the angle.
We have the parameters: maximum lift coefficient, and stall angle, at which lift coefficient is max.

We create a table with the peak points. Then we can find between what 2 X values our angle of attack is, get the Y values and find our angle of attack's Y value by interpolation.

![lift curve](MovementImages/functionsLift.png)

The lift and drag magnitude are calculated by `coefficient * relativeVelocity^2`.


For the rotations, each of the 3 torques have a direction depending on the corresponding axis, and magnitude depends on the torque multipler, the input multiplier and the plane velocity.

---