# Scene Management

The game needs to control the transition between the multiple phases and scenes.

---

The Main scene is the first scene loaded and has a game object with a `GameManager` script.

`GameManager` is a script deriving from Unity `MonoBehaviour`, and can be attached to a game object.

`GameManager` has a state machine that follows the [state pattern](https://refactoring.guru/design-patterns/state), where each state represents the active scene. <br>
The base class `SceneState` is an abstract class with an injectable reference to the `GameManager`. <br>
`SceneState` has abstract methods to Load and switch to other scenes. <br>
- Load methods loads and initializes the scene of the active state.
- LoadMainMenu unloads current scene and changes state to `MainMenuSceneState`.
- LoadMap unloads current scene and changes state to `MapSceneState`, passing in the respective `MapInfo`.

To change scene, `GameManager`.ChangeScene is called, passing as argument the new `SceneState`, and calling the Load method of the new state.

The loading and unloading of scenes is made using Unity's `SceneManager`.

At startup, `MainMenuSceneState` is set as initial state.

---

