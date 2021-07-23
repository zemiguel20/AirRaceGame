# Architecture

The engine used is Unity, where games can be divided into multiple loadable scenes. Scenes can be loaded individualy,
where the current one is replaced with the loaded one, and each scene is isolated. They can be also loaded additively, where
scenes can be loaded on top of the current one, and objects in different loaded scenes can communicate.

There will be a scene for each map.

