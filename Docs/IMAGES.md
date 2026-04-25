# Future Shock Image Archive

Unlike Daggerfall, where images are usually kept in individual files, Future 
Shock stashes them all in a BSA archive. The files in here are of two types:

1. `*.IMG` files, which are essentially compressed bitmaps. Firstly, they are 
run-length encoded, and secondly they have had their individual colour codes 
replaced with index lookups on an associated palette file. `*.IMG` files appear
to be identical to [those used in Daggerfall](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/IMG)
and can be loaded with the same code. Their associated palette files also share
the same formats as Daggerfall's [`*.COL` and `*.PAL` files](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/Palette),
though in Future Shock both of these have the same `*.COL` extension.

2. `*.CFA` files, multi-frame image files originating in The Elder Scrolls:
Arena, similar to Daggerfall's `*.CIF` files. These are used in Daggerfall, but
were not covered in the UESP wiki, so [I added documentation for them there](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/CFA) 
rather than here.

## Image Index

The order of numbered files that relate to characters - eye/mouth 
animations, stills, etc - match the character codes described in the 
[text archive documentation](./TEXT.md). The only exceptions are the mission
briefing wide-shot sprites, which follow the same order but are zero-indexed 
from `BRIEF100` upward. I assume this was done to irritate _me specifically_,
thirty years later. 

For weapons, there appear to be no static sprites for their onscreen appearance
but it makes sense that they would simply show the weapon's CFA animation 
onscreen the whole time and pause it at frame 0 when the user is not attacking.

Some images are overly dark, and may need their RGB values boosted. The
Daggerfall Unity code has reference to this happening with images that have
embedded palettes. In Future Shock these images seem to be those related to the
demo/promotional material, not those used in the game. Added a column below to
note which images need boosting.

