# Quiet Traps

Mutes the sound a trap makes once it has caught something. Every other sound plays as normal.

When a trap in Survival Log catches prey, it keeps playing a sound until you collect the catch. With a few traps set, that adds up. Quiet Traps silences that sound, and only that sound: setting, moving, checking and removing traps still sound the same.

## Features

- **Catch sounds muted**: every trap catch sound, including the Iron Animal Trap's rattle, is silenced by default.
- **Pick which ones**: each catch sound has its own setting, so you can keep the ones you want to hear.
- **Nothing else goes quiet**: the mod mutes four exact sounds and nothing else.
- **Find a missed sound**: turn on `LogSounds` and the mod writes the sounds it sees to a log file.

The game has five traps but only three catch sounds. The Live Capture Cage and the Simple Bird Net each have their own. The Simple Mousetrap, the Iron Animal Trap and the Auto Trap Cage share one, so they are turned on and off together. The Iron Animal Trap also rattles while it holds a catch; that rattle goes with the same setting.

Quiet Traps only changes what you hear. It adds nothing to your save and does not edit game files. You can remove it at any time.

## Requirements

- Survival Log 1.1.18902. Built for this version; a game update can break it until the mod is updated.
- The [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12), the BepInEx 6 (IL2CPP) build for the game.

## Install

1. Install the [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12) (if no other mods were installed before, start the game once so BepInEx finishes setup, then quit).
2. Download `QuietTraps-x.y.z.zip` from the [latest release](https://github.com/AnExiledDev/SurvivalLog-QuietTraps/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/55).
3. Extract the zip into the game folder (the folder with `SurvivalLog.exe`). It holds a `BepInEx` folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
   `C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\QuietTraps.dll`

## Uninstall

Delete `QuietTraps.dll` from the `BepInEx\plugins` folder. For a clean slate, also delete `BepInEx\config\com.dev.survivallog.quiettraps.cfg` and, if it exists, `BepInEx\quiet-traps.log`.

## Configuration

Settings live in `BepInEx\config\com.dev.survivallog.quiettraps.cfg`, created the first time the game starts with the mod. Edit it while the game is closed; changes apply at the next game start. See [CONFIG.md](CONFIG.md) for every setting.

## Troubleshooting

Built against BepInEx `6.0.0-be.788`.

Check `BepInEx\LogOutput.log` for `===== Quiet Traps 0.1.3 started`, followed by lines starting with `hooked`. A line starting with `could not hook` means part of the mod does not work on this game version.

## Known issues

None known. If a trap sound still plays, set `LogSounds` to `true` and start the game. Then look in `BepInEx\quiet-traps.log` for a sound with `xianjing` in its name that is not marked `(muted)`, and send that line on Discord.

## Feedback and bug reports

Found a bug, have an idea, or something feels off? Come say so on the Core Directive Discord: https://discord.gg/trTsyxUFqg . You can also [open an issue](https://github.com/AnExiledDev/SurvivalLog-QuietTraps/issues). When reporting a bug, attach `BepInEx\LogOutput.log` and, if you turned on `LogSounds`, `BepInEx\quiet-traps.log`.

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against BepInEx and the game's IL2CPP interop assemblies, so a game install with BepInEx set up and started once is required. Those assemblies come from the game and are not part of this repo. The .NET 8 SDK is required.

```
dotnet build src/QuietTraps -c Release -p:GameDir="C:\path\to\Survival Log"
```

`GameDir` is the game folder. The default in the project file is a local path you will almost certainly need to override. The output DLL is at `src\QuietTraps\bin\Release\QuietTraps.dll`.

The unit tests cover the pure muting rules and need nothing from the game:

```
dotnet test tests/QuietTraps.Tests
```

## License

MIT. Copyright (c) 2026 AnExiledDev. See [LICENSE](LICENSE).
