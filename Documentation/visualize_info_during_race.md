# Visualize info during race

During the race, the player should see the info on the UI:
- Countdown text.
- HUD with velocity, altitude, chronometer time, throttle percentage and an waypoint to the current goal.
- End game panel with player time and the updated leaderboard.

---

When GameManager is in the MapSceneState, it also loads the UI scene which has the UI components needed for the race and initializes them.

The components are layed out in a canvas object.

The CountdownTimerUI is a object with a text component and a script.
When initialized, is subscribes to CountdownStarted and CountdownFinished events in RaceController.
On CountdownStarted, CountdownTimerUI reads the remaining time in the Timer in RaceController and updates the label text.
On CountdownFinished, CountdownTimerUI stops reading the Timer value and displays a "GO" message for a short time and then stops displaying by disabling the object itself.

The HUD is a group of objects used to display information during the race.
There are text components for the plane velocity, altitude, chronometer time and throttle percentage.
The HUD script component updates this components text constantly with the values from RaceController and Airplane.

The Waypoint is part of the HUD, and is an object with an arrow shape that rotates as the plane moves in order to point to the current goal.
It needs the player Transform, the player Camera and the Path.
It gets the current goal position and the player position, both relative to the camera Transform, because it depends on the players point of view
The Waypoint acts as if in the same position as the player.
The difference between both positions gives the point relative to the Waypoint position at which the Waypoint should be pointing at.

The EndGamePanel is an object with a script that has a reference to the RaceController and the map's Leaderboard, and 2 text components, one for the player time and another for the leaderboard.
It subscribes to RaceEnded event in RaceController.
When raised, EndGamePanel gets the player time and leaderboard and displays the text components.

EndGamePanel has a Exit button, which when pressed calls the RaceController.ExitRace method which raises the RaceExited event.
The GameManager listens for this event to switch back to the MainMenu.

---

The Waypoint has its own camera that renders the Waypoint to an image. That image is displayed in the HUD.