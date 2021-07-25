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