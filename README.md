# Panteon-Demo-Project  
This project is a 3rd Person Runner Game created as a demo project for the company Panteon Games.  
  
Unity Version: 2019.4.17f1  
Build Resolution: 585x1266  
  
This document will explain the how certain prefabs are intended to be used.  
  
### Game Play
Game is played with standard W,A,S,D keys.  
In the built up version, cheat is enabled.   
Shift Key: Reduced gravity  
Space w Shift: Fly up.  
  
## Task 1 - Mandatory Obstacles  
Each variable that might create confusion has ToolTip explanation.  
  
### Static Obstacle  
These are just static obstacles with Rigidbody and Collider attached. Nothing Special.  
  
### Moving Obstacle  
To use these efficiently to create new levels for this game  
 1. Drag and drop the prefab into hierarchy  
 2. Here you will see the prefabs contains two children named "MovingObstacle" and "Paths"  
   
This obstacle type will follow the given list of paths in index order.   
 3. Dublicate "path" gameObject inside Paths and create the desired path.  
 4. Reset to put the MovingObstacle.cs script component on MovingObstacle child object to assign the paths in order, or place them in a specific order as you wish into the list  
 5. Done!  
  
When the game's started the MovingObstacle will circle the path until the game is over.  
You can specify it's movement speed, and the ExplosionForce variables that will be executed for the player on collision.  
  
## Task 1 - Bonus Obstacles  
Each variable that might create confusion has ToolTip explanation.  
### Rotating Platform  
Simply place into hierarchy and assign the rotationg speed AND the colliders that might block the platform's rotation.  
  
### Half Donut  
Simply place into hierarchy and assign the timers, speed and the range of the stick on MovingStick child object.  
  
### Rotator  
This obstacle works similar to moving obstace. After placing one into hierarchy;  
 1. Dublicate the child RotatingStick object as many as you want  
 2. RESET the Rotator.cs script component to fix the stick's position  
  
Just like MovingObstacle, you can specify it's movement speed, and the ExplosionForce variables that will be executed for the player on collision.  
  
// I have tried to automize using executions in edit mode, but OnValidate did not allow Destroy or DestroyImmidiate. So instead of spending more time, i kept it as it is.  
  
## Task 2 - Paint a Wall  
All that needs to be done is to put one into scene, the player object will detect and assign the necessary variables on Start.  
if there is Wall on the scene, the game will end after the wall is painted,  other wise, the game will finish once the player steps on Finish Area.  
  
The Percentage of the painted wall will be updated every 0.8 seconds for performance, but this can be made faster by using the component.  
  
## Task 3 - Opponent Players  
Currently implementing mlagents to complete this task... X  
mlAgent training abandoned due to lack of time, the training and testing process takes way to long for a simple demo.  
  
The AI made with a simple random path generator kind of algorithm.  
  
  
  
  
### Known Issues  
1- When tested on a pc with spesifics:  
 Windows 8.1, Intel Core i5-4200M, Nvidia GeForce 710M.  
The game has issues where when the player gets stuck in a position, the Player objects rotations are shifting (even though the Rigidbody has frozen those values)  
This issue was never occured in any other device (6 different computers)  