|Filename|Description|Default Palette|Needs Boost|
|---|---|---|---|
|`BLINK001.CFA`|Blinking animation for Bill Hanover's eyes|`BRIEF.COL`|No|
|`BLINK002.CFA`|Blinking animation for Laurie Brinks' eyes|`BRIEF.COL`|No|
|`BLINK003.CFA`|Blinking animation for John Connor's eyes|`BRIEF.COL`|No|
|`BLINK004.CFA`|Blinking animation for Kyle Reese's eyes|`BRIEF.COL`|No|
|`BLINK005.CFA`|Blinking animation for Thomas Jensen's eyes|`BRIEF.COL`|No|
|`BLINK006.CFA`|Blinking animation for Kathryn Parker's eyes|`BRIEF.COL`|No|
|`BLINK007.CFA`|Blinking animation for Milton Bishop's eyes|`BRIEF.COL`|No|
|`BLINK008.CFA`|Blinking animation for John Connor's eyes (w/ scar)|`BRIEF.COL`|No|
|`BLINK009.CFA`|Blinking animation for Sgt. Roberts' eyes|`BRIEF.COL`|No|
|`BLINK011.CFA`|Blinking animation for John Connor's eyes (outdoors)|`BRIEF.COL`|No|
|`BRIEF000.IMG`|Background-only wide-shot of the briefing room, for character sprites to be rendered over.|`BRIEF.COL`|No|
|`BRIEF001.IMG`|Mission briefing close-up of Bill Hanover|`BRIEF.COL`|No|
|`BRIEF002.IMG`|Mission briefing close-up of Laurie Brinks|`BRIEF.COL`|No|
|`BRIEF003.IMG`|Mission briefing close-up of John Connor|`BRIEF.COL`|No|
|`BRIEF004.IMG`|Mission briefing close-up of Kyle Reese|`BRIEF.COL`|No|
|`BRIEF005.IMG`|Mission briefing close-up of Thomas Jensen|`BRIEF.COL`|No|
|`BRIEF006.IMG`|Mission briefing close-up of Kathryn Parker|`BRIEF.COL`|No|
|`BRIEF007.IMG`|Mission briefing close-up of Milton Bishop|`BRIEF.COL`|No|
|`BRIEF008.IMG`|Mission briefing close-up of John Connor (w/ scar)|`BRIEF.COL`|No|
|`BRIEF009.IMG`|Mission briefing close-up of Sgt. Roberts|`BRIEF.COL`|No|
|`BRIEF010.IMG`|Mission briefing close-up of hand-held radio|`BRIEF.COL`|No|
|`BRIEF011.IMG`|Mission briefing close-up of John Connor (outdoors)|`BRIEF.COL`|No|
|`BRIEF100.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|No|
|`BRIEF101.IMG`|Mission briefing wide-shot sprite of Laurie Brinks|`BRIEF.COL`|No|
|`BRIEF102.IMG`|Mission briefing wide-shot sprite of John Connor|`BRIEF.COL`|No|
|`BRIEF103.IMG`|Mission briefing wide-shot sprite of Kyle Reese|`BRIEF.COL`|No|
|`BRIEF104.IMG`|Mission briefing wide-shot sprite of Thomas Jensen|`BRIEF.COL`|No|
|`BRIEF105.IMG`|Mission briefing wide-shot sprite of Kathryn Parker|`BRIEF.COL`|No|
|`BRIEF106.IMG`|Mission briefing wide-shot sprite of Milton Bishop|`BRIEF.COL`|No|
|`BRIEF107.IMG`|Mission briefing wide-shot sprite of John Connor (w/ scar)|`BRIEF.COL`|No|
|`BRIEFBAR.IMG`|Briefing screen menu bar, including button text. (Overlaid in-game with other images?)|`BRIEF.COL`|No|
|`BROW001.CFA`|Eyebrow animation for Bill Hanover|`BRIEF.COL`|No|
|`BROW002.CFA`|Eyebrow animation for Laurie Brinks|`BRIEF.COL`|No|
|`BROW003.CFA`|Eyebrow animation for John Connor|`BRIEF.COL`|No|
|`BROW004.CFA`|Eyebrow animation for Kyle Reese|`BRIEF.COL`|No|
|`BROW005.CFA`|Eyebrow animation for Thomas Jensen|`BRIEF.COL`|No|
|`BROW006.CFA`|Eyebrow animation for Kathryn Parker|`BRIEF.COL`|No|
|`BROW007.CFA`|Eyebrow animation for Milton Bishop|`BRIEF.COL`|No|
|`BROW008.CFA`|Eyebrow animation for John Connor (w/ scar)|`BRIEF.COL`|No|
|`BROW009.CFA`|Eyebrow animation for Sgt. Roberts|`BRIEF.COL`|No|
|`BUTTON00.IMG`|Up arrow button for scroll area, inactive state.|`BRIEF.COL`|No|
|`BUTTON01.IMG`|Up arrow button for scroll area, active state.|`BRIEF.COL`|No|
|`BUTTON02.IMG`|Down arrow button for scroll area, inactive state.|`BRIEF.COL`|No|
|`BUTTON03.IMG`|Down arrow button for scroll area, active state.|`BRIEF.COL`|No|
|`BUTTON04.IMG`|"Begin" button, inactive state.|`BRIEF.COL`|No|
|`BUTTON05.IMG`|"Begin" button, active state.|`BRIEF.COL`|No|
|`BUTTON06.IMG`|"Briefing" button, inactive state.|`BRIEF.COL`|No|
|`BUTTON07.IMG`|"Briefing" button, active state.|`BRIEF.COL`|No|
|`BUTTON08.IMG`|"Tactical" button, inactive state.|`BRIEF.COL`|No|
|`BUTTON09.IMG`|"Tactical" button, active state.|`BRIEF.COL`|No|
|`BUTTON10.IMG`|"Statistics" button, inactive state.|`BRIEF.COL`|No|
|`BUTTON11.IMG`|"Statistics" button, active state.|`BRIEF.COL`|No|
|`BUTTON12.IMG`|"Exit" button, inactive state.|`BRIEF.COL`|No|
|`BUTTON13.IMG`|"Exit" button, active state.|`BRIEF.COL`|No|
|`BUTTON14.IMG`|Left turn arrow button, inactive state.|`SHOCK.COL`|No|
|`BUTTON15.IMG`|Left turn arrow button, active state.|`SHOCK.COL`|No|
|`BUTTON16.IMG`|Right turn arrow button, inactive state.|`SHOCK.COL`|No|
|`BUTTON17.IMG`|Right turn arrow button, active state.|`SHOCK.COL`|No|
|`BUTTON18.IMG`|"Look up" arrow button, inactive state.|`SHOCK.COL`|No|
|`BUTTON19.IMG`|"Look up" arrow button, active state.|`SHOCK.COL`|No|
|`BUTTON20.IMG`|"Look down" arrow button, inactive state.|`SHOCK.COL`|No|
|`BUTTON21.IMG`|"Look down" arrow button, active state.|`SHOCK.COL`|No|
|`BUTTON22.IMG`|"Exit" button, inactive state.|`SHOCK.COL`|No|
|`BUTTON23.IMG`|"Exit" button, active state.|`SHOCK.COL`|No|
|`COMPASS.IMG`|In-game compass dial, from N through other ordinals and back to N, so it can be scrolled.|`SHOCK.COL`|No|
|`CONBTN.CFA`|Highlight states for tab buttons on control options screen - "joystick","mouse","default","exit".|`START.COL`|No|
|`CONTROLS.IMG`|Full-screen background for the "Control Configuration" screen.|`START.COL`|No|
|`CREDITS.IMG`|Background image for credits screen.|`CREDITS.COL`|No|
|`CROSHAIR.IMG`|Large crosshair, for things like the jeep's laser cannon.|`SHOCK.COL`|No|
|`CROSHAR2.IMG`|Small crosshair.|`SHOCK.COL`|No|
|`DEMOBTN.CFA`|Highlight states for demo screen options "Begin" and "DOS".|`DEMOSCR3.COL`|Yes|
|`DEMOSCR2.IMG`|Screenshot gallery from game demo.|`DEMOSCRN.COL`|Yes|
|`DEMOSCR3.IMG`|Title screen from game demo.|`DEMOSCR3.COL`|Yes|
|`DEMOSCRN.IMG`|Identical to `DEMOSCR2.IMG`, except that one of the screenshots in the gallery is different.|`DEMOSCRN.COL`|Yes|
|`DETAIL.IMG`|"Render Detail" option dialog.|`START.COL`|No|
|`DETBTN.CFA`|"Render Detail" button highlight states - "low", "med", "high"|`START.COL`|No|
|`DRIVE.IMG`|Original demo/concept screenshot for the driving interface. Significantly different from final in-game version, seems to have fixed forward vision rather than free-look.|`SHOCK.COL`|No|
|`EYES001.CFA`|Eye movement animation for Bill Hanover|`BRIEF.COL`|No|
|`EYES002.CFA`|Eye movement animation for Laurie Brinks|`BRIEF.COL`|No|
|`EYES003.CFA`|Eye movement animation for John Connor|`BRIEF.COL`|No|
|`EYES004.CFA`|Eye movement animation for Kyle Reese|`BRIEF.COL`|No|
|`EYES005.CFA`|Eye movement animation for Thomas Jensen|`BRIEF.COL`|No|
|`EYES006.CFA`|Eye movement animation for Kathryn Parker|`BRIEF.COL`|No|
|`EYES007.CFA`|Eye movement animation for Milton Bishop|`BRIEF.COL`|No|
|`EYES008.CFA`|Eye movement animation for John Connor (w/ scar)|`BRIEF.COL`|No|
|`EYES009.CFA`|Eye movement animation for Sgt. Roberts|`BRIEF.COL`|No|
|`EYES011.CFA`|Eye movement animation for John Connor (outdoors)|`BRIEF.COL`|No|
|`FAILED.IMG`|Mission failure text alert, "Mission Failed, Soldier!" in the English version.|`SHOCK.COL`|No|
|`FIRST01.IMG`|Initial-state screenshot (level preview) for level 01.|`SHOCK.COL`|No|
|`FIRST02.IMG`|Initial-state screenshot (level preview) for level 02.|`SHOCK.COL`|No|
|`FIRST03.IMG`|Initial-state screenshot (level preview) for level 03.|`SHOCK.COL`|No|
|`FIRST05.IMG`|Initial-state screenshot (level preview) for level 05.|`SHOCK.COL`|No|
|`FIRST06.IMG`|Initial-state screenshot (level preview) for level 06.|`SHOCK.COL`|No|
|`FIRST07.IMG`|Initial-state screenshot (level preview) for level 07.|`SHOCK.COL`|No|
|`FIRST09.IMG`|Initial-state screenshot (level preview) for level 09.|`SHOCK.COL`|No|
|`FIRST10.IMG`|Initial-state screenshot (level preview) for level 10.|`SHOCK.COL`|No|
|`FIRST11.IMG`|Initial-state screenshot (level preview) for level 11.|`SHOCK.COL`|No|
|`FIRST12.IMG`|Initial-state screenshot (level preview) for level 12.|`SHOCK.COL`|No|
|`FIRST13.IMG`|Initial-state screenshot (level preview) for level 13.|`SHOCK.COL`|No|
|`FIRST14.IMG`|Initial-state screenshot (level preview) for level 14.|`SHOCK.COL`|No|
|`FIRST15.IMG`|Initial-state screenshot (level preview) for level 15.|`SHOCK.COL`|No|
|`FIRST16.IMG`|Initial-state screenshot (level preview) for level 16.|`SHOCK.COL`|No|
|`FIRST17.IMG`|Initial-state screenshot (level preview) for level 17.|`SHOCK.COL`|No|
|`FIRST18.IMG`|Initial-state screenshot (level preview) for level 18.|`SHOCK.COL`|No|
|`FIRST19.IMG`|Initial-state screenshot (level preview) for level 19.|`SHOCK.COL`|No|
|`FLY.IMG`|Original demo/concept screenshot for the flying interface. Significantly different from final in-game version.|`SHOCK.COL`|No|
|`HEDSHOT.IMG`|Title screen almost identical to the one in `DEMOSCR3.IMG`. Could not find a valid palette in the game files for this image. It's possible this is a leftover image with no palette, or that it has an embedded palette done differently to those in Daggerfall.|N/A|N/A|
|`JOYBTN.CFA`|Highlight states for buttons on the joystick calibration dialog.|`START.COL`|No|
|`JOYSTICK.IMG`|Joystick calibration dialog.|`START.COL`|No|
|`JOYSTIK2.CFA`|Instruction dialogs for joystick calibration, not including `JOYSTIK2.IMG`.|`START.COL`|No|
|`JOYSTIK2.IMG`|Joystick calibration instructions, modal dialog.|`START.COL`|No|
|`LOAD.IMG`|"Load Game" (save game listing) screen layout.|`START.COL`|No|
|`LOADBTN.IMG`|Active state for the "Exit" image that is part of `LOAD.IMG`.|`START.COL`|No|
|`LOADING.IMG`|Loading screen centre text, "Loading..." in English version.|`SHOCK.COL`|No|
|`MAIN1.IMG`|Main menu screen's menu bar.|`START.COL`|No|
|`MAIN1BTN.CFA`|Highlight states for main menu buttons.|`START.COL`|No|
|`MAIN2.IMG`|Pause screen's menu bar.|`START.COL`|No|
|`MAIN2BTN.CFA`|Seems identical to `MAIN1BTN.CFA`|`START.COL`|No|
|`MAPBAR.IMG`|Blank horizontal bar from map screen.|`SHOCK.COL`|No|
|`MAPBAR1.IMG`|Camera control buttons on map screen.|`SHOCK.COL`|No|
|`MAPBAR2.IMG`|Identical to `MAPBAR.IMG`|`SHOCK.COL`|No|
|`MAPBTN.CFA`|Highlight states for arrow/exit buttons on map screen.|`START.COL`|No|
|`MAPGRID.IMG`|Background grid image for map screen.|`SHOCK.COL`|No|
|`MDMAIM.IMG`|Small crosshair - double check which crosshairs are used where.|`SHOCK.COL`|No|
|`MENU000.IMG`|Scrolling text area, used on briefing/tactical screens.|`BRIEF.COL`|No|
|`MENU004.IMG`|Simplified menu bar with only "Begin" and "Exit. No matching palette. Assumed to be left over from demo code.|N/A|N/A|
|`MOUSBTN.CFA`|Highlight states for "reverse vertical" and "exit" buttons on mouse options screen.|`START.COL`|No|
|`MOUSE.IMG`|"Mouse Sensitivity" setting dialog.|`START.COL`|No|
|`MOUTH001.CFA`|Mouth animation for Bill Hanover|`BRIEF.COL`|No|
|`MOUTH002.CFA`|Mouth animation for Laurie Brinks|`BRIEF.COL`|No|
|`MOUTH003.CFA`|Mouth animation for John Connor|`BRIEF.COL`|No|
|`MOUTH004.CFA`|Mouth animation for Kyle Reese|`BRIEF.COL`|No|
|`MOUTH005.CFA`|Mouth animation for Thomas Jensen|`BRIEF.COL`|No|
|`MOUTH006.CFA`|Mouth animation for Kathryn Parker|`BRIEF.COL`|No|
|`MOUTH007.CFA`|Mouth animation for Milton Bishop|`BRIEF.COL`|No|
|`MOUTH008.CFA`|Mouth animation for John Connor (w/ scar)|`BRIEF.COL`|No|
|`MOUTH009.CFA`|Mouth animation for Sgt. Roberts|`BRIEF.COL`|No|
|`MOUTH011.CFA`|Mouth animation for John Connor (outside)|`BRIEF.COL`|No|
|`N_A.IMG`|"Not available in demo" text|`START.COL`|No|
|`NAME.IMG`|Player name/callsign dialog used when starting a new game.|`START.COL`|No|
|`OPTBTN.CFA`|Button highlight states and red horizonal scale bars for main options dialog.|`START.COL`|No|
|`OPTIONS.IMG`|Main options screen.|`START.COL`|No|
|`PANEL0.IMG`|In-game player UI bar (health, radiation, radio, etc)|`SHOCK.COL`|No|
|`PANEL1.IMG`|Original driving UI overlay seen in `DRIVE.IMG`, not used in final game.|`SHOCK.COL`|No|
|`PANEL2.IMG`|Original flying UI overlay seen in `FLY.IMG`, not used in final game.|`SHOCK.COL`|No|
|`PANELBAR.IMG`|Red bar used to represent health/armour levels in player UI.|`SHOCK.COL`|No|
|`PLASMA.IMG`|Pre-release concept/demo screenshot of the player firing the plasma cannon. Pretty similar to the final release, though with some obvious colour/layout changes.|`SHOCK.COL`|No|
|`POINTER.IMG`|Mouse pointer.|`SHOCK.COL`|No|
|`QUIT.IMG`|"Quit game" dialog.|`START.COL`|No|
|`QUITBTN.CFA`|Button highlight states for "yes"/"no" quit confirmation dialog.|`START.COL`|No|
|`QUITMAIN.IMG`|"Quit to menu" dialog.|`START.COL`|No|
|`RADCOUNT.CFA`|"Safe"/"Caution"/"Danger" states for in-game geiger counter.|`SHOCK.COL`|No|
|`RECONFIG.IMG`|Duplicate key binding warning dialog.|`START.COL`|No|
|`RESTART.IMG`|"Restart mission" dialog.|`START.COL`|No|
|`RESTBTN.CFA`|Button highlights for restart confirmation "yes"/"no" dialog.|`START.COL`|No|
|`RUN.IMG`|Screenshot of player firing the plasma cannon. Unlike `PLASMA.IMG` this appears to match the final release UI.|`SHOCK.COL`|No|
|`SAVE.IMG`|"Save Game" screen - game slot listing.|`START.COL`|No|
|`SAVED.IMG`|Game save success confirmation.|`START.COL`|No|
|`SAVED2.IMG`|Game save failure message.|`START.COL`|No|
|`START.IMG`|Main title screen.|`START.COL`|No|
|`TACTBAK.IMG`|Circuit board background image for "Tactical" screen.|`BRIEF.COL`|No|
|`TXTSCRN1.IMG`|Bethesda logo. Seems dark?|`TXTSCRN1.COL`|Yes|
|`TXTSCRN2.IMG`|Screen capture of H/K fighter flying back, still image from the game's intro movie.|`TXTSCRN2.COL`|Yes|
|`TXTSCRN3.IMG`|Same image as `TXTSCRN2.IMG`, but with expository text overlaid. These intro screens are from the game's demo.|`TXTSCRN2.COL`|Yes|
|`TXTSCRN4.IMG`|Same image as `TXTSCRN2.IMG`, but with a feature list for the full version of the game.|`TXTSCRN2.COL`|Yes|
|`TXTSCRN5.IMG`|Nuclear explosion still from the game's intro movie, with phone number and website for purchasing full version of the game.|`TXTSCRN5.COL`|Yes|
|`WEAPON00.CFA`|Weapon attack animation for the lead pipe.|`SHOCK.COL`|No|
|`WEAPON00.IMG`|Weapon inventory image for the lead pipe.|`SHOCK.COL`|No|
|`WEAPON01.CFA`|Weapon attack animation for the Uzi 9mm.|`SHOCK.COL`|No|
|`WEAPON01.IMG`|Weapon inventory image for the Uzi 9mm.|`SHOCK.COL`|No|
|`WEAPON02.CFA`|Weapon attack animation for the assault rifle.|`SHOCK.COL`|No|
|`WEAPON02.IMG`|Weapon inventory image for the assault rifle.|`SHOCK.COL`|No|
|`WEAPON03.CFA`|Weapon attack animation for the machine gun.|`SHOCK.COL`|No|
|`WEAPON03.IMG`|Weapon inventory image for the machine gun.|`SHOCK.COL`|No|
|`WEAPON04.CFA`|Weapon attack animation for the shotgun.|`SHOCK.COL`|No|
|`WEAPON04.IMG`|Weapon inventory image for the shotgun.|`SHOCK.COL`|No|
|`WEAPON05.CFA`|Weapon attack animation for the grenade launcher.|`SHOCK.COL`|No|
|`WEAPON05.IMG`|Weapon inventory image for the grenade launcher. It's definitely not a pulse rifle, no sir.|`SHOCK.COL`|No|
|`WEAPON06.CFA`|Weapon attack animation for the rocket launcher.|`SHOCK.COL`|No|
|`WEAPON06.IMG`|Weapon inventory image for the rocket launcher.|`SHOCK.COL`|No|
|`WEAPON07.CFA`|Weapon attack animation for the laser rifle.|`SHOCK.COL`|No|
|`WEAPON07.IMG`|Weapon inventory image for the laser rifle.|`SHOCK.COL`|No|
|`WEAPON08.CFA`|Weapon attack animation for the laser cannon.|`SHOCK.COL`|No|
|`WEAPON08.IMG`|Weapon inventory image for the laser cannon.|`SHOCK.COL`|No|
|`WEAPON09.CFA`|Weapon attack animation for the plasma pistol.|`SHOCK.COL`|No|
|`WEAPON09.IMG`|Weapon inventory image for the plasma pistol.|`SHOCK.COL`|No|
|`WEAPON10.CFA`|Weapon attack animation for the plasma rifle.|`SHOCK.COL`|No|
|`WEAPON10.IMG`|Weapon inventory image for the plasma rifle.|`SHOCK.COL`|No|
|`WEAPON11.CFA`|Weapon attack animation for the plasma cannon.|`SHOCK.COL`|No|
|`WEAPON11.IMG`|Weapon inventory image for the plasma cannon.|`SHOCK.COL`|No|
|`WEAPON12.CFA`|Weapon attack animation for the Uzi 9mm, but red? Not sure where this is used, if at all. Do not recognise it or its inventory image below from the final game.|`SHOCK.COL`|No|
|`WEAPON12.IMG`|Weapon inventory image for the Uzi 9mm, but red.|`SHOCK.COL`|No|
|`WEAPON13.IMG`|Weapon inventory image for the pipe bomb.|`SHOCK.COL`|No|
|`WEAPON14.IMG`|Weapon inventory image for the molotov cocktail.|`SHOCK.COL`|No|
|`WEAPON15.IMG`|Weapon inventory image for the fragmentation grenade.|`SHOCK.COL`|No|
|`WEAPON16.IMG`|Weapon inventory image for an unknown grenade type. Not used in the final game.|`SHOCK.COL`|No|
|`WEAPON17.IMG`|Weapon inventory image for the canister bomb.|`SHOCK.COL`|No|
|`WEAPON18.IMG`|Weapon inventory image for the satchel charge.|`SHOCK.COL`|No|
|`WEAPON19.IMG`|Weapon inventory image for the vehicle-mounted plasma cannon.|`SHOCK.COL`|No|
|`WEAPON20.IMG`|Identical to `WEAPON19.IMG`.|`SHOCK.COL`|No|
|`WEAPON21.IMG`|Weapon inventory image for the vehicle-mounted rocket launcher.|`SHOCK.COL`|No|
|`WEAPON22.IMG`|Identical to `WEAPON21.IMG`.|`SHOCK.COL`|No|
|`WEAPON23.IMG`|Weapon inventory image for the vehicle-mounted laser cannon.|`SHOCK.COL`|No|
|`WEAPON24.IMG`|Identical to `WEAPON21.IMG`.|`SHOCK.COL`|No|
|`WELLDONE.IMG`|In-game mission success text, "Well Done, Soldier!" in the English version.|`SHOCK.COL`|No|