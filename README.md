# 📄 About
Wanna race solo or with a friend? Here is a game for you! This is a small racing game with 3 maps you could play. Boost your way to the end with fuels! 


<br><br/>
# 🏎️ Gameplay
Try out the game, turbo included! 🔥
Add gifs here


<br><br/>
# ⚙️ Game mechanics
- You accelerate/decelerate and steer the car like in real life and complete laps.  
- There’s a boost system where players can burn fuels to gain bonus speed and acceleration.  
- Players can gain fuel by hitting pickups within the race tracks.
- A player wins the game when they completed a determined amount of laps.

<br><br/>
# 🔁 Game loop
1. Game starts.
2. Countdown begins.
3. Players race.
    - Players can do pickups to gain fuel
    - Players can use fuels to boost their speed
6. Players win when they reach the finish line. 


<br><br/>
# 🎮 Playing The Game
### How to load the game
Currently there are no builds for the game. The game can only be played within the Unity Editor.  
Requirements:
- Unity Editor version 2022.3.10f1 LTS.
- Packages used:
    - Input System 1.7.0 (From Unity Registry)

Additional details:
- There's currently only 1 main menu scene and 3 game scenes. They are located in the [Assets/Scenes](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scenes) directory.
- The project is structured in a way which you can start playing in whichever scene you load
    - As long as the scene contains RaceTrackTileMap and some other stuff (EDIT THIS)
    - If you are starting the game in one of the game scenes instead of the main menu scene, the game scene needs to have a game manager prefab that contains settings you can alter like game modes, and number of laps to win. (EDIT THIS)


### Player Controls
There are currently two modes in the game: **One Player**, **Two Player**
| Actions | One Player | Two Player (Player 1) | Two Player (Player 2) |
| --- | --- | --- | --- |
| Accelerate/Deaccelerate | W/S | W/S | Arrow Up/Down |
| Steer Left/Right | A/D | A/D | Arrow Left/Right |
| Boost | C | C | Right Shift |
| Pause Game | Esc | Esc | Esc |


<br><br/>
# 🧭 Navigating the project
To learn more about the code I’ve written, navigate it through [Assets/Scripts](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scripts).
From there, the scripts are organized into different folders.
- [Core](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scripts/Core): The core gameplay scripts such as car movement, input system, fuel and finish line.
- [Managers](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scripts/Managers): The managers of the game. Contains code involving scene loading and managing game loop
- [UI](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scripts/UI): Everything that has to do with UI. Main menu, pause menu, end game menu, etc.
- [Visuals](https://github.com/proxima-k/fg-car-racer/tree/main/Assets/Scripts/Visuals): Contains code that helps with the visuals of the game.

<br><br/>
# 💭 Thought process / Lessons



<br><br/>
# 🗃️ Resources
Here are the resources that helped me in the making of this project:

<br><br/>
Developed by Kent Chua
