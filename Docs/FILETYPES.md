# Future Shock / xNgine File Types

The various file types that are part of Future Shock's `GAMEDATA` folder, and their contents. Most of these files follow
formats similar/identical to the files found in Future Shock's sister game, Daggerfall. 

The community documentation for these file formats is here:https://en.uesp.net/wiki/Daggerfall_Mod:File_Formats. 
Many thanks to those modders of the past 25 years who bothered writing any of it down.

Where differences exist between Daggerfall and Future Shock, they will be documented here.

`xxx.BSA`
: Generic binary archives, can contain a range of data. Not sure what `.BSA` means but I'm going to assume "Bethesda". ;-) Bethesda Softworks Archive? **Note:** Format is _almost_ identical to 
[Daggerfall's](https://en.uesp.net/wiki/Daggerfall_Mod:BSA_file_formats), but the `BsaType` part of the header is not
present. 

`xxx.COL`
: Colour palette. **Note:** Some of these files are 776 bytes in length, matching Daggerfall's `*.COL` file type, and 
some are 768 bytes in length, matching Daggerfall's `*.PAL` file type. It seems that Future Shock does not outwardly 
distinguish between palettes with an attached header and those without.

`FONTxxxx.FNT`
: Font file, contains glyphs for rendered text. Not documented on Daggerfall wiki but exists in the codebase. Something 
I can contribute. Future Shock follows the same format and the existing code was able to load the glyphs OK with only 
some minor tweaks.

`HAZE.000`
: A [Colour Translation](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/Colour_Translation) file, used to shift
indices in *.COL files as the user moves toward/away from a billboard, providing a fogging implementation.

[`xxx.HMI`](./MUSIC.md)
: MIDI files, confirmed from Daggerfall's `MIDI.BSA`. In the Human Machine Interface proprietary format. Was able to 
convert one to regular MIDI using an [open source utility](https://github.com/Sembiance/midistar2mid/).

`LIGHT.DAT`
: Currently unknown, but likely lighting data. In a hex editor it looks like the inverse of `HAZE.000`; one long value
gradient, but in the opposite direction. Could these be PAK files similar to Daggerfall's `CLIMATE.PAK`?

`SHADE.xxx`
: A [Colour Translation](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/Colour_Translation) file, used to shift
indices in *.COL files as the user moves toward/away from a billboard, providing a fogging implementation.

`SHOCKRED.000`
: Currently unconfirmed, but based on the value gradient within the file and file extension this is potentially intended
as a colour translation file similarly to the `HAZE.000` and `SHADE.xxx` files above. This file is much smaller, however.
Potentially used for player damage?

`TEXTURE.xxx`
: Contains texture data, simple numerical file extensions. Seems to follow the same format as Daggerfall.

`TEST.WAV`
: The Human Machine Interface sample file for testing sound card configuration.

`WLD.xxx`
: "World" files? Numerical extensions. These seem to follow the `*.WLD` format in Daggerfall, which means these are 
height maps. Header format checks out, as does the fixed-length file size. Curiously, there's only 16 of them, when 
there's 17 missions in the game. (The final mission is entirely indoors though, right?)

## BSA Files

Future Shock contains seven:

* [`MDMDBRIF.BSA`](./TEXT.md) - Briefing, in-mission and beastiary text.
* `MDMDENMS.BSA` - Enemy model data, equivalent of MONSTER.BSA
* `MDMDIMGS.BSA` - Image data
* `MDMDMAPS.BSA` - Maps, equivalent of DF's MAPS.BSA
* `MDMDMUSC.BSA` - Music data, XMI format, whatever the heck that is. Judging by filenames (`FMINTRO.XMI`, etc) some of the audio appears to be for in-game movies? Asset folder also contains lots of HMI files, as above, so these may be intended specifically for video.
* `MDMDOBJS.BSA` - Level objects - walls, rubble, interior items, etc. Equivalent to DF's `ARCH3D.BSA`.
* `MDMDSFXS.BSA` - Sound effects, equivalent to DF's 'DAGGER.SND`.

(Anyone want to take a guess at what the hell "MDMD" stands for?)