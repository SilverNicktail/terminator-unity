# Terminator Unity

Terminator Unity is an attempt to create a Unity wrapper around the game Terminator: Future Shock, that will allow the
original game to run cleanly on modern systems, with many quality-of-life upgrades. It is based directly on the
extraordinary work done by the Daggerfall Unity team, who already achieved this for The Elder Scrolls: Daggerfall.

Daggerfall Unity is an open source engine port/uplift of Daggerfall to the Unity engine created by
[Daggerfall Workshop](http://www.dfworkshop.net).

## Hello, Lawyers!

Terminator Unity is a game mod (an engine port) and requires a legal copy of Terminator: Future Shock to run. This 
provides all necessary game assets such as textures, 3D models, and sound effects. **Terminator Unity does not 
distribute any copyrighted material.** Unlike Daggerfall, Future Shock was never made available as freeware, and 
because of its movie licensing it will likely never be released again, so users will have to source an original copy of
the game.

There's nothing I can do about this. Do not try to circumnavigate this restriction. Any issues or discussions created
asking for a copy of the game will be deleted and the user banned.

This mod has nothing to do with the owners of the Terminator intellectual property, or Future Shock specifically, and
doesn't pretend otherwise. Obviously I don't own the "Terminator" name, or anything like that, and the people that do
haven't endorsed me. I just love this game and I want to restore it for the people that own it.

## Future What?

A sadly overlooked achievement in gaming, Terminator: Future Shock was released in 1995 by Bethesda Softworks. A truly
revolutionary game, it was the first mouse-look FPS game released (no, that wasn't Quake!). As well as creating the 
basic control scheme of all future first-person games, it also featured 3D free-roaming levels, 3D enemies,
the ability to enter and explore buildings around the map at will, and driving/flying vehicle sections. 

None of that may sound particularly impressive today, but when you consider that Future Shock came out only one year
after Doom II, and one year __before__ both Duke Nukem 3D and Quake, it's far more so.

It was also just a pretty damn good FPS, if you ask me. Future Shock was moody, atmospheric, lonely, occasionally 
terrifying and mostly faithful to the world created by the films. For myself, Future Shock was a formative experience, 
directly contributing to a lifelong love of gaming. Unfortunately it sits forgotten in the annals of the medium, with
no digital release (owing to its status as a movie-licensed game), system requirements too archaic for modern machines,
and visuals/AI that - let's be honest - hold up as well as a chocolate teapot. I hope to help resolve some of these
issues and make the game more accessible to modern audiences.

## What about SkyNET?

Future Shock got a standalone expansion pack, SkyNET, in 1996. If possible, I would like to support that release as
well, but for the initial work I will focus on Future Shock (while leaving space to accommodate SkyNET where possible).

## Goals

The primary goals of the project are:

* Seamless gameplay on modern Windows and Linux systems (I do not have Mac to test)
* Improved native resolution
* Widescreen support
* Controller support, for heathens
* Improved enemy AI/pathfinding (the original's is pretty rubbish, let's be honest)
* Accessibility enhancements where appropriate
* Translation support
* Improved audio balancing, perhaps even a replacement SFX pack
* Integration with RetroAchievements or similar
* Autosave and mission-select functionality
* Some form of cloud save integration, if possible

There are also several prominent bugs I would like to patch:

* Enemy AI & animations are deactivated at a range slightly less than the draw distance, allowing them to be killed and
freeze mid-explosion.
* Some level edges have incorrectly placed invisible walls that allow the player to get permanently stuck.
* (If SkyNET is integrated) The "Twisted Ankle" bug, where sliding down even small slopes can result in massive health
loss when reaching the end.
* (If SkyNET is integrated) Game-breaking bug where a mission-critical human target cannot be killed if running Future 
Shock in SkyNET's enhanced mode.

## Progress

**Current goal: loading, validating and documenting all file formats.**

Most file formats line up pretty well between the '95 and '96 versions of the engine, but there are some files in 
Future Shock that don't appear in Daggerfall, and other formats that have had minor changes made.

* Archives :white_check_mark:
* Colour Translation
* Fonts :white_check_mark:
* Height Maps
* Images (Static) :white_check_mark:
* Images (Animated) :white_check_mark:
* Maps
* Models (Enemies)
* Models (Level)
* Music - Validated, not loaded in code
* Palettes :white_check_mark:
* Sounds
* Text :white_check_mark:
* Textures
* Video

## Development Notes

Linked here are some development notes from my initial work on the project. These will be uplifted into a wiki in
future.

* [File Types](./Docs/FILETYPES.md) - The files in the `GAMEDATA` folder and their technical specs
(where they differ from Daggerfall)

## Installation (TBC)

Can't confirm how this will work yet, but I can confirm you'll need a copy of the original install disc.

Don't worry, no need to run DOSBox and manually install everything! The Future Shock disc contains the full
installation data in an uncompressed form. Simply copy the `/SHOCK/GAMEDATA/` folder to your hard drive and point
Terminator Unity to it.

(The original installation process does not copy video files to the hard drive, even when "full installation" is
selected, so Terminator Unity will not work against copies already installed via DOSBox.)

## Requirements

### Minimum
* Operating system: Windows, Linux, MacOS
* Processor: Intel i3 (Skylake) equivalent
* Graphics: DirectX 11 capable with 1GB video memory and up-to-date drivers
* Memory: 2GB system RAM

### Recommended
* Operating system: Windows, Linux, MacOS
* Processor: Intel i5 (Skylake) equivalent
* Graphics: GTX 660 with 2GB video memory and up-to-date drivers
* Memory: 4GB system RAM

## Unity Version

Daggerfall Unity was most recently developed against Unity 2019. I am hopeful that I can update the project to a more
recent version. Not sure yet of the reason why this hasn't occurred upstream. Could be the custom tools that were
developed for the project (which I plan to update as my first task anyway).