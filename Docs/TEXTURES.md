# Future Shock Texture Files

Related textures are stored in individual files named `TEXTURE.xxx` where `xxx` is a simple numerical extension. These
files seem to match the format described in [Daggerfall Unity](https://en.uesp.net/wiki/Daggerfall_Mod:Image_formats/Texture).

Seems like all of these files use `RUN.COL` for their palette, but I'm encountering a weird bug where I need to load
them with `SHOCK.COL` first and then switch back to `RUN.COL` to get it working properly. Guessing that there's an
issue with Future Shock's `*.COL` files being of two lengths (with/without header), compared to Daggerfall dividing
them into `*.COL` and `*.PAL`.

Descriptions/uages listed here are off the top of my head for now, need to go texture-hunting in-game to confirm some.

This file is incomplete and not fully verified, as I need to fix some bugs and get previews working for animated
textures before I can fully flesh it out.

|Filename|Number of Textures|Contents|
|---|---|---|
|TEXTURE.000|128|Spectrum of solid colours.|
|TEXTURE.001|128|Solid colours, particularly many shades of primary colours.|
|TEXTURE.013|12|Internal SkyNet base greebling.|
|TEXTURE.025|0|**Invalid file, can't be loaded.** Blocked it in code (DFU does something similar)|
|TEXTURE.120|5|Stone wall textures.|
|TEXTURE.200|16|Side views of weapons, used in the HUD.|
|TEXTURE.201|11|More side views of weapons. Also some "Duel Racer" textures? Left over from Xcar?|
|TEXTURE.202|21|Corpses, including the named NPC target in Mission 15.|
|TEXTURE.203|47|Bones/skeletons.|
|TEXTURE.204|31|Barrels, hanging lights and switches, light poles, spade and pick, T-800 vat.|
|TEXTURE.205|18|Dancers?? Dancing NPCs in suits, children's coin rides, stone columns. Probably unused.|
|TEXTURE.206|19|Clutter: Barrels, discarded bottles, stone debris.|
|TEXTURE.207|14|Clutter: Cans, papers, damaged pram.|
|TEXTURE.208|10|Clutter: Signs, dead tree, hydrant, burning barrel/tyres.|
|TEXTURE.209|14|Poles. Death camp fence posts, steel pipes, telegraph poles.|
|TEXTURE.210|15|Clutter: Plastic bags? Piles of trash bags?|
|TEXTURE.211|16|Clutter: Bags, stone and metal debris.|
|TEXTURE.212|10|Clutter: Crates, broken TV, flipped chair, broken lamp, ceiling lights.|
|TEXTURE.213|14|Clutter: Metal debris, tree fragments/stumps, tyre pile, garbage can/lid.|
|TEXTURE.214|15|Pickups: Armour, medpacks, batteries, some unused items like a flashlight.|
|TEXTURE.215|??|**Not loading correctly, but seems to have content. Opening it screws up the browser. Needs investigating.**|
|TEXTURE.216|19|Fires. **Not all records appear to be loading correctly - some empty?**|
|TEXTURE.217|??|**Not loading correctly, but seems to have content. Opening it screws up the browser. Needs investigating.**|
|TEXTURE.218|4|Larger fires.|
|TEXTURE.219|1|Small explosion.|
|TEXTURE.220|1|Large explosion.|
|TEXTURE.221|10|T-Rex textures.|
|TEXTURE.222|12|Switches/valves.|
|TEXTURE.223|8|Yellow wheeled vehicle. Forklift, I think.|
|TEXTURE.224|5|Internal base wall details.|
|TEXTURE.225|3|Wall grates.|
|TEXTURE.226|11|Wall greebling - wiring, pipes, etc|
|TEXTURE.227|6|"Bad Dog Beer" textures. Unused? Can't seem to find a correct palette.|
|TEXTURE.228|9|Wall paneling? Includes what I think is a T-800 bioscan monitor. Pod textures? **Usage needs confirming.**|
|TEXTURE.229|18|Base wall/ceiling panels, inc. lights.|
|TEXTURE.230|8|Base wall/ceiling panels, inc. lights.|
|TEXTURE.231|11|Grates, sliding door, battered panels.|
|TEXTURE.232|8|Inset displays, cable bundles, greebling.|
|TEXTURE.233|3|Electrical wiring on brick wall.|
|TEXTURE.234|4|Dirty wall panels.|
|TEXTURE.235|12|Tiki Grand Hotel - doors, elevators, walls, windows, etc|
|TEXTURE.236|9|Human building internals - doors, walls, oven.|
|TEXTURE.237|4|Tendrils? **Need to confirm once animations are working**|
|TEXTURE.238|7|Fires|
|TEXTURE.239|4|Assorted wall panels.|
|TEXTURE.240|8|Church exterior textures.|
|TEXTURE.241|12|Railcar exteriors? Don't recall any railcars in game, may be unused.|
|TEXTURE.242|7|Cactus/scrub, alive & dead, animal bones. Unused?|
|TEXTURE.243|2|Wall paneling, large electrical boxes and an interface.|
|TEXTURE.288|84|Goliath textures, as well as many smaller ones I don't recognise. Don't seem to fit the same palette, include a skull with glowing eyes and an old man's face. Daggerfall?|
|TEXTURE.289|102|HK aircraft.|
|TEXTURE.298|10|In-game "LS" markers, 00-09, not sure what "LS" is yet.|
|TEXTURE.299|100|In-game markers, 000-099, mostly generic but including more specific ones such as flight ceilings, radiated area boundaries, etc.|
|TEXTURE.300|58|Blasted sand/dirt textures.|
|TEXTURE.302|??|**Not loading correctly, but seems to have content. Opening it screws up the browser. Needs investigating.**|
|TEXTURE.303|7|Office/store greebles including smashed monitor.|
|TEXTURE.354|9|Appears to be identical to the first half of `TEXTURE.205`, unused.|
|TEXTURE.355|9|Appears to be identical to the second half of `TEXTURE.205`, unused.||
|TEXTURE.356|1|Explosion? Unsure, **Need to confirm once animations are working**|
|TEXTURE.357|||
|TEXTURE.358|||
|TEXTURE.359|||
|TEXTURE.360|||
|TEXTURE.361|||
|TEXTURE.362|||
|TEXTURE.363|||
|TEXTURE.364|||
|TEXTURE.365|||
|TEXTURE.366|||
|TEXTURE.367|||
|TEXTURE.368|||
|TEXTURE.369|||
|TEXTURE.370|||
|TEXTURE.371|||
|TEXTURE.372|||
|TEXTURE.373|||
|TEXTURE.374|||
|TEXTURE.375|||
|TEXTURE.376|||
|TEXTURE.377|||
|TEXTURE.378|||
|TEXTURE.379|||
|TEXTURE.380|||
|TEXTURE.381|||
|TEXTURE.382|||
|TEXTURE.383|||
|TEXTURE.384|||
|TEXTURE.385|||
|TEXTURE.386|||
|TEXTURE.387|||
|TEXTURE.388|||
|TEXTURE.389|||
|TEXTURE.390|||
|TEXTURE.391|||
|TEXTURE.392|||
|TEXTURE.393|||
|TEXTURE.394|||
|TEXTURE.395|||
|TEXTURE.396|||
|TEXTURE.397|||
|TEXTURE.398|||
|TEXTURE.399|||
|TEXTURE.400|||
|TEXTURE.401|||
|TEXTURE.402|||
|TEXTURE.403|||
|TEXTURE.404|||
|TEXTURE.405|||
|TEXTURE.406|||
|TEXTURE.407|||
|TEXTURE.408|||
|TEXTURE.409|||
|TEXTURE.410|||
|TEXTURE.411|||
|TEXTURE.412|||
|TEXTURE.413|||
|TEXTURE.414|||
|TEXTURE.415|||
|TEXTURE.416|||
|TEXTURE.417|||
|TEXTURE.418|||
|TEXTURE.419|||
|TEXTURE.420|||
|TEXTURE.421|||
|TEXTURE.422|||
|TEXTURE.423|||
|TEXTURE.424|||
|TEXTURE.425|||
|TEXTURE.426|||
|TEXTURE.427|||
|TEXTURE.428|||
|TEXTURE.429|||
|TEXTURE.430|||
|TEXTURE.431|||
|TEXTURE.432|||
|TEXTURE.433|||
|TEXTURE.434|||
|TEXTURE.435|||
|TEXTURE.436|||
|TEXTURE.437|||
|TEXTURE.438|||
|TEXTURE.439|||
|TEXTURE.440|||
|TEXTURE.441|||
|TEXTURE.442|||
|TEXTURE.443|||
|TEXTURE.444|||
|TEXTURE.445|||
|TEXTURE.446|||
|TEXTURE.447|||
|TEXTURE.448|||
|TEXTURE.449|||
|TEXTURE.450|||
|TEXTURE.451|||
|TEXTURE.452|||
|TEXTURE.453|||
|TEXTURE.454|||
|TEXTURE.455|||
|TEXTURE.456|||
|TEXTURE.457|||
|TEXTURE.458|||
|TEXTURE.459|||
|TEXTURE.460|||
|TEXTURE.461|||
|TEXTURE.462|||
|TEXTURE.463|||
|TEXTURE.464|||
|TEXTURE.465|||
|TEXTURE.466|||
|TEXTURE.467|||
|TEXTURE.468|||
|TEXTURE.469|||
|TEXTURE.470|||
|TEXTURE.471|||
|TEXTURE.472|||
|TEXTURE.474|||
|TEXTURE.475|||
|TEXTURE.476|||
|TEXTURE.477|||
|TEXTURE.478|||
|TEXTURE.479|||
|TEXTURE.480|||
|TEXTURE.481|||
|TEXTURE.482|||
|TEXTURE.483|||
|TEXTURE.484|||
|TEXTURE.485|||
|TEXTURE.486|||
|TEXTURE.487|||
|TEXTURE.488|||
|TEXTURE.489|||
|TEXTURE.490|||
|TEXTURE.491|||
|TEXTURE.492|||
|TEXTURE.493|||
|TEXTURE.494|||
|TEXTURE.495|||
|TEXTURE.496|||
|TEXTURE.497|||
|TEXTURE.498|||
|TEXTURE.499|||
|TEXTURE.500|||
|TEXTURE.501|||
|TEXTURE.502|||
|TEXTURE.503|||
|TEXTURE.504|||
|TEXTURE.505|||
|TEXTURE.506|||
|TEXTURE.507|||
|TEXTURE.508|||
|TEXTURE.509|||
|TEXTURE.510|||
|TEXTURE.511|||