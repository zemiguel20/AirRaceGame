# Map selection

The player sees a list with all the maps.
When a map is selected, it shows the info of that map.

This requires the [map info to be bootstrapped](loading_map_info.md).

---

The MainMenu is a canvas with multiple different panels.
Each panel is a game object that can be enabled and disabled by the MainMenu.

MainMenu as a MonoBehaviour script attached, with callback methods for switching between the different panels
Also it has an initialize method thats called by the GameManager right after the scene is loaded.
The GameManager passes the maps info list to the MainMenu, and the MainMenu instantiates a MapButton object for every map and passes in the respective MapInfo.

In the Main Menu, if the Play button is pressed, it switches to the Map Selection panel.

The Map Selection panel has MapButton objects in a grid layout, where each button has the name and image of the map.

A MapButton raises an MapSelected event when clicked, and passes the respective MapInfo as an argument.

The MainMenu, which is listening to this event, switches to the Map Info panel, where the image, name and leaderboard of the selected map are shown.
The MainMenu passes the selected MapInfo to the MapInfoPanel.

In this panel there is also a Play button, and if pressed, a PlayMapPressed event is raised, passing the selected map as argument.
MainMenu listens for this event and emits a similar event MapChosen, to which GameManager listens to.
GameManager loads the selected map.

---

