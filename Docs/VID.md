# Video Files (*.vid)

[Daggerfall Unity Documentation](https://en.uesp.net/wiki/Daggerfall_Mod:VID_Files) (Contributed by me. 😉)

Block-based video format containing audio and video block data to be displayed and synchronised, and an embedded
palette, unlike with other image formats in the engine where palettes are usually externalised.

Future Shock contains only four fullscreen video files:

* `LOGO.VID` - The Bethesda logo on startup. BAAAAAAOOOOOOWWWMMM!
* `START.VID` - The aninmated Terminator on the main menu screen.
* `BEGIN.VID` - Campaign intro video.
* `END.VID` - Campaign victory video.

Reading the code and doing some tests, these _initially_ seemed to follow the same format as in Daggerfall, but there
appear to be some minor incompatibilities. 

## `LOGO.VID`

The Bethesda logo video follows an identical format to the videos in Daggerfall. The video needed one adjustment to the
existing parser in order to work correctly: lowering the minimum audio block length to 555 bytes. The `*.VID` parser 
enforces a minimum block length in order to keep frame pacing consistent. `LOGO.VID` has many audio blocks 555 bytes in
length, straying over this minimum, which caused audio blocks to have gaps at either side. Easily fixed.

## Other Videos

When I tried the same thing with the other vids, I got a parsing error. In Daggerfall videos there is always an audio
block before the first video block, and the code expects it to be there. In the Future Shock videos, the first block is
a video block, causing a parsing error when trying to calculate the applicable audio period. I haven't confirmed yet if
the audio is simply stored out of sequence, or is stored externally. (Several files in the SFX archive seem like
potential candidates.)

After doing a quick update to `VidFile.cs` to use the minimum frame delay if the audio block is not present, I am able 
to play the videos silently. Naturally, they run too quickly, but this confirms that the video data is compatible. 
Logging out the block IDs in `START.VID` as they go through the renderer, there do not appear to be any audio blocks
_or_ unknown blocks (IDs that don't exist in the existing codebase/Daggerfall docs). This would seem to confirm that
the audio for these files is externalised somehow. Somewhere.