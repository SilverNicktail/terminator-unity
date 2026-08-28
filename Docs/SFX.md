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

| Record Name | Description |
|---|---|
| `AMB_CITY.RAW` | Ambient L.A. city noise, identical to audio in intro movie before the nuke hits. |
| `AMB_CORE.RAW` | Ambient background loop for some SkyNET facilities. Not 100% which at time of writing - TDTS? |
| `AMB_FIRE.RAW` | Ambient sound of a street fire. |
| `AMB_WIND.RAW` | Ambient wind SFX. |
| `AMTECH1.RAW` | Ambient SkyNET facility interior loop, type 1. |
| `AMTECH2.RAW` | Ambient SkyNET facility interior loop, type 2. (Sounds exactly like the death camp in the intro movie.) |
| `AMTECH3.RAW` | Ambient SkyNET facility interior loop, type 1. |
| `AMTECH4.RAW` | Ambient SkyNET facility interior loop, type 1. |
| `BAMFIRE.RAW` | Ambient fire loop, much longer than `AMB_FIRE.RAW`. |
| `BAMSTRET.RAW` | Ambient L.A. city noise, seems identical to `AMB_CITY.RAW` (but louder?). |
| `BDTHCAMP.RAW` | Background audio loop for the death camp, very similar to `AMTECH2.RAW` but much longer. |
| `BEXPLO2.RAW` | Explosion, used for objects detonating, also part of machine explosions. |
| `BHK.RAW` | HK aircraft turbine whine. |
| `BNUKE.RAW` | Initial explosion and crescendo of approaching blast - used for nuclear explosion in intro movie. |
| `BREATH1.RAW` | Sharp breathing, don't recall this being used. TBC. |
| `BREATH2.RAW` | Slower breathing, don't recall this being used either. |
| `BREATHIN.RAW` | Collection of breathing patterns, sound very odd together. Don't recall this being used. |
| `BUBBLES.RAW` | Sound of many bubbles in liquid, like a Terminator gestation tank. |
| `BUTTON1.RAW` | In-game button/switch sound. |
| `BUTTON2.RAW` | In-game button/switch sound. |
| `CARCOLL1.RAW` | Car collision sound. |
| `CARCOLL2.RAW` | Car collision sound. |
| `CARENG1.RAW` | Car engine (idling). |
| `CLICK.RAW` | Click sound, believe this is a weapon being out of ammo. |
| `COLLIDE.RAW` | Generic vehicle collision sound, might be in use when flying the HK. |
| `COMM.RAW` | Beep prompt, used when receiving radio messages in-game to draw attention. |
| `COMM1.RAW` | Seems identical to `COMM.RAW` |
| `COMPLETE.RAW` | Military snare drums, guessing this was originally used with "Well done, Soldier!" at the end of missions? Not used. |
| `DOORA.RAW` | SkyNet door opening sound. |
| `DOORB.RAW` | SkyNet door opening sound. |
| `DOORC.RAW` | SkyNet door opening sound. |
| `DOORD.RAW` | SkyNet door opening sound. |
| `DOORW.RAW` | Wooden door opening sound. |
| `DRIPS.RAW` | Dripping water, used in sewers. |
| `ELEVAT1.RAW` | Elevator movement, though it's definitely used for other actions triggered by switches. |
| `EXPLO1.RAW` | Explosion. |
| `EXPLO2.RAW` | Explosion. |
| `EXPLO3.RAW` | Explosion. |
| `EXPLO4.RAW` | Explosion. |
| `EXPLO5.RAW` | Explosion. |
| `EXPLO6.RAW` | Explosion. |
| `FAIL.RAW` | Military drum phrase, guessing this was originally used with "game over" text on death. Unused. |
| `FASTGUN2.RAW` | Gun firing noise for the machine gun. |
| `FIRE.RAW` | Small fire burning loop. |
| `FIZZLE1.RAW` | Sci-fi click and falling whine, believe this is the sound used when energy weapons are out of ammo. |
| `GAUSS1.RAW` | Sound of machines "teleporting" into place as the time stream is changed. |
| `GEIGER1.RAW` | Geiger counter activation, low-radiation state. |
| `GEIGER2.RAW` | Geiger counter activation, high-radiation state. |
| `GRNLAUN2.RAW` | Sound of a grenade being fired from the grenade launcher. THHUM! |
| `GRUNT1.RAW` | Player grunt, used for fall damage or large weapon damage. |
| `HEART1.RAW` | Heartbeat, slow. |
| `HEART2.RAW` | Heartbeat, faster. |
| `HIT2.RAW` | Some form of impact, don't recognise it. |
| `HK2.RAW` | HK turbine whine. |
| `HVYFT4.RAW` | Machine footfall - Raptor. |
| `HVYFT5.RAW` | Machine footfall - Rex. |
| `HYDRA2.RAW` | Machine actuation noise - Spiderbot. |
| `HYDRA3.RAW` | Machine actuation noise - Raptor (and others, I think). |
| `HYDRA3.RAW` | Machine actuation noise - Terminator. |
| `JMPCON.RAW` | Jump landing - on concrete. |
| `JMPMET.RAW` | Jump landing - on metal. |
| `JMPSEW.RAW` | Jump landing - in sewer. |
| `JMPWOD.RAW` | Jump landing - on wood. |
| `LASER1.RAW` | Laser firing - HK Drone. |
| `LASER2.RAW` | Laser cannon firing - Jeep. |
| `LASER3.RAW` | Energy weapon firing, think this is the plasma cannon firing. |
| `LASER4.RAW` | Energy weapon firing, think this is the plasma pistol firing. |
| `LASER5.RAW` | Long energy weapon firing sequence, don't think this is used. |
| `LASER6.RAW` | Energy weapon firing, many enemy bolts such as the Raptor. |
| `LASER7.RAW` | Variant on `LASER6.RAW`. |
| `LASER8.RAW` | "Lighter" variant on `LASER6.RAW`. |
| `LEVER1.RAW` | Lever/switch activation. |
| `LFWGRV.RAW` | Player left footfall - on gravel/ground. |
| `LFWMET.RAW` | Player left footfall - on metal. |
| `LFWSEW.RAW` | Player left footfall - in sewer. |
| `LFWWOD.RAW` | Player left footfall - on wood. |
| `LOGO2.RAW` | Background drone used in Bethesda logo video file. BWWWUUUUUUURRRMMMM. |
| `MESSTEST.RAW` | A test file for an unused implementation of voice acting for radio messages. Hilariously bad. |
| `METDOOR.RAW` | Sound of a metal roller door opening. |
| `MILTDIES.RAW` | Milton Bishop death sound. |
| `MILTKILL.RAW` | Milton Bishop begging to be killed. |
| `MOVGTERM.RAW` | Movement audio loop for Terminators. |
| `N_ACTR.RAW` | The sound of the Terminator turning its head on the menu screen. |
| `N_SLAM1.RAW` | Menu screen text "slamming" into place. |
| `N_SLAM2.RAW` | Menu screen text "slamming" into place, alternate. Need to figure out which one is used for which text slam. |
| `PIPEHIT1.RAW` | Lead pipe hitting something. |
| `PPC100.RAW` | Energy weapon firing. "PPC" is certainly "phased plasma cannon", though from memory this sounds like the rifle. Need to confirm. |
| `PPCLOAD.RAW` | Reload sound for the plasma cannon? But weapons in this game don't have reloads. Sound is identical to the sound of a Seeker powering up before it explodes, may have had its use changed. |
| `PPCRIFLE.RAW` | Plasma rifle firing. (But from memory I think this is the cannon? Need to confirm.) |
| `PRESS1.RAW` | Menu button press. |
| `RFWGRV.RAW` | Player right footfall - on gravel/ground. |
| `RFWMET.RAW` | Player right footfall - on metal. |
| `RFWSEW.RAW` | Player right footfall - in sewer. |
| `RFWWOD.RAW` | Player right footfall - on wood. |
| `RICH05.RAW` | Bullet ricochet, can't recall if this is used. |
| `RICH08.RAW` | Bullet ricochet, can't recall if this is used. |
| `RICH05.RAW` | Bullet ricochet, can't recall if this is used. |
| `ROCKET1.RAW` | Rocket streaking sound. |
| `ROCKET2.RAW` | Alternate rocket sound, the classic "GOW-Sshhh" of the player's launcher. |
| `RUSHWIND.RAW` | Sound of rushing air, don't recall this being used. |
| `SGCOCK1.RAW` | Weapon cocking sound? Used for ammo pickups. |
| `SGCOCK2.RAW` | Weapon cocking sound? Used for ammo pickups. |
| `SHOTS2.RAW` | Slug-thrower firing sound. Pretty sure this is the assault rifle. |
| `SHOTS3.RAW` | Seems identical to `SHOTS2.RAW`. |
| `SHOTS5.RAW` | Slug-thrower firing sound. Pretty sure this is the Uzi. |
| `SHTGUN.RAW` | Shotgun fire-and-pump sound, my favourite. |
| `SKID2.RAW` | Squealing tyres, used when hitting the brakes in the Jeep. |
| `SOFTERM.RAW` | Alternate Terminator movement sound, feels like a mix of sounds. |
| `SWISH1.RAW` | Sound of swinging the lead pipe. |
| `TANK1.RAW` | Tank movement/treads. |
| `TERMITOR.RAW` | Terminator movement sound, rapid and grating. |
| `TEST.RAW` | The same Human Machine Interface "Space Station Mercury" sound also found in `TEST.WAV` in the `GAMEDATA` folder. |
| `TURRET.RAW` | Turret turning sound? Don't recall this being used. Very obviously the beginning of the Terminator head-turn sound from the menu screen, but cut off. |
| `UZICOCK3.RAW` | Gun cocking sound. I recognise it as the gun cock that plays when selecting a game file to load. |
| `WATER.RAW` | Ambient water trickling SFX. |
| `WIND.RAW` | Ambient wind noise. |
| `WINDMET.RAW` | Ambient wind noise w/ sound of swinging/creaking metal mixed in. |
| `WINDWOD.RAW` | Ambient wind noise w/ sound of creaking tree branch mixed in. |