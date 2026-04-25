# MDMDBRIF.BSA - Text Archive

This BSA contains the overwhelming majority of the game's text. Each record in
the archive represents either the text for a mission, or the "tactical" 
(bestiary) entry for a machine.

Note that all the information here is referencing an English-only copy of the
game and may vary slightly for other languages/locales. I have access to four
UK copies of the CD and I will check them all for variation. I will also
attempt to find other localisations of the game.

## Record Names

Tactical records have relatively self-explanatory names like `TACDRONE.TXT`, 
whereas mission records have simple numerical names like `010.TXT`. 

Generally speaking, mission records are simple two-digit numbers, in order of
their appearance in the game. A trailing zero is then added to allow for multi-
part missions. 

For example, mission 09's text can be found in `090.TXT`. Mission 14 is spread
across multiple levels, so the initial mission text is in `140.TXT`, with
parts 2 and 3 living in `141.TXT` and `147.TXT`.

Exceptions to this rule, such as `000.TXT` and `014.TXT` seem to be leftover
development data, containing unfinished/unused versions of text from other
missions.

## Encoding

All text records are encoded/obfuscated for some reason. At first I thought
that they were compressed, as the 
[Daggerfall wiki's page on BSA files](https://en.uesp.net/wiki/Daggerfall_Mod:BSA_file_formats#NameRecord)
mentions that records in BSAs have a "compression" flag that can be set, and it
is indeed set for all records in `MDMDBRIF.BSA`.

However, while trying to ascertain which compression method had been used I
wrote the raw text of a known record next to its hex and they were roughly the
same size, indicating that the text was encoded rather than compressed.

Luckily a Russian fansub forum pointed me in the correct direction, so all
credit to the [old-games.ru](https://www.old-games.ru/forum/threads/o-vozmozhnosti-perevoda-igry-terminator-future-shock.54268/)
community members who figured this out and wrote it down. Through a haze of
Google Translate I was able to get the gist of what was being described and
validate it myself.

Each record in the file is encoded using what is essentially a Vigenère cipher.
A key is repeated along the length of the record's hex, and each byte of the
record has the value of the corresponding byte in the key added to it,
obfuscating the text. 

We can thus find the encoding key by finding a record where we know the text
(easy to do with access to the game), laying its hex alongside the expected
ASCII values, calculating the difference between expected and actual for each
byte, and then looking for patterns in the differences. Sure enough, I reached
the same conclusion as the forum members. The encoding key for Future Shock's
text is `DD836557EA780848B801389408DD3FC2BEAB76C614` (21 bytes).

## Mission Record Text Formatting

While tactical records are just plain text descriptions of an enemy or other
object, mission records are more sophisticated. They contain the text for the
mission briefings, as well as the in-mission text that appears in the HUD
communicator. They also contain references to which tactical items should
appear during briefings, and other auxilliary pieces of configuration. 

The format created to represent this information is similar in concept to
[Daggerfall's text record format](https://en.uesp.net/wiki/Daggerfall_Mod:Text_Record_Format)
but very different in implementation. This section will describe this format.

(This documentation is currently a work-in-progress.)

### Sections

Each mission file consists of several sections in a specific order. A section
begins with a header consisting of a section ID within square brackets, and
ends with four percentage (`%`) marks. For example:

```
[M1]
This looks like the hotel Sgt. Roberts was talking about...
####
%%%%
```

Within a section, subsections are demarked by four hash marks (`#`) at their 
end. If a section contains only one subsection, it is still ended with four
hashes, as you can see above. Here is an example of a section with two 
subsections:

```
[BR]
[010]
Connor here. Sorry to hear about Sgt. Roberts...
####
[010]
Mission Objective: 1)Find Resistance Headquarters.
####
%%%%

```

### Section Types

So far I have identified the following section headers, listed here in an order
matching their appearance in mission files. Their usage is noted, if known.

|Header Code|Name|Description|
|---|---|---|
|`MP`|Mission Profile|Identifies the mission/level to the game, contains its ID (matches filename).|
|`EN`|Intro|Initial state of briefing screen - contains list of present characters' IDs and initial text area content.|
|`ST`|Unknown||
|`TA`|Tactical|Contains a list of IDs for which enemies/objects should appear in the "Tactical" tab.|
|`BR`|Briefing|Each subsection contains briefing text starting with the character code of the person speaking. The final subsection contains a state for the screen to reset to afterward.|
|`BE`|Briefing Ending?|Seems initially to match/serve the same purpose as the final subsection in each `BE` block.|
|`M*`|In-Mission Text|Series of subsections containing text to be triggered during missions - comms with base, commentary by the PC, etc. Named in sequence, `M1`, `M2`, etc. May relate to trigger names in code.|
|`C*`|In-Mission Text (Aux)|Formatted the same way as `M*` blocks, but not always present in sequence. Used for text reacting to events like the player attempting to leave the mission area (`G9`).|

### Character Codes

Each subsection within a briefing will start with the character code of the
person speaking:

```
[BR]
[003]
Glad you made it here in one piece, #N. I'm Col. John Connor. I'd like you
to meet my staff. This is my second-in-command, Major Kathryn Parker.
####
[006]
Good to have you aboard, #N.
####
```

Character codes are as follows:

|Code|Character|
|---|---|
|001|Dr. Bill Hanover|
|002|Laurie Brinks|
|003|Colonel John Connor|
|004|Kyle Reese|
|005|Sergeant Thomas Jensen|
|006|Major Kathryn Parker|
|007|Captain Milton Bishop|
|008|Colonel John Connor (w/scar)|
|009|Sergeant Roberts|
|010|Briefing over radio|
|011|John Connor (outdoors)|

Having successfully retrieved briefing images, the duplicate character codes
for John make more sense. John's appearance changes twice during the game - 
once after he receives a scar, and once when he appears outside. The filenames
for character-specific images in briefings are in the same order with the same
numerical values as the text in this archive. This explains why John would 
appear to have three different character codes.

### Placeholders

Some additional inline codes exist as placeholders for runtime values.

|Code|Purpose|
|---|---|
|`#C`|Replaced with player's callsign.|
|`#N`|Replaced with player's name.|

## Control Codes

Some text files contain control codes to change how the text is rendered. These
are prepended with a `#`, similarly to placeholders in mission files.

`#F` appears to switch the font, specifying its three digit ID. For example, 
`#F003`.

`#C` appears to switch the colour palette, again specifying a 3 digit palette 
ID. For example, `#C244`.

## File Index

Ordered alphabetically for ease of reference - order in the file is different.
(Missions are not named in-game, these names are also for ease of reference.)

|Record Name|Description|
|---|---|
|`000.TXT`|Leftover development data, unused|
|`010.TXT`|Mission 01: The Tiki Grand|
|`014.TXT`|Unused development version of mission 14|
|`020.TXT`|Mission 02: Reach the Resistance|
|`030.TXT`|Mission 03: Liberate the Death Camp|
|`035.TXT`|Another unused development version of mission 14|
|`040.TXT`|Mission 04: Drive to Safety|
|`050.TXT`|Mission 05: H/K Salvage|
|`060.TXT`|Mission 06: Destroy Encampment|
|`070.TXT`|Mission 07: Destroy Satellite Uplink|
|`080.TXT`|Mission 08: Rescue|
|`090.TXT`|Mission 09: Return to Base|
|`100.TXT`|Mission 10: Evacuation|
|`110.TXT`|Mission 11: Destroy Aerodrome|
|`120.TXT`|Mission 12: Destroy Convoy|
|`130.TXT`|Mission 13: Attack Refinery|
|`140.TXT`|Mission 14: Data Retrieval. Part 1: Reach the Outpost|
|`141.TXT`|Mission 14, Part 2: Steal the Data|
|`147.TXT`|Mission 14, Part 3: Escape|
|`150.TXT`|Mission 15: Milton Bishop|
|`160.TXT`|Mission 16: TDTS Airstrike|
|`170.TXT`|Mission 17: Move into Position|
|`180.TXT`|Mission 18: Infiltrate TDTS Perimeter|
|`190.TXT`|Mission 19: Transmission|
|`MAP.TXT`|List of map IDs and their matching names|
|`SCROLLER.TXT`|Game credits|
|`TAC600PS.TXT`|Tactical: Model T-600 Terminators|
|`TAC800RF.TXT`|Tactical: Model T-800 Terminators|
|`TACBMBR.TXT`|Tactical: Hunter/Killer Bomber|
|`TACBOSS.TXT`|Tactical: Goliath|
|`TACCMBRF.TXT`|Tactical: Model T-400 Terminator Endoskeletons|
|`TACDRONE.TXT`|Tactical: Hunter/Killer Drone|
|`TACFLNCR.TXT`|Tactical: Flencer|
|`TACGLOBE.TXT`|Tactical: Seeker|
|`TACHKFTR.TXT`|Tactical: Hunter/Killer Fighter|
|`TACHVRTK.TXT`|Tactical: Hover Tank|
|`TACHVYTK.TXT`|Tactical: Heavy Tank|
|`TACMANTK.TXT`|Tactical: Light Tank|
|`TACRAPTR.TXT`|Tactical: Raptor|
|`TACSCOUT.TXT`|Tactical: Hunkter/Killer Scout|
|`TACSPIDR.TXT`|Tactical: Spiderbot|
|`TACT-REX.TXT`|Tactical: T-Rex|
|`TFSTRANS.TXT`|Samples of text from various mission briefings. Translation samples?|
|`TACTRUCK.TXT`|Tactical: Anti-Gravity Truck|
|`TACTURCN.TXT`|Incomplete T-600 tactical record, dev cruft|
|`TACTURLS.TXT`|Tactical: Laser Turret|
|`TACTURMS.TXT`|Incomplete T600 tactical record, cruft|