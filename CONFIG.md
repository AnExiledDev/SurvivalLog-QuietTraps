# Quiet Traps settings

Settings live in `BepInEx\config\com.dev.survivallog.quiettraps.cfg`, created the first time the game starts with the mod. Edit the file while the game is closed; changes apply at the next game start. Delete the file to get the defaults back.

## Mute

Each setting mutes the sound a trap plays while it holds a catch. Set one to `false` to hear that sound again.

| Setting | Default | Traps |
| --- | --- | --- |
| `LiveCaptureCage` | `true` | Live Capture Cage |
| `SimpleBirdNet` | `true` | Simple Bird Net |
| `OtherTraps` | `true` | Simple Mousetrap, Iron Animal Trap and Auto Trap Cage. The game gives all three the same sound, so they share one setting. It also mutes the Iron Animal Trap's rattle. |

## Diagnostics

| Setting | Default | What it does |
| --- | --- | --- |
| `LogSounds` | `false` | Writes the first three starts of each sound to `BepInEx\quiet-traps.log` with the time, marked `(muted)` when this mod silenced it. Turn it on to find a trap sound that still plays: look for a sound with `xianjing` in its name that is not marked `(muted)`. The file starts fresh at each game start. |
