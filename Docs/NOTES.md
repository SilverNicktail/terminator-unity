# Development Notes/Scratch Pad

This page will contain WIP/stream-of-consciousness notes as I analyse and learn how DFU works internally. Some may be elevated to full documentation, some may be nuked as idiocy. Take nothing in this file as reliable, but do have fun laughing at what a clumsy berk I am.

## Startup Sequence

DFU has two scenes:
    * `DaggerfallUnity`, which runs...you'll never guess
    * `DaggerfallUnityStartup`, which handles initial setup, game file locations, etc. Essentially a launcher within the game.

The major systems of the game live on top-level objects in `DaggerfallUnity`'s hierarchy. `DaggerfallUnityStartup` naturally omits most of these, keeping essentials like the input manager, enough assets to make the file selection screen look nice, a bit of music, etc.

The main `DaggerfallUnity` script appears in the startup scene to allow access to settings, the asset folder, etc. It doesn't take any immediate actions in this scene on its own, it's just the game environment/settings.

`SceneControl` looks for existing valid game settings, and if they exist immediately loads the main scene. Otherwise, it triggers the setup UI by posting an event to load `DaggerfallUnitySetupGameWizard`.

### Impacts on Project Structure

While the asset folder abstraction I've made so far is useful and I should keep porting scripts over to use it, having that kind of if-else/polymorphism in every script and trying to run the same two scenes for very different games would be stupid and unmanageable. 

Next step: clone `DaggerfallUnity` for Future Shock, rip out the RPG-related stuff and begin customising what's left to work with FPS. Try to keep `DaggerfallUnityStartup` and make it game-agnostic?

**Note:** It might seem strange to onlookers to be trying so hard to keep the project compatible with Daggerfall. I'm aware that it's a _bit_ daft, but it would be cool to pull off. It also makes it easier to re-use DFU code wherever possible while I continue learning how all this works under the hood. Picked a hell of a first game mod project, I'll take any leg up I can get. There may come a moment later on when it becomes necessary/practical to sever the link to Daggerfall and make this a stand-alone Terminator project, but we ain't nearly there yet.

## Running the Game

In the main scene, the game startup code lives in `StartGameBehaviour`, which lives on its own top-level object in the scene hierarchy. When this object wakes up, it:

1. Sets up screen settings - resolution, fullscreen, vsync, FOV, etc
2. Sets up general input preferences
3. Sets up general HUD settings and preferences

At runtime, this object checks whether a "Start Method" has been set - load the title screen, the new character screen, etc - and then kicks off the process if one is present. Generally it handles the initial settings for a scenario, like the list above, and then fires an event for the various systems in the scene to respond to.

## Impacts on Project Structure

This is a decent code structure that I should be able to keep and modify for Future Shock. Not everything it deals with will be necessary, but by keeping the other game systems decoupled I can swap out the RPG-specific ones with FPS-specific ones.