# Quiet Traps

Mutes the sound a trap makes once it has caught something. Every other sound plays as normal.

When a trap in Survival Log catches prey, it keeps playing a sound until you collect the catch. With a few traps set, it gets loud. Quiet Traps mutes that sound. Setting, moving, checking and removing a trap still make their usual sounds.

## Features

- **Catch sounds muted**: by default, every trap's catch sound is muted, including the Iron Animal Trap's rattle.
- **One setting per sound**: each catch sound has its own setting, so you can keep the ones you want to hear.
- **Other sounds still play**: the mod only mutes the three catch sounds and the Iron Animal Trap's rattle.
- **Sound log**: if a trap sound still plays, turn on `LogSounds` and the mod lists the sounds that play in a log file.

The game has five traps but only three catch sounds. The Live Capture Cage and the Simple Bird Net each have their own sound. The Simple Mousetrap, the Iron Animal Trap and the Auto Trap Cage share one, so one setting mutes all three. The Iron Animal Trap also rattles while it holds a catch, and the same setting mutes the rattle.

Quiet Traps only changes what you hear. It adds nothing to your save and does not edit game files. You can remove it at any time.

## Requirements

- Survival Log 1.1.20719. Built for this version, so a game update can break it until the mod is updated.
- The [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12), the BepInEx 6 (IL2CPP) build for the game.
- [Exiled's Toolkit](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit) 0.19.0 or newer. Quiet Traps does not load without it. It also lets settings you change on the Mod Manager's Mods screen apply without a restart.
- Optional: [Mod Manager](https://www.nexusmods.com/survivallog/mods/73). With it, you can change every setting in game on its Mods screen and see what each one does, without opening the config file.

## Install

1. Install the [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12) (if no other mods were installed before, start the game once so BepInEx finishes setup, then quit).
2. Install Exiled's Toolkit. Download `ExiledToolkit-x.y.z.zip` from [its latest release](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/61), and extract it into the game folder (the folder with `SurvivalLog.exe`).
3. Download `QuietTraps-x.y.z.zip` from the [latest release](https://github.com/AnExiledDev/SurvivalLog-QuietTraps/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/55).
4. Extract the zip into the game folder (the folder with `SurvivalLog.exe`). It holds a `BepInEx` folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
   `C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\QuietTraps.dll`

## Uninstall

Delete `QuietTraps.dll` from the `BepInEx\plugins` folder. To remove its settings and log too, also delete `BepInEx\config\com.dev.survivallog.quiettraps.cfg` and, if it exists, `BepInEx\quiet-traps.log`.

## Configuration

Settings live in `BepInEx\config\com.dev.survivallog.quiettraps.cfg`, created the first time the game starts with the mod. Change them in game on the Mod Manager's Mods screen, where they take effect at once, or edit the file while the game is closed. If a trap already holds a catch when you change a setting, its sound only changes after you load a save. See [CONFIG.md](CONFIG.md) for every setting.

## Troubleshooting

Built against BepInEx `6.0.0-be.788`.

Check `BepInEx\LogOutput.log` for `===== Quiet Traps 0.2.3 started`, followed by lines starting with `hooked`. A line starting with `could not hook` means part of the mod does not work on this game version.

## Known issues

None known. If a trap sound still plays, set `LogSounds` to `true` and start the game. Then look in `BepInEx\quiet-traps.log` for a sound with `xianjing` in its name that is not marked `(muted)`, and send that line on Discord.

## Feedback and bug reports

Found a bug or have an idea? Come say so on the Core Directive Discord: https://discord.gg/trTsyxUFqg . You can also post in the Posts tab of the mod's Nexus Mods page. When reporting a bug, attach `BepInEx\LogOutput.log` and, if you turned on `LogSounds`, `BepInEx\quiet-traps.log`.

To reach me directly, message `exiled.dev` on Discord. You can find me in the [official Survival Log Discord](https://discord.gg/MZGg4gQAUe). I'm also on X as [@AnExiledDev](https://x.com/AnExiledDev).

A Survival Log wiki with detailed documentation for all of my mods is coming soon.

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against BepInEx, the game's IL2CPP interop assemblies and Exiled's Toolkit, so a game install with BepInEx set up and started once is required. Those assemblies are not part of this repo. The .NET 8 SDK is required.

```
dotnet build src/QuietTraps -c Release -p:GameDir="C:\path\to\Survival Log" -p:ToolkitDll="C:\path\to\ExiledToolkit.dll"
```

`GameDir` is the game folder and `ToolkitDll` is a built `ExiledToolkit.dll`. The defaults in the project file are local paths you will almost certainly need to override. The output DLL is at `src\QuietTraps\bin\Release\QuietTraps.dll`.

The unit tests cover which sounds get muted and logged, and need nothing from the game:

```
dotnet test tests/QuietTraps.Tests
```

## License

MIT. Copyright (c) 2026 AnExiledDev. See [LICENSE](LICENSE).
