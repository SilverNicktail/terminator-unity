# Future Shock Save Files

Notes attempting to crack the Future Shock save files. Brute force methodology to start: make a save, take an action, 
save again, check the diff. This could take a while, and isn't really needed to build out the source port. Low 
priority.

**Information in this file should be considered unreliable until this notice is removed.**

**File length:** Files grow in size over time, initial size at level 1 on my UK CD-ROM copy is 11163 bytes. Loading each new level or building interior increases the size of the save file. Highly likely this is flag data for the new location - enemy positions/state, switch/door states, destroyed objects, etc.

Unlike a BSA archive, there does not appear to be any kind of record index/footer for these different areas, though I
could be mistaken.

Need to figure out checksumming. Modifying even one byte of the player's health value to a known value from another 
save causes the game to crash out with an "invalid save file" error message.

## File Structure

|Byte Offset|Size|Field|Description|
|---|---|---|---|
|`0x0000`|4B|Version|Game version that created the save, my unpatched CD-ROM shows `v099`.|
|`0x0004`|31B|Save Name|User-provided name for the save.|
|`0x0024`|4B|Unknown|Value changes significantly on every save, non-incrementally. Could be a checksum.|
|`0x0058`|2B|Player Inclination|Player's inclination (viewpoint up/down)|
|`0x005C`|2B|Player Rotation|Player's rotation around Y axis (turning)|
|`0x0064`|4B?|Player Armour|Player's current armour value. Values are a little weird. Max: 57 EA 79 08. None: 57 EA 78 08. Third byte appears to be `79` only when armour is full? Fourth byte (`08`) may be irrelevant.|
|`0x0068`|4B?|Player Health|Player's current health value. Max: 48 B8 01 38|