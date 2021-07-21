# Loading Map Info

Each map should have a data container with its information, namely the name and leaderboard values.
This editor should be used to create and edit the containers.

This info is loaded at startup.

Leaderboards can be updated during runtime and persisted for loading in the future.

---

`GameManager` is a script deriving from Unity `MonoBehaviour`, and can be attached to a game object.

`MapInfo` is a `ScriptableObject`, a Unity class useful to create scene independent game objects that can be used as data containers, that can also be created and edited in the editor and live as assets in the project.
It has the map name, preview image, leaderboard, scene, path, player starting position. <br>
The `Leaderboard` is a data structure for managing an array of player time values, which can be updated during runtime with new values.

Everything in `MapInfo` is constant except the Leaderboard.

Each map will have its own `MapInfo` object instance loaded with predefined constant info.

The `Leaderboard` values are loaded from previously persisted data in files.

On start, `GameManager` bootstraps the `MapInfo` leaderboards for each map using the `SaveManager`. 

---

`MapInfo` has an C# attibute from Unity, `CreateAssetMenu`, that makes possible to create `MapInfo` instances through the Create Asset Menu in the Editor, which live as asset files in the project.
`MapInfo` constant variables are marked with `SerializeField` attribute so they can be changed in the Editor, and are automatically serialized by Unity.

The `GameManager` map list has a `SerializeField` attribute so the list can be edited in the editor.

Leaderboards are persisted to a binary text file using a `BinaryFormatter`.