# Loading and Saving Map Info

Each map should have a data container with its information, namely the name and leaderboard values.

This info is loaded at startup.

Leaderboards can be updated during runtime and saved.

---

`GameManager` is a singleton script.
On start, it bootstraps the `MapInfo` data containers for each map. 

Each `MapInfo` has the map name, preview image, leaderboard, scene, path, player starting position.

The `Leaderboard` is a data structure for managing an array of player time values, which can be updated during runtime with new values.

Everything in `MapInfo` is constant except the Leaderboard.

`MapInfo` can be a `ScriptableObject`, a Unity class useful to create scene independent game objects that can be used as data containers.
Each map will have its own `MapInfo` object instance loaded with predefined constant info.

The `Leaderboard` values can be persisted in files.

The loading and saving is done with the `SaveManager`.

---

`MapInfo` has an C# attibute from Unity, `CreateAssetMenu`, that makes possible to create `MapInfo` instances through the Create Asset Menu in the Editor, which live as asset files in the project.
`MapInfo` constant variables are marked with `SerializeField` attribute so they can be changed in the Editor, and are automatically serialized by Unity.

This assets are stored inside a Resources folder so they can be loaded at runtime using `Resources.Load`.

Leaderboards are persisted to a binary text file using a `BinaryFormatter`.