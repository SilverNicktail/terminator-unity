# Future Shock Music Index

## .HMI Files

*.HMI files can be converted to standard MIDI files using 
[midistar2mid](https://github.com/Sembiance/midistar2mid/).

For a slap-dash conversion to WAV for easy listening, 
[Timidity](https://timidity.sourceforge.net/) can be utilised. 

I knocked together the simple Bash script below to hot-convert files from my
Future Shock disc for reference during development. This document contains a
list of each file and a description of what it is/where it is intended to be
used, which I suppose I will be _forced_ to fill out by playing the game. ;-)

(None of these files are distributed with this project. As ever, this project 
is designed as an enhancement for existing, legally purchased software, not a
distribution of software I do not own.)

|Filename|Description|
|---|---|
|`T01.HMI`|Terminator theme music, slow. (Not main menu.)|
|`T03.HMI`|TBC|
|`T100.HMI`|TBC|
|`T101.HMI`|TBC|
|`T102.HMI`|TBC|
|`T103.HMI`|TBC|
|`T104.HMI`|TBC|
|`T200.HMI`|TBC|
|`T201.HMI`|TBC|
|`T202.HMI`|TBC|
|`T203.HMI`|TBC|
|`T204.HMI`|TBC|
|`T205.HMI`|TBC|
|`T206.HMI`|TBC|
|`TEST.HMI`|Test music used in soundcard calibration.|
|`TITLE.HMI`|Main menu/title version of the Terminator theme.|

### .HMI Conversion

```bash
#!/usr/bin/env bash

for hmi in *.HMI; do
    [ -f "$hmi" ] || break
    filename="${hmi%.*}"
    midistar2mid "$hmi" "$filename.mid"
    timidity "$filename.mid" -OwS -o - | ffmpeg -f wav -i - "$filename.mp3"
done
```