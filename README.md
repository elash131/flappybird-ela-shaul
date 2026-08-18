# Flappy Bird

A Flappy Bird clone made in Unity.

You control a bird that keeps falling down. Tap to make it fly up, and pass
through the gaps between the pipes. Every pipe you pass gives you one point.
If you hit a pipe or the ground, the game is over.

## What you need

- Unity **6000.3.20f1** (Unity 6)

Please use this exact version. An older Unity may ask to upgrade the project
and can show errors.

## How to run it

1. Clone this repository.
2. Open Unity Hub, click **Add**, and choose the project folder.
3. Open the project. The first time takes a few minutes, because Unity builds
   its `Library` cache. This is normal.
4. In the **Project** window at the bottom, open `Assets/Scenes/MainScene.unity`.
5. Press **Play**.

The game is designed for a 9:16 portrait screen. Press **Play** - the camera
locks to that ratio at runtime.

Note: a fresh clone does not open the scene by itself, so step 4 is needed.
The `Library` folder is not saved in Git, and that folder is what remembers
the last open scene.

You can also use **File > Build and Run**. `MainScene` is already set as the
first scene in the Build Settings.

## How to play

| Screen | What to do |
| --- | --- |
| Start screen | Click the button to start the game |
| While playing | Press **Space** or **left click** to fly up |
| Game over | Press **Space** or **left click** to go back to the start screen |

Your best score is saved on your computer, so it is still there the next time
you play.

## The scripts

All scripts are in `Assets/Scripts`.

| Script | What it does |
| --- | --- |
| `GameManager.cs` | Runs the game: score, game state, and the menu screens |
| `GameState.cs` | The three states: `PreGame`, `Game`, `GameOver` |
| `BirdController.cs` | Moves the bird up, turns it, and finds crashes |
| `PipeSpawner.cs` | Creates new pipes while you play |
| `PipeMovement.cs` | Moves each pipe to the left and deletes it at the end |
| `ScoreZone.cs` | Adds one point when the bird passes a pipe |
| `LoopGround.cs` | Repeats the ground so it looks endless |
| `MenuButton.cs` | Connects the UI buttons to the `GameManager` |
| `FixedAspectCamera.cs` | Keeps the same screen shape on any window size |

## Folders

| Folder | What is inside |
| --- | --- |
| `Assets/Scenes` | `MainScene` - the only scene in the game |
| `Assets/Scripts` | All the C# code |
| `Assets/Prefabs` | The bird and the pipe |
| `Assets/Sprites` | The images |
| `Assets/Animations` | The wing animation |
| `Assets/Fonts` | The game font |
