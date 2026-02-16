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

|Filename|Description|Associated Palette (If Applicable)|
|---|---|---|
|`BLINK001.CFA`|||
|`BLINK002.CFA`|||
|`BLINK003.CFA`|||
|`BLINK004.CFA`|||
|`BLINK005.CFA`|||
|`BLINK006.CFA`|||
|`BLINK007.CFA`|||
|`BLINK008.CFA`|||
|`BLINK009.CFA`|||
|`BLINK010.CFA`|||
|`BLINK011.CFA`|||
|`BRIEF000.IMG`|Background-only wide-shot of the briefing room, for small caracter sprites to be rendered over.|
|`BRIEF001.IMG`|Mission briefing close-up of Bill Hanover|`BRIEF.COL`|
|`BRIEF002.IMG`|Mission briefing close-up of Laurie Brinks|`BRIEF.COL`|
|`BRIEF003.IMG`|Mission briefing close-up of John Connor|`BRIEF.COL`|
|`BRIEF004.IMG`|Mission briefing close-up of Kyle Reese|`BRIEF.COL`|
|`BRIEF005.IMG`|Mission briefing close-up of Thomas Jensen|`BRIEF.COL`|
|`BRIEF006.IMG`|Mission briefing close-up of Kathryn Parker|`BRIEF.COL`|
|`BRIEF007.IMG`|Mission briefing close-up of Milton Bishop|`BRIEF.COL`|
|`BRIEF008.IMG`|Mission briefing close-up of John Connor (w/ scar)|`BRIEF.COL`|
|`BRIEF009.IMG`|Mission briefing close-up of Sgt. Roberts|`BRIEF.COL`|
|`BRIEF010.IMG`|Mission briefing close-up of hand-held radio|`BRIEF.COL`|
|`BRIEF011.IMG`|Mission briefing close-up of John Connor (outdoors)|`BRIEF.COL`|
|`BRIEF100.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEF101.IMG`|Mission briefing wide-shot sprite of Laurie Brinks|`BRIEF.COL`|
|`BRIEF102.IMG`|Mission briefing wide-shot sprite of |`BRIEF.COL`|
|`BRIEF103.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEF104.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEF105.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEF106.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEF107.IMG`|Mission briefing wide-shot sprite of Bill Hanover|`BRIEF.COL`|
|`BRIEFBAR.IMG`|Briefing screen menu bar, including button text. (Overlaid in-game with other images?)|`BRIEF.COL`|
|`BROW001.CFA`|||
|`BROW002.CFA`|||
|`BROW003.CFA`|||
|`BROW004.CFA`|||
|`BROW005.CFA`|||
|`BROW006.CFA`|||
|`BROW007.CFA`|||
|`BROW008.CFA`|||
|`BROW009.CFA`|||
|`BUTTON00.IMG`|Up arrow button for scroll area, inactive state.|`BRIEF.COL`|
|`BUTTON01.IMG`|Up arrow button for scroll area, active state.|`BRIEF.COL`|
|`BUTTON02.IMG`|Down arrow button for scroll area, inactive state.|`BRIEF.COL`|
|`BUTTON03.IMG`|Down arrow button for scroll area, active state.|`BRIEF.COL`|
|`BUTTON04.IMG`|"Begin" button, inactive state.|`BRIEF.COL`|
|`BUTTON05.IMG`|"Begin" button, active state.|`BRIEF.COL`|
|`BUTTON06.IMG`|"Briefing" button, inactive state.|`BRIEF.COL`|
|`BUTTON07.IMG`|"Briefing" button, active state.|`BRIEF.COL`|
|`BUTTON08.IMG`|"Tactical" button, inactive state.|`BRIEF.COL`|
|`BUTTON09.IMG`|"Tactical" button, active state.|`BRIEF.COL`|
|`BUTTON10.IMG`|"Statistics" button, inactive state.|`BRIEF.COL`|
|`BUTTON11.IMG`|"Statistics" button, active state.|`BRIEF.COL`|
|`BUTTON12.IMG`|"Exit" button, inactive state.|`BRIEF.COL`|
|`BUTTON13.IMG`|"Exit" button, active state.|`BRIEF.COL`|
|`BUTTON14.IMG`|Left turn arrow button, inactive state.|`SHOCK.COL`|
|`BUTTON15.IMG`|Left turn arrow button, active state.|`SHOCK.COL`|
|`BUTTON16.IMG`|Right turn arrow button, inactive state.|`SHOCK.COL`|
|`BUTTON17.IMG`|Right turn arrow button, active state.|`SHOCK.COL`|
|`BUTTON18.IMG`|"Look up" arrow button, inactive state.|`SHOCK.COL`|
|`BUTTON19.IMG`|"Look up" arrow button, active state.|`SHOCK.COL`|
|`BUTTON20.IMG`|"Look down" arrow button, inactive state.|`SHOCK.COL`|
|`BUTTON21.IMG`|"Look down" arrow button, active state.|`SHOCK.COL`|
|`BUTTON22.IMG`|"Exit" button, inactive state.|`SHOCK.COL`|
|`BUTTON23.IMG`|"Exit" button, active state.|`SHOCK.COL`|
|`COMPASS.IMG`|In-game compass dial, from N through other ordinals and back to N, so it can be scrolled.|`SHOCK.COL`|
|`CONBTN.CFA`|||
|`CONTROLS.IMG`|Full-screen background for the "Control Configuration" screen.|`START.COL`|
|`CREDITS.IMG`|Background image for credits screen.|`CREDITS.COL`|
|`CROSHAIR.IMG`|Large crosshair, for things like the jeep's laser cannon.|`SHOCK.COL`|
|`CROSHAR2.IMG`|Small crosshair.|`SHOCK.COL`|
|`DEMOBTN.CFA`|||
|`DEMOSCR2.IMG`|Screenshot gallery from game demo. Image is very dark for some reason, might benefit from the RGB value-boosting used with embedded palettes.|`DEMOSCRN.COL`|
|`DEMOSCR3.IMG`|Title screen from game demo, also dark.|`DEMOSCR3.COL`|
|`DEMOSCRN.IMG`|Identical to `DEMOSCR2.IMG`, except that one of the screenshots in the gallery is different.|`DEMOSCRN.COL`|
|`DETAIL.IMG`|"Render Detail" option dialog.|`START.COL`|
|`DETBTN.CFA`|||
|`DRIVE.IMG`|Original demo/concept screenshot for the driving interface. Significantly different from final in-game version, seems to have fixed forward vision rather than free-look.|`SHOCK.COL`|
|`EYES001.CFA`|||
|`EYES002.CFA`|||
|`EYES003.CFA`|||
|`EYES004.CFA`|||
|`EYES005.CFA`|||
|`EYES006.CFA`|||
|`EYES007.CFA`|||
|`EYES008.CFA`|||
|`EYES009.CFA`|||
|`EYES010.CFA`|||
|`EYES011.CFA`|||
|`FAILED.IMG`|Mission failure text alert, "Mission Failed, Soldier!" in the English version.|`SHOCK.COL`|
|`FIRST01.IMG`|Initial-state screenshot (level preview) for level 01.|`SHOCK.COL`|
|`FIRST02.IMG`|Initial-state screenshot (level preview) for level 02.|`SHOCK.COL`|
|`FIRST03.IMG`|Initial-state screenshot (level preview) for level 03.|`SHOCK.COL`|
|`FIRST05.IMG`|Initial-state screenshot (level preview) for level 05.|`SHOCK.COL`|
|`FIRST06.IMG`|Initial-state screenshot (level preview) for level 06.|`SHOCK.COL`|
|`FIRST07.IMG`|Initial-state screenshot (level preview) for level 07.|`SHOCK.COL`|
|`FIRST09.IMG`|Initial-state screenshot (level preview) for level 09.|`SHOCK.COL`|
|`FIRST10.IMG`|Initial-state screenshot (level preview) for level 10.|`SHOCK.COL`|
|`FIRST11.IMG`|Initial-state screenshot (level preview) for level 11.|`SHOCK.COL`|
|`FIRST12.IMG`|Initial-state screenshot (level preview) for level 12.|`SHOCK.COL`|
|`FIRST13.IMG`|Initial-state screenshot (level preview) for level 13.|`SHOCK.COL`|
|`FIRST14.IMG`|Initial-state screenshot (level preview) for level 14.|`SHOCK.COL`|
|`FIRST15.IMG`|Initial-state screenshot (level preview) for level 15.|`SHOCK.COL`|
|`FIRST16.IMG`|Initial-state screenshot (level preview) for level 16.|`SHOCK.COL`|
|`FIRST17.IMG`|Initial-state screenshot (level preview) for level 17.|`SHOCK.COL`|
|`FIRST18.IMG`|Initial-state screenshot (level preview) for level 18.|`SHOCK.COL`|
|`FIRST19.IMG`|Initial-state screenshot (level preview) for level 19.|`SHOCK.COL`|
|`FLY.IMG`|Original demo/concept screenshot for the flying interface. Significantly different from final in-game version.|`SHOCK.COL`|
|`HEDSHOT.IMG`|Title screen almost identical to the one in `DEMOSCR3.IMG`. Could not find a valid palette in the game files for this image. It's possible this is a leftover image with no palette, or that it has an embedded palette done differently to those in Daggerfall.|N/A|
|`JOYBTN.CFA`|||
|`JOYSTICK.IMG`|Joystick calibration dialog.|`START.COL`|
|`JOYSTIK2.CFA`|||
|`JOYSTIK2.IMG`|Joystick calibration instructions, modal dialog.|`START.COL`|
|`LOAD.IMG`|"Load Game" (save game listing) screen layout.|`START.COL`|
|`LOADBTN.IMG`|Active state for the "Exit" image that is part of `LOAD.IMG`.|`START.COL`|
|`LOADING.IMG`|Loading screen centre text, "Loading..." in English version.|`SHOCK.COL`|
|`MAIN1.IMG`|Main menu screen's menu bar.|`START.COL`|
|`MAIN1BTN.CFA`|||
|`MAIN2.IMG`|Pause screen's menu bar.|`START.COL`|
|`MAIN2BTN.CFA`|||
|`MAPBAR.IMG`|Blank horizontal bar from map screen.|`SHOCK.COL`|
|`MAPBAR1.IMG`|Camera control buttons on map screen.|`SHOCK.COL`|
|`MAPBAR2.IMG`|Identical to `MAPBAR.IMG`|`SHOCK.COL`|
|`MAPBTN.CFA`|||
|`MAPGRID.IMG`|Background grid image for map screen.|`SHOCK.COL`|
|`MDMAIM.IMG`|Small crosshair - double check which crosshairs are used where.|`SHOCK.COL`|
|`MENU000.IMG`|Scrolling text area, used on briefing/tactical screens.|`BRIEF.COL`|
|`MENU004.IMG`|Simplified menu bar with only "Begin" and "Exit. No matching palette. Assumed to be left over from demo code.|N/A|
|`MOUSBTN.CFA`|||
|`MOUSE.IMG`|"Mouse Sensitivity" setting dialog.|`START.COL`|
|`MOUTH001.CFA`|||
|`MOUTH002.CFA`|||
|`MOUTH003.CFA`|||
|`MOUTH004.CFA`|||
|`MOUTH005.CFA`|||
|`MOUTH006.CFA`|||
|`MOUTH007.CFA`|||
|`MOUTH008.CFA`|||
|`MOUTH009.CFA`|||
|`MOUTH010.CFA`|||
|`MOUTH011.CFA`|||
|`N_A.IMG`|"Not available in demo" text|`START.COL`|
|`NAME.IMG`|Player name/callsign dialog used when starting a new game.|`START.COL`|
|`OPTBTN.CFA`|||
|`OPTIONS.IMG`|Main options screen.|`START.COL`|
|`PANEL0.IMG`|In-game player UI bar (health, radiation, radio, etc)|`SHOCK.COL`|
|`PANEL1.IMG`|Original driving UI overlay seen in `DRIVE.IMG`, not used in final game.|`SHOCK.COL`|
|`PANEL2.IMG`|Original flying UI overlay seen in `FLY.IMG`, not used in final game.|`SHOCK.COL`|
|`PANELBAR.IMG`|Red bar used to represent health/armour levels in player UI.|`SHOCK.COL`|
|`PLASMA.IMG`|Pre-release concept/demo screenshot of the player firing the plasma cannon. Pretty similar to the final release, though with some obvious colour/layout changes.|`SHOCK.COL`|
|`POINTER.IMG`|Mouse pointer.|`SHOCK.COL`|
|`QUIT.IMG`|"Quit game" dialog.|`START.COL`|
|`QUITBTN.CFA`|||
|`QUITMAIN.IMG`|"Quit to menu" dialog.|`START.COL`|
|`RADCOUNT.CFA`|||
|`RECONFIG.IMG`|Duplicate key binding warning dialog.|`START.COL`|
|`RESTART.IMG`|"Restart mission" dialog.|`START.COL`|
|`RESTBTN.CFA`|||
|`RUN.IMG`|Screenshot of player firing the plasma cannon. Unlike `PLASMA.IMG` this appears to match the final release UI.|`SHOCK.COL`|
|`SAVE.IMG`|"Save Game" screen - game slot listing.|`START.COL`|
|`SAVED.IMG`|Game save success confirmation.|`START.COL`|
|`SAVED2.IMG`|Game save failure message.|`START.COL`|
|`START.IMG`|Main title screen.|`START.COL`|
|`TACTBAK.IMG`|Circuit board background image for "Tactical" screen.|`BRIEF.COL`|
|`TXTSCRN1.IMG`|Bethesda logo. Seems dark?|`TXTSCRN1.COL`|
|`TXTSCRN2.IMG`|Screen capture of H/K fighter flying back, still image from the game's intro movie. Also dark.|`TXTSCRN2.COL`|
|`TXTSCRN3.IMG`|Same image as `TXTSCRN2.IMG`, but with expository text overlaid. These intro screens are from the game's demo. Also dark.|`TXTSCRN2.COL`|
|`TXTSCRN4.IMG`|Same image as `TXTSCRN2.IMG`, but with a feature list for the full version of the game. Also dark.|`TXTSCRN2.COL`|
|`TXTSCRN5.IMG`|Nuclear explosion still from the game's intro movie, with phone number and website for purchasing full version of the game. Also dark.|`TXTSCRN5.COL`|
|`WEAPON00.CFA`|||
|`WEAPON00.IMG`|Weapon inventory image for the lead pipe.|`SHOCK.COL`|
|`WEAPON01.CFA`|||
|`WEAPON01.IMG`|Weapon inventory image for the Uzi 9mm.|`SHOCK.COL`|
|`WEAPON02.CFA`|||
|`WEAPON02.IMG`|Weapon inventory image for the assault rifle.|`SHOCK.COL`|
|`WEAPON03.CFA`|||
|`WEAPON03.IMG`|Weapon inventory image for the machine gun.|`SHOCK.COL`|
|`WEAPON04.CFA`|||
|`WEAPON04.IMG`|Weapon inventory image for the shotgun.|`SHOCK.COL`|
|`WEAPON05.CFA`|||
|`WEAPON05.IMG`|Weapon inventory image for the grenade launcher. It's definitely not a pulse rifle, no sir.|`SHOCK.COL`|
|`WEAPON06.CFA`|||
|`WEAPON06.IMG`|Weapon inventory image for the rocket launcher.|`SHOCK.COL`|
|`WEAPON07.CFA`|||
|`WEAPON07.IMG`|Weapon inventory image for the laser rifle.|`SHOCK.COL`|
|`WEAPON08.CFA`|||
|`WEAPON08.IMG`|Weapon inventory image for the laser cannon.|`SHOCK.COL`|
|`WEAPON09.CFA`|||
|`WEAPON09.IMG`|Weapon inventory image for the plasma pistol.|`SHOCK.COL`|
|`WEAPON10.CFA`|||
|`WEAPON10.IMG`|Weapon inventory image for the plasma rifle.|`SHOCK.COL`|
|`WEAPON11.CFA`|||
|`WEAPON11.IMG`|Weapon inventory image for the plasma cannon.|`SHOCK.COL`|
|`WEAPON12.CFA`|||
|`WEAPON12.IMG`|Weapon inventory image for the Uzi 9mm, but red? "Not present" image, perhaps. Don't recognise it from the final game.|`SHOCK.COL`|
|`WEAPON13.IMG`|Weapon inventory image for the pipe bomb.|`SHOCK.COL`|
|`WEAPON14.IMG`|Weapon inventory image for the molotov cocktail.|`SHOCK.COL`|
|`WEAPON15.IMG`|Weapon inventory image for the fragmentation grenade.|`SHOCK.COL`|
|`WEAPON16.IMG`|Weapon inventory image for an unknown grenade type. Not used in the final game.|`SHOCK.COL`|
|`WEAPON17.IMG`|Weapon inventory image for the canister bomb.|`SHOCK.COL`|
|`WEAPON18.IMG`|Weapon inventory image for the satchel charge.|`SHOCK.COL`|
|`WEAPON19.IMG`|Weapon inventory image for the vehicle-mounted plasma cannon.|`SHOCK.COL`|
|`WEAPON20.IMG`|Identical to `WEAPON19.IMG`.|`SHOCK.COL`|
|`WEAPON21.IMG`|Weapon inventory image for the vehicle-mounted rocket launcher.|`SHOCK.COL`|
|`WEAPON22.IMG`|Identical to `WEAPON21.IMG`.|`SHOCK.COL`|
|`WEAPON23.IMG`|Weapon inventory image for the vehicle-mounted laser cannon.|`SHOCK.COL`|
|`WEAPON24.IMG`|Identical to `WEAPON21.IMG`.|`SHOCK.COL`|
|`WELLDONE.IMG`|In-game mission success text, "Well Done, Soldier!" in the English version.|`SHOCK.COL`|