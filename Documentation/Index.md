# Documentation

## Index

 
1. [Architecture](Architecture/architecture)
2. Player
    1. [Airplane Movement](Player/plane_movement.md)
    2. [Input](Player/input.md)
    3. [Plane Colliders](Player/plane_colliders.md)
    4. [Player Camera](Player/player_camera.md)
    5. [Hit terrain and respawn](#Respawn) 
3. [Race](#Race)
    1. [Race State/Flow](#RaceState)
    2. [Game Manager](#GameManager)
    3. [Goals](#Goals)
    4. [Path Manager](#PathManager)
    5. [Chronometer](#Chronometer)
    6. [Leaderboard](#Leaderboard)
    7. [Pausing the game](#Pausing)
4. [UI](#UI)
   1. [UI Prefab](#UIPrefab)
   2. [Waypoint](#Waypoint)
5. [Main Menu](#MainMenu)


## Hit terrain and respawn <a name="Respawn"></a> <a href="#Index" style="font-size:13px">(index)</a>

A script *PlayerRespawner* is added as a component to the Plane. When a TerrainHit event happens, it triggers the player *Respawn* as a coroutine.

```csharp
private void OnTerrainHit()
        {
            StartCoroutine(Respawn());
        }

private IEnumerator Respawn()
        {
            _airplaneController.EnablePhysics(false);

            yield return new WaitForSeconds(0.3f);

            _airplaneController.SetPlanePositionAndRotation(new PositionRotationTuple(respawnPoint, respawnRotation));

            yield return new WaitForSeconds(0.3f);

            _airplaneController.EnablePhysics(true);

        }
```
First, forces on the plane are disabled.
Then it stops for a small time for the player to recognize it collided and his going to respawn.
Then the player position and rotation are set to the ones saved as variables and stops for a small time again for the
player to prepare. Finally enables forces on the plane again.

The saved respawn position and rotation start off as the starting point of the race.

This component listens for the *GoalPassed* event, and when raised, the respawn transform is updated to the passed Goal transform.

```csharp
public void UpdateRespawn(GameObject goal)
        {
            respawnPoint = goal.transform.position;
            respawnRotation = goal.transform.rotation;
        }
```


## Race <a name="Race"></a> <a href="#Index" style="font-size:13px">(index)</a>

Each scene is a race/map, which has a path.

A path is a group of goals/checkpoints with an order.
The player has to pass through the goals in the order they are defined.

A chronometer tracks the time passed from start of the race until the last goal is reached.

Each map has a leaderboard of the top player times.

### Race State/Flow <a name="RaceState"></a> <a href="#Index" style="font-size:13px">(index)</a>

The game flow is divided into a sequence of phases.

![states](./GameStateImages/state_diagram.png)

The game starts on the *Countdown* phase, where only a countdown happens to let the player prepare.

Then it transitions to the Race phase, where the player can move around the map and the time and score start counting.

Then when the player finishes the race, it transitions to the End Game phase, where the leaderboard is updated and is displayed in a panel.

At any moment, the game can also be Paused and Resumed.

To coordinate the flow of the events of the game, a GameManager is used.

### Game Manager <a name="GameManager"></a> <a href="#Index" style="font-size:13px">(index)</a>


The GameManager controls the flow of the race.

It starts by starting the countdown. Airplane movent is disabled during this phase.

```csharp
private void StartCountdown()
        {
            _airplaneController.EnablePhysics(false);

            StartCoroutine(_timer.StartTimer(5));
        }
```
When the *TimerEnded* event is raised, then it starts the race. The race path is initialized, the chronometer is started and the airplane movement is activated.

```csharp
private void StartRace()
        {
            _chronometer.StartChrono();
            _pathManager.StartPath();

            _airplaneController.EnablePhysics(true);
        }
```

During the race, when a Goal is passed, a *GoalPassed* event is raised. GameManager listens for this event and tells the PathManager to change the active goal. If this was the last goal and the race is finished, then it changes to the EndGame phase.

```csharp
private void OnGoalPassed(Goal goal)
        {
            _pathManager.ChangeActiveGoal();

            if (_pathManager.IsFinished())
            {
                EndRace();
            }
        }
```

When the game ends, the chronometer and the airplane movement are both stopped. Then the leaderboard is updated and saved to a file.

```csharp
private void EndRace()
        {
            _chronometer.StopChrono();
            _airplaneController.EnablePhysics(false);

            //Save leaderboard
            _leaderboard.AddEntry(_chronometer.time);
            SaveManager.SaveLeaderboard(_leaderboard.ToSerializable(), _leaderboard.name);

            RaceEnded?.Invoke();
        }
```


### Goals <a name="Goals"></a> <a href="#Index" style="font-size:13px">(index)</a>

![goal prefab](./RaceImages/goal_prefab.png)

A goal is composed by a particle system which gives the ring visual, and a trigger collider with circle shape.

Also it has a script which implements the response to the trigger. If the object thats colliding with the trigger
has the tag *GoalHitter* then it raises a *GoalPassed* event.
```csharp
private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalHitter"))
        {
            GoalPassed?.Invoke(this);
        }
    }
```

### Path Manager <a name="PathManager"></a> <a href="#Index" style="font-size:13px">(index)</a>

Component that manages a list of Goals during the race.

The GameManager uses this component to control the race path during the race.

###### Race Path

The PathManager has a ordered list of Goals. This list is made public to the inspector, so the list can be
edited there.

![goal_list](./RaceImages/race_manager_goal_list.png)

Goals from the scene are dragged in the list and can be ordered in any way.

###### Initialize Path

Starts by deactivating all Goals in the list, and then Activates the first one.

```csharp
public void StartPath()
        {
            //Turns off goals
            foreach (Goal goal in goals)
            {
                goal.gameObject.SetActive(false);
            }

            currentGoalIndex = 0;
            SetGoalStatus(currentGoalIndex, true);
        }

(...)

private void SetGoalStatus(int index, bool status)
        {
            goals[index].gameObject.SetActive(status);
        }
```


###### Change active goal

When called, if the path isnt yet finished, the current goal is deactivated and current index is incremented.

For the new current index, if the path isnt yet finished it activates the goal on the current index.

```csharp
public void ChangeActiveGoal()
{
    if (currentGoalIndex < goals.Count)
    {
        SetGoalStatus(currentGoalIndex, false);
        currentGoalIndex++;
        GameLogger.Debug("Goal passed! Num of goals passed: " + currentGoalIndex);
    }

    if (currentGoalIndex < goals.Count)
    {
        SetGoalStatus(currentGoalIndex, true);
    }
}
```

### Chronometer <a name="Chronometer"></a> <a href="#Index" style="font-size:13px">(index)</a>

Chronometer is a component that counts time passed by incrementing a variable, if active.

```csharp
// Update is called once per frame
    void Update()
    {
        if (active)
        {
            time += Time.deltaTime;
        }
    }
```
It has 2 methods, to Start and Stop the chronometer.

```csharp
public void StartChrono()
        {
            active = true;
        }

public void StopChrono()
        {
            active = false;
        }
```

### Leaderboard <a name="Leaderboard"></a> <a href="#Index" style="font-size:13px">(index)</a>

Each race map has a Leaderboard of the best times.

This will be a data container that should be accessable both in the Main Menu scene and the Race scene itself,
so it will be a ScriptableObject.

```csharp
 [CreateAssetMenu(menuName = "ScriptableObjects/Leaderboard")]
    public class LeaderboardSO : ScriptableObject
    {
        [SerializeField]
        private int LEADERBOARD_SIZE = 10;

        [SerializeField]
        private List<float> _leaderboard = new List<float>();

        public void AddEntry(float time)
        {
            _leaderboard.Add(time);

            _leaderboard.Sort(); //default is ascending order

            // Trims to SIZE
            if (_leaderboard.Count > LEADERBOARD_SIZE)
            {
                _leaderboard = _leaderboard.GetRange(0, LEADERBOARD_SIZE);
            }
        }

    (....)
    }
```

Also we have the *LeaderboardSerializable*, a class marked as *Serializable*, which represents the
Leaderboard in a form that can be *persisted in a binary file* by the *SaveManager*.


### Pausing the game <a name="Pausing"></a> <a href="#Index" style="font-size:13px">(index)</a>

The [player Input action map](#Input) has a keybind for pausing and unpausing the game.

The GameManager switched between the two, and the time scale and paused flag are set accordingly.

As said in the [documentation](https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Time-timeScale.html),
the time scale *"is the scale at which time passes"*. 

*"When timeScale is 1.0, time passes as fast as real time." ... 
"When timeScale is set to zero your application acts as if paused if all your functions are frame rate independent."*. 

Also, when the game is paused, a PauseMenu is displayed.

## UI <a name="UI"></a> <a href="#Index" style="font-size:13px">(index)</a>

For developing runtime UI, unity recommends using one of its core packages, *Unity UI*, and so thats the
one that is used.

Documentation pages talking about the different UI packages:
- [Creating user interfaces (UI)](https://docs.unity3d.com/2021.1/Documentation/Manual/UIToolkits.html)
- [Comparison](https://docs.unity3d.com/2021.1/Documentation/Manual/UI-system-compare.html)

Documentation pages for Unity UI:
- [Unity UI](https://docs.unity3d.com/2021.1/Documentation/Manual/com.unity.ugui.html)

Also, TextMeshPro, a unity standard package, will be used to build certain UI elements on top of UnityUI.

### UI Prefab <a name="UIPrefab"></a> <a href="#Index" style="font-size:13px">(index)</a>

Every race scene will have a similar core set of game objects, like the Managers. One of the is them UI prefab.

![ui_prefab](./UIImages/ui_prefab.png)

The base UI element is the [*Canvas*](https://docs.unity3d.com/2021.1/Documentation/Manual/UICanvas.html).
It offers a space where more UI elements can be added as childs of the Canvas, creating a more complex UI object,
and also defines how the object itself will be rendered.

We can have multiple Canvas objects, each with different UI elements, and displayed in a different form, but
the elements of our game will be displayed in *screen overlay mode*, so only *one* canvas will be used.

As child elements of this Canvas, we have the *CountdownTimerUI*, *EndGamePanel* and the *HUD*.

Also, we have the default created EventSystem object, which manages the input events for the UI, using the Unity's
Input System UI Input Module.

For the Scaling of the Canvas, we put the option *UI Scale Mode* in *Scale With Screen Size*, for simple scaling across
multiple resolutions.

There is also a UI script component attached to the root object, which other objects like GameManager can use was an interface
to manage the UI elements.

### Waypoint <a name="Waypoint"></a> <a href="#Index" style="font-size:13px">(index)</a>

Waypoint is an arrow with its own camera. This camera renders its image to an asset of type *Render Texture*. A Waypoint layer is used so that
only the waypoint is rendered by this camera.

Then a component in the HUD is added which displays this Render Texture.

The waypoint points the location of the current Goal relative to the Player Camera's local transform. 

## Main Menu <a name="MainMenu"></a> <a href="#Index" style="font-size:13px">(index)</a>

The Main Menu scene is the first scene that opens when the game is launched.

It has a canvas with a background color and the various panels.

This menu is has 2 button elements with text. One is *Play* and the oher one *Quit*.

Play opens a Map selector, with box buttons for each map.

When a map is selected, the panel for that map is shown, which shows a preview photo of the map and the leaderboard for that map.
If the Play button on this panel is clicked, the Scene of the map is loaded.

