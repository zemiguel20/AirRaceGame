# Documentation

## Index


1. [Player](#Player)
    1. [Player Movement](#PlayerMovement)
        1. [Movement Input](#MovementInput)
        2. [Physics Calculations](#PhysicsCalculations)
        3. [Plane Rotation](#PlaneRotation)
        4. [Plane Lift](#PlaneLift)
    

## 1. Player <a name="Player"></a> <a href="#Index" style="font-size:13px">(index)</a>

### 1.1 Player Movement Controller <a name="PlayerMovement"></a> <a href="#Index" style="font-size:13px">(index)</a>

The player movement is controlled by a PlayerController object with a PlayerController script. 
This will control a plane Rigidbody.

![player](./PlayerMovementImages/player_prefab_tree.png)


This Controller will read player input and control the movement of the plane accordingly.

Since the Gravity and Drag forces are already applied by the physics engine, the script algorithm does the following tasks:

1. Calculate and apply rotations if input given
2. Calculate Thrust
3. Calculate Lift
4. Apply both Thrust and Lift

There is information below that explain this steps.

#### 1.1.1 Movement Input <a name="MovementInput"></a> <a href="#Index" style="font-size:13px">(index)</a>


Using the Input System, a InputAction is created with the input configuration for the Movement
and Acceleration.

The main movement controlls will consist of an axis for the elevators (turning up and down), 
an axis for ailerons (rotating sides), and an axis/button for acceleration too.

(images below are just example)

![input_action](./PlayerMovementImages/input_action_movement.png)

Then a PlayerInput component is added to PlayerController and callbacks are binded to both Actions.



![player_input_bind](./PlayerMovementImages/player_controller_input_bind.png)

![callbacks](./PlayerMovementImages/movement_callbacks.png)



#### 1.1.2 Physics calculations <a name="PhysicsCalculations"></a> <a href="#Index" style="font-size:13px">(index)</a>

First of all, physics calculations are made inside the FixedUpdate function, as recommended 
in https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Rigidbody.html

We multiply our force vectors by FixedDeltaTime, so they are applied "per second", 
instead of per physics frame.

The plane physics ideas came from this videos:

 * [How Do Airplanes Fly?](https://youtu.be/Gg0TXNXgz-w)
 * [Realistic Aircraft Physics for Games](https://youtu.be/p3jDJ9FtTyM) 

For the translation of the plane, we have 4 main forces being applied:

- Thrust
- Drag
- Gravity
- Lift

![plane_forces](./PlayerMovementImages/plane_forces.png)

##### Thrust

Thrust as a force applied always in plane's facing forward direction,
which is the Z axis in **local** space.

The acceleretion is controled by input. 

There is a max acceleration value, and the applied force 
strength is the input strength (0 to 1) multiplied by the max acceleration value.

##### Drag

In order for our plane to have a terminal velocity, we need a Drag force.

This force is built-in the Rigidbody component, we just have to set a strength value.

![drag_value](./PlayerMovementImages/drag_value.png)


##### Weigth/Gravity

This force can be applied automatically by the physics engine by activating one property of the 
Rigidbody component:

![use_gravity](./PlayerMovementImages/use_gravity.png)

##### Lift

This force is an outcome of the aerodynamics of the plane, and its always in 
the opposite direction of gravity.

The strength of the lift depends on the velocity and the rotation of the plane.


#### 1.1.3 Plane Rotation <a name="PlaneRotation"></a> <a href="#Index" style="font-size:13px">(index)</a>


Because the model of the plane is imported, the axis are inverted.
Looking at a circle representing the rotation around an axis:

![circle](./PlayerMovementImages/circle_angles.png)

The ailerons input vector needs to be multiplied by -1, but the elevators are meant to be inverted,
so it stays the same.

The plane is rotated by applying a torque force.

<br>

The direction of the force is given by the following:
```csharp
Vector3 direction = new Vector3(inputElevators, 0, -inputAilerons);
```
The elevators rotate the plane on the *x* axis and the ailerons rotate on the *z* axis, both axis being local.

<br>

Then we apply strength factors to this vector, resulting in the force to apply. <br>
```csharp
Vector3 force = direction * velocityFactor * rotationSpeed;
```
The input vectors already have a strength factor, so the direction vector 
has the input strength factor already.

The velocity of the plane is the second strength factor. Based on a given velocity threshold,
the factor is current velocity divided by this threshold.

Also, there is a multiplier named rotationSpeed.


#### 1.1.4 Plane Lift <a name="PlaneLift"></a> <a href="#Index" style="font-size:13px">(index)</a>



