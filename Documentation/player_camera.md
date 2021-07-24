# Player Camera

The position of the camera that renders the scene should follow the player's position during the race.

---

In Unity, the camera is represented by a game object in the scene with a camera component attached.

A component is attached to the camera game object that controls the Transform component.
It takes in a reference of the airplanes Transform, which uses as target.
Each frame, the cameras position is interpolated between the current position and the airplane position with an offset in local space specified by parameter.
Then, the camera is rotated in order to be looking at the airplane.
The interpolation of the camera position uses a damping functions.

---

Using Unity's Cinemachine module, a CinemachineBrain component is attached to the Camera game object which controls the Camera position.

The CinemachineBrain has a reference to a VirtualCamera object which uses as target, and interpolates the Camera position to the VirtualCamera position.

The movement properties are specified by the target VirtualCamera.

In the VirtualCamera, the target Transform is set to the Airplane.
The Aim is set to always look at the Airplane.
The Body, which defines the translation properties, has some damping and an offset specified.