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
