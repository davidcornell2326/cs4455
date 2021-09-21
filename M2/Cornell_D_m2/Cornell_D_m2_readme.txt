David Bennett Cornell
dcornell3@gatech.edu

Main scene: start (which automatically loads "demo" after 3 seconds)

I had an issue where physics would simulate for a second or so before the scene was visible (in the build), EVEN if I set the time scale (and even gravity) to 0 in awake() of the FIRST frame.
So, I added an empty dummy scene called "start" that will be on screen for 3 seconds and then load the demo scene.
The code is on the DelayStart game object in "start" (DelayStart.cs).

All required features should be visible from the initial camera position, but if not, you can move the players.