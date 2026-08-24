# MDMDSFXS.BSA - Sound Effects Archive

Future Shock stores its sound effects in this single archive, in almost the 
same way as Daggerfall uses its own `DAGGER.SND` archive. As with Daggerfall,
each entry consists of raw 8-bit PCM data at 11025Hz. However, in Future Shock
these files are encoded using the same cipher applied to the text archive, with
the same cipher key. Details of this encoding can be found in the documentation
for the [text archive](./TEXT.md).

Combining the existing audio playback code from Daggerfall Unity with an 
additional decoding layer to remove the cipher worked fine.

## Sound Effect Index

TBA
