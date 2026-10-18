Name: Quiet Traps
Summary: Mutes the sound a trap keeps playing once it has caught something. Every other sound plays as normal. 静音陷阱捕获猎物后持续发出的声音，其他声音照常播放。

Description (BBCode):
When a trap catches prey, it keeps making noise until you collect the catch. With a few traps set, that adds up. Quiet Traps silences that sound, and only that sound: setting, moving, checking and removing traps still sound the same.

[size=5]Main features[/size]
[list]
[*][b]Catch sounds muted[/b]: every trap catch sound, including the Iron Animal Trap's rattle, is silenced by default.[/*]
[*][b]Pick which ones[/b]: each catch sound has its own setting, so you can keep the ones you want to hear.[/*]
[*][b]Nothing else goes quiet[/b]: the mod mutes four exact sounds and nothing else.[/*]
[/list]
The game has five traps but only three catch sounds. The Live Capture Cage and the Simple Bird Net each have their own. The Simple Mousetrap, the Iron Animal Trap and the Auto Trap Cage share one, so they are turned on and off together. The Iron Animal Trap also rattles while it holds a catch; that rattle goes with the same setting.

Quiet Traps only changes what you hear. It adds nothing to your save and does not edit game files. You can remove it at any time.

Works in English and Chinese.

[size=5]Known issues[/size]
None known. If a trap sound still plays, turn on [font=Courier New]LogSounds[/font] in the config, stay near the trap until you hear it, then look in [font=Courier New]BepInEx\quiet-traps.log[/font] for the sound that started at that time.

[size=5]Requirements[/size]
[list]
[*]Survival Log 1.1.18902. A game update can break it until the mod is updated.[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/12]BepInEx for Survival Log[/url]. Install it first.[/*]
[/list]

[size=5]Installation[/size]
[list=1]
[*]Install [url=https://www.nexusmods.com/survivallog/mods/12]BepInEx[/url] [i](if no other mods were installed before, start the game once so BepInEx finishes setup, then quit)[/i].[/*]
[*]Download the zip from the Files tab.[/*]
[*]Extract it into the game folder (the folder with [font=Courier New]SurvivalLog.exe[/font]). The zip holds a BepInEx folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
[code]C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\QuietTraps.dll[/code][/*]
[/list]

[size=5]Uninstall[/size]
Delete [font=Courier New]QuietTraps.dll[/font] from the [font=Courier New]BepInEx\plugins[/font] folder.

[size=5]Configuration[/size]
To change settings, edit [font=Courier New]BepInEx\config\com.dev.survivallog.quiettraps.cfg[/font] while the game is closed (see [url=https://github.com/AnExiledDev/SurvivalLog-QuietTraps/blob/main/CONFIG.md]CONFIG.md[/url] for every setting). Changes apply at the next game start.

[size=5]Feedback[/size]
Found a bug or have an idea? Come say hi on the [url=https://discord.gg/trTsyxUFqg]Core Directive Discord[/url], or leave a comment here. For bugs, please include [font=Courier New]BepInEx\LogOutput.log[/font] and [font=Courier New]BepInEx\quiet-traps.log[/font].

[size=5]Source and license[/size]
Open source under MIT: [url=https://github.com/AnExiledDev/SurvivalLog-QuietTraps]github.com/AnExiledDev/SurvivalLog-QuietTraps[/url]

[line]
[size=5]简体中文[/size]

陷阱捕获猎物后，会一直发出声音，直到你把猎物取走。放了几个陷阱，声音就叠在一起了。Quiet Traps 只静音这一种声音：放置、移动、查看和拆除陷阱的声音都和原来一样。

[size=5]主要功能[/size]
[list]
[*][b]捕获声音静音[/b]：所有陷阱的捕获声音默认都会被静音，包括「铁制捕兽夹」的晃动声。[/*]
[*][b]自行选择[/b]：每种捕获声音都有单独的设置，想听的可以保留。[/*]
[*][b]其他声音照常[/b]：模组只静音四个指定的声音，其他一概不动。[/*]
[/list]
游戏里有五种陷阱，但只有三种捕获声音。「活捉笼」和「简易捕鸟网」各有自己的声音。「简易捕鼠夹」、「铁制捕兽夹」和「自动诱捕笼」共用一个声音，所以它们一起开关。「铁制捕兽夹」捕获猎物后还会发出晃动声，这个声音也归同一项设置。

Quiet Traps 只改变你听到的声音，不会往存档里添加任何内容，也不会修改游戏文件，随时可以移除。

支持英文和中文。

[size=5]已知问题[/size]
目前没有。如果仍能听到某个陷阱声音，在配置中打开 [font=Courier New]LogSounds[/font]，待在陷阱旁直到听到这个声音，然后在 [font=Courier New]BepInEx\quiet-traps.log[/font] 中查找那个时间开始播放的声音。

[size=5]前置要求[/size]
[list]
[*]Survival Log 1.1.18902。游戏更新后模组可能暂时失效，直到模组更新为止。[/*]
[*][url=https://www.nexusmods.com/survivallog/mods/12]BepInEx for Survival Log[/url]，请先安装。[/*]
[/list]

[size=5]安装[/size]
[list=1]
[*]安装 [url=https://www.nexusmods.com/survivallog/mods/12]BepInEx[/url] [i]（如果之前没有装过其他模组，先启动一次游戏，让 BepInEx 完成初始化，然后退出）[/i]。[/*]
[*]在「文件」页下载压缩包。[/*]
[*]把压缩包解压到游戏目录（[font=Courier New]SurvivalLog.exe[/font] 所在的文件夹）。压缩包里带有 BepInEx 文件夹，DLL 会自动放到正确位置。如果提示合并文件夹，选择合并。解压后应为：
[code]C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\QuietTraps.dll[/code][/*]
[/list]

[size=5]卸载[/size]
从 [font=Courier New]BepInEx\plugins[/font] 文件夹中删除 [font=Courier New]QuietTraps.dll[/font]。

[size=5]配置[/size]
如需修改设置，请在游戏关闭时编辑 [font=Courier New]BepInEx\config\com.dev.survivallog.quiettraps.cfg[/font]。配置文件中每项设置都有中文说明，完整说明见 [url=https://github.com/AnExiledDev/SurvivalLog-QuietTraps/blob/main/CONFIG.md]CONFIG.md[/url]（英文）。修改会在下次启动游戏时生效。

[size=5]反馈[/size]
发现 bug 或有好点子？欢迎来 [url=https://discord.gg/trTsyxUFqg]Core Directive Discord[/url] 打个招呼，或在这里留言。报告 bug 时，请附上 [font=Courier New]BepInEx\LogOutput.log[/font] 和 [font=Courier New]BepInEx\quiet-traps.log[/font]。

[size=5]源代码与许可[/size]
基于 MIT 许可开源：[url=https://github.com/AnExiledDev/SurvivalLog-QuietTraps]github.com/AnExiledDev/SurvivalLog-QuietTraps[/url]
