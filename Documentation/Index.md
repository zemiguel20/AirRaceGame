# Documentation

## Index


1. [General Notes](#GeneralNotes)
   1. [Setting references using methods like GameObject.Find](#SettingReferences) 
2. [Player](#Player)
    1. [Player Movement](#PlayerMovement)
        1. [Movement Input](#MovementInput)
        2. [Physics Calculations](#PhysicsCalculations)
        3. [Plane Rotation](#PlaneRotation)
        4. [Plane Lift](#PlaneLift)
    2. [Plane Colliders](#PlaneColliders)
3. [Race](#Race)
    1. [Goals](#Goals)
    2. [Race Manager](#RaceManager)
    

## 1. General Notes <a name="GeneralNotes"></a> <a href="#Index" style="font-size:13px">(index)</a>

##### Setting references using methods like GameObject.Find <a name="SettingReferences"></a>

As recommended in the [*Awake* documentation](https://docs.unity3d.com/2021.1/Documentation/ScriptReference/MonoBehaviour.Awake.html),
setting up references between GameObjects using methods such as *Find* should be done in the *Awake* function. <br>
This is mostly applied when setting a reference to a Manager object.


## 2. Player <a name="Player"></a> <a href="#Index" style="font-size:13px">(index)</a>

### 2.1 Player Movement Controller <a name="PlayerMovement"></a> <a href="#Index" style="font-size:13px">(index)</a>

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

#### 2.1.1 Movement Input <a name="MovementInput"></a> <a href="#Index" style="font-size:13px">(index)</a>


Using the Input System, a InputAction is created with the input configuration for the Movement
and Acceleration.

The main movement controlls will consist of an axis for the elevators (turning up and down), 
an axis for ailerons (rotating sides), and an axis/button for acceleration too.

(images below are just example)

![input_action](./PlayerMovementImages/input_action_movement.png)

Then a PlayerInput component is added to PlayerController and callbacks are binded to both Actions.



![player_input_bind](./PlayerMovementImages/player_controller_input_bind.png)

![callbacks](./PlayerMovementImages/movement_callbacks.png)



#### 2.1.2 Physics calculations <a name="PhysicsCalculations"></a> <a href="#Index" style="font-size:13px">(index)</a>

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


#### 2.1.3 Plane Rotation <a name="PlaneRotation"></a> <a href="#Index" style="font-size:13px">(index)</a>


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

The velocity of the plane is the second strength factor. The velocity is divided by a velocity threshold,
and the result is clamped between 0 and 1.
This threshold is the velocity at which the rotation speed is max.

Also, there is a multiplier named rotationSpeed.


#### 2.1.4 Plane Lift <a name="PlaneLift"></a> <a href="#Index" style="font-size:13px">(index)</a>

The lift strength will be based on 2 factors

- Velocity
- Z axis angle. 

The formula is 
```csharp
Vector3 force = baseForce * velocityFactor * inclinationFactor;
```
where *baseForce* is the inverted gravity.

The velocity is divided by a velocity threshold, and the result is clamped between 0 and 1.
This threshold is the velocity at which the lift strength is max.

The inclination factor is calculated by a formula with this form
```
y = 0.0000205761 * (x-180)^2 + 0.333333
```
![rotation-lift-graph](./PlayerMovementImages/rotation_lift_graph.png)

where *x* is the angle and the *y* is the factor.

### 2.2 Plane Colliders <a name="PlaneColliders"></a> <a href="#Index" style="font-size:13px">(index)</a>

The plane has two colliders:

- Body Collider
- Wings Collider

![body collider](./PlaneColliderImages/body_collider.png)

![wings collider](./PlaneColliderImages/wings_collider.png)

The BodyCollider has a tag *GoalHitter* which is used in the collision with the Goals.

## 3. Race <a name="Race"></a> <a href="#Index" style="font-size:13px">(index)</a>

Each scene is a race/map, which has a path.

A path is a group of goals/checkpoints with an order.
The player has to pass through the goals in the order they are defined.

The way the points are tracked is by the time the player takes to reach from 
one goal to another. Each segment has a limit duration. A timer tracks the time passed until the next
goal is reached. The less the time taken the more points. If limit duration is reached, no points are gained
for that segment.

The tracking of the goals passed and the points gained is done by the Race Manager.


### 3.1 Goals <a name="Goals"></a> <a href="#Index" style="font-size:13px">(index)</a>

![goal prefab](./RaceImages/goal_prefab.png)

A goal is composed by a particle system which gives the ring visual, and a trigger collider with circle shape.

Also it has a script which implements the response to the trigger. If the object thats colliding with the trigger
has the tag *GoalHitter* then it calls the callback function of the associated RaceManager.
```csharp
private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoalHitter")
            raceManager?.OnGoalHit();
    }
```

The RaceManager dependency is set at startup with *Awake*
```csharp
private void Awake()
{
    raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
}
```

### 3.2 Race Manager <a name="RaceManager"></a> <a href="#Index" style="font-size:13px">(index)</a>

RaceManager is a simple object with a script attached. <br>
It will track the race from start to finish, managing the goals and tracking the score.

#### 3.2.1 Race Path <a name="RacePath"></a> <a href="#Index" style="font-size:13px">(index)</a>

The RaceManager has a ordered list of Goals. This list is made public to the inspector, so the list can be
edited there.

![goal_list](./RaceImages/race_manager_goal_list.png)

Goals from the scene are dragged in the list and can be ordered in any way.

#### 3.2.2 Race Start <a name="RaceStart"></a> <a href="#Index" style="font-size:13px">(index)</a>

To start the race, StartRace method needs to be called by the Game Manager. <br>

This method does some initial setup and starts counting time.

First it resets some variables and sets *raceStarted* flag to true. 
```csharp
raceStarted = true;
goalsPassed = 0;
timeCounter = 0;
score = 0;
```
This flag enables the *Update* method to
start counting the time.
```csharp
    private void Update()
    {
        if (raceStarted)
        {
            timeCounter += Time.deltaTime;
        }
    }
```

Then it deactivates all Goal gameobjects *except the first one*.
```csharp
for (int i = 1; i < goals.Count; i++)
{
    goals[i].gameObject.SetActive(false);
}
```

#### 3.2.3 Passing Goals <a name="PassingGoals"></a> <a href="#Index" style="font-size:13px">(index)</a>

When a Goal is passed through, it triggers a call to *OnGoalHit* method.

Based on the time taken to reach this Goal, points are calculated and added to score.
Then, the time counter is reset.

The goal just passed is deactivated using *GameObject.SetActive* method.

Then, if there are still more goals to pass, the next Goal in the list is activated using the same method.

If not, then game is finished, and the Game Manager is notified.

#### 3.2.4 Score <a name="Score"></a> <a href="#Index" style="font-size:13px">(index)</a>

When a goal is reached, we calculate the difference between the time taken to reach the goal and the Time Limit.
For every decisecond less than the Time Limit, a point is gained.

If Time Limit is reached then no points are gained.

Time Limit can be changed in the editor.
- [State Pattern](https://refactoring.guru/design-patterns/state)
