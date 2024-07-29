How do I set it up???
1. Find the camera of choice or take a premade prefab and locate it in the scene and put the CameraLookWithZoom script on it.
2. You are almost done! You can now set the variables
3. Set the Object to track Variable!

Tips:
1: If you are going to put it on an animation DO NOT ANIMATE THE ROTATION. That is done by the script.
2: It is robust but do not edit the cameras rotation or fov. This may cause errors/glitches with scripts fighting over the rotation/fov.

What do the variables do?
1. minzoomdistance = the length to start zooming at.
2. maxzoomdistance = the farthest length away to zoom.

3. minzoomfov = the fov to be at when the obkject is at or under min zoom distance
4. maxzoomfov = the fov to be at when the object is at or farther than the max distance

5. Zoom Lerp Speed = is how fast it zooms to desired fov

6. Debug Mode = when enabled displays errors and data in debug.

7. Speed = how fast the camera pans to object

8. Auto Speed If Object Out Of View = multiplyes speed of pan by speed multiplyer if object is out of view
9. speed multiplyer = explained in number 8.

10. sensitivity:
---RULES to follow---
- the closer to zero the more often it pans.
- 0 = when an object moves it moves
- 1 = when the object is out of screen then move

11. EnableObjectData
- Tells the program to write data about the object
A. Float - ObjectDistance Distance from camera to object
B. Bolean - canSee - can the camera see the object
	- Note: It will say the object is not seen if it is even slightly obstructed. This is why you only change camera if seenLast is greater than a set time
C. Float - seenlast - time in seconds from when the object has been seen.

Object Data is great because you can take the bool has seen and if it is not true and seenLast is greater than 1 second then you can switch the camera!

If you have any questions or concerns please contact me at my contact email found on the asset page/my publisher page.