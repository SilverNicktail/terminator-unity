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

### Impacts on Project Structure

This is a decent code structure that I should be able to keep and modify for Future Shock. Not everything it deals with will be necessary, but by keeping the other game systems decoupled I can swap out the RPG-specific ones with FPS-specific ones.

## Daggerfall Scene Hierarchy

`DaggerfallUnity`
  : Hosts singletons/utility scripts providing asset access.
`DaggerfallUI`
  : Loads/displays appropriate UI elements, also hosts the primary audio source.
`QuestMachine`
  : Script handling quests in Daggerfall - structure, state, etc. Also has its own audio source, not sure why at present.
`EntityEffectBroker`
  : Handles the effects of magic spells between game entities.
`PlayerAdvanced`
  : Player movement and state management. Using very old movement code, modern Unity may provide some of these features.
`RetroPresentation`
  : Handles viewport setup depending on monitor resolution/HUD size. Also handles graphical effects/filtering to make the retro graphics look good in a modern engine.
`InputManager`
  : Input handling/customisation specific to Daggerfall. Sets speeds, limits, etc to give everything the right feel.
`GameManager`
  : Runtime utilities, primarily provides a way to dynamically load/locate game systems at runtime.
`StartGameBehaviour`
  : Handles all initial windowing setup and raises the necessary events to start the game.
`SaveLoadManager`
  : Handles loading and saving of the game, using DFU's own format.
`Exterior`
  : Hierarchy of objects that host the 3D world of the current level, if the player is outside - streaming world data, sunlight, the sky, background music, etc.
`StreamingWorld`
  : Loads level terrain data from Daggerfall's famous streaming world (where some areas are pre-defined, and others are generated from seed).
`Interior`
  : Hierarchy of objects that host the world if the player is in an indoor environment.
`Dungeon`
  : Hierarchy of objects that host the world if the player is in a dungeon.
`Canvas`
  : Hosts the developer console as a child.
`EventSystem`
  : Handles user input, as well as items like raycasting.
`Automap`
  : Objects for interior and exterior automaps. (Are these the same in Daggerfall as the 3D map in Future Shock?)
`WeatherManager`
  : Manages, shockingly, in-game weather effects.
`TalkManager`
  : Handles conversation trees with NPCs.
`BankPurchase`
  : Empty object, not sure what it's used for but obviously related to in-game trades.
`TextManager`
  : Organises/provides access to the game's text assets.
`PrintScreenManager`
  : Takes screenshots
`PostProcessVolume`
  : Controls graphical post-processing options.

So I can likely straight-up ditch most of these for Future Shock. I'll either need to split the other existing scripts
into XnGine-generic and Daggerfall-specific components, or write fresh ones from scratch. The UI and input code in DFU
is pretty outdated by Unity standards, and if I'm intending to get this off Unity 2019 it'd probably be a good idea to
use newer options.