# Pause and Unpause game

During the race the player can pause and unpause the game.

While the game is paused, a menu should appear where the player can exit, restart, or resume the race.

---

RaceController starts the race phase, it subscribes to InputController's PauseInputTriggered event.
When pause input is triggered, RaceControllers calls the PauseResumeGame method, which depending on the IsPaused flag value, the time scale is changed to 0 (stopped) and a GamePaused event is raised, or 1 (normal) and a GameResumed event is raised.
PauseMenu listens for both this events. On GamePaused, it enables the menu, and on GameResumed it disables the panel.

PauseMenu is a panel in the UI canvas, and has a script.
It has 3 buttons.
The Resume button calls PauseResumeGame method, which should resume the game.
The Restart button calls the RestartRace method in RaceController, which raises the RaceRestarted event, to which GameManager listens.
The Restart button calls the ExitRace method in RaceController, which raises the RaceExited event, to which GameManager listens.

---