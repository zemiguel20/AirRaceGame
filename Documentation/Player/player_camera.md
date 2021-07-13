# Player Camera

The camera that renders the scene should follow the player during the race.

---

In Unity, the camera is represented by a game object in the scene with a camera component attached.

A script component can be attached to the camera game object that controls the Transform component.
It takes in a reference of the airplanes Transform, which uses as target.
Each frame, the cameras position is interpolated between the current position and the airplane position with an offset in local space specified by parameter.
Then, the camera is rotated in order to be looking at the airplane.

---

As an interpolation function for camera follow, Unity recommends using the 
[SmoothDamp function](https://docs.unity3d.com/2021.1/Documentation/ScriptReference/Vector3.SmoothDamp.html).

The target position is the player position with an offset *in local space*. The offset is specified in the editor's inspector.
```csharp
Vector3 target = player.position + player.up * offsety + player.forward * offsetz;
```

Then we set the camera position using SmoothDamp.
```scharp
this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref velocity, smoothTime);
```

Then we use *LookAt* function to rotate the camera to look at the player.
```scharp
this.transform.LookAt(player);
```
