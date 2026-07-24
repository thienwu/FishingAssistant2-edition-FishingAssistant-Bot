# 🎣 Fishing Assistant Bot 

![FishingAssistant2](Image/FishingAssistant2.gif)

A friendly and smart fishing companion for `Stardew Valley`! Based on the original Fishing Assistant 2, this modified version brings a helpful **Auto-Navigation Bot** to your farm. 

Not only does it help you catch fish automatically, but it can also walk to the fishing spot for you, fish all day, and safely find its way back to your bed before you pass out at night!

---

## 🌟 What's New?

### 🤖 Smart Navigation
The bot has spatial awareness and can navigate around trees, buildings, and NPCs. It easily travels across multiple maps (e.g., from your FarmHouse to the Beach).

### 🗺️ Route Recording System
You can easily teach the bot how to get to your favorite fishing spots!
- Stand next to your bed and press `F9` to start recording.
- Walk to your desired fishing spot.
- Press `F9` again at the water's edge to save the route as `AutoFishing`.

### 🛏️ Auto-Sleep & Next Day Resume
No more passing out at 2:00 AM!
- When the game reaches your configured "Pause Time" (e.g., 1:00 AM), the bot will automatically pack up its fishing rod.
- It will safely walk back home following your route, open the door, and get straight into bed.
- **Bonus:** The next morning, it will automatically wake up and run back to the fishing spot to continue its work!

### 🛠️ Visual Grid Editor
Press `F10` to open the NavTool menu. You can toggle the visual grid to see exactly how the bot perceives walkable tiles and blocked obstacles.

---

## 🐟 Core Fishing Features

All the great features from the original Fishing Assistant 2 are still here:

- **Hands-Free Fishing:** Auto-cast, auto-hook, and auto-play the fishing minigame.
- **Smart Inventory:** Automatically loots treasures. If your inventory is full, it can drop, discard, or auto-trash junk items.
- **Auto-Eat:** Automatically detects low energy and eats food to keep you going.
- **Bait & Tackle Management:** Automatically attaches your preferred bait and tackles.
- **Fish Preview:** Displays a UI preview of the fish or treasure currently on the hook before you even catch it.
- **Customization:** Instantly bite, skip minigames, force max-size fish, modify fishing difficulty, or use infinite bait and tackles.

---

## ⚙️ How to Setup & Use

1. **Install** the mod via SMAPI. We highly recommend installing [Generic Mod Config Menu (GMCM)](https://www.nexusmods.com/stardewvalley/mods/5098) for easy in-game configuration.
2. **Start the Game** and wake up in your FarmHouse or Cabin.
3. **Record a Route:**
   - Stand right next to your bed.
   - Press `F9` to start recording.
   - Walk normally to your fishing spot (e.g., the Beach).
   - Once at the water, press `F9` to stop recording.
4. **Start Fishing:** Press `F5` to toggle automation. The bot will now handle everything!
5. **Auto-Sleep:** Make sure `AutoPauseFishing` is set to `WarnAndPause` in the config. When the time hits the limit, the bot will run home and sleep.

### 🎮 Default Key Bindings
- `F5`: Toggle Fishing Automation (Start/Stop the bot)
- `F6`: Toggle Catch/Ignore Treasure
- `F9`: Toggle Route Recording (Start/Stop)
- `F10`: Open the Navigation Settings Menu (View Grid, Edit Routes)

---

## 📜 Full Configuration Options

You can edit these settings via the `config.json` file or in-game using GMCM.

### General & Automation
* `EnableAutomationButton` / `CatchTreasureButton` / `OpenConfigMenuButton`: Customize your hotkeys.
* `ModStatusPosition`: Position of the mod's HUD status (`Left` or `Right`).
* `AutoCastFishingRod` / `AutoHookFish` / `AutoPlayMiniGame`: Toggles for core fishing automation.
* `AutoClosePopup`: Automatically closes the "Fish Caught" popup.

### Inventory & Trash Management
* `AutoLootTreasure`: Automatically collects items from treasure chests.
* `ActionIfInventoryFull`: Choose what happens when full (`Stop`, `Drop`, `Discard`).
* `AutoTrashJunk`: Automatically trashes cheap junk items when you need space.
* `JunkHighestPrice`: Items worth less than or equal to this value are considered junk.
* `AllowTrashFish`: Allows the bot to trash cheap fish.
* `JunkIgnoreList`: List of item IDs that should NEVER be trashed.

### Night time & Energy Management
* `AutoPauseFishing`: Behavior when it gets late (`Off`, `WarnOnly`, `WarnAndPause`). *Note: Set to WarnAndPause to enable auto-return to bed!*
* `PauseFishingTime`: The time (e.g., `24` for 12:00 AM) the bot stops fishing.
* `AutoEatFood`: Automatically eat food when energy is low.
* `EnergyPercentToEat`: The energy percentage threshold to trigger eating.
* `AllowEatingFish`: Allows the bot to eat raw fish if no other food is available.

### Bait, Tackle & Rods
* `AutoAttachBait` / `AutoAttachTackles`: Automatically equips bait/tackle.
* `PreferBait` / `PreferTackle` / `PreferAdvIridiumTackle`: Set specific items to equip.
* `SpawnBaitIfDontHave` / `SpawnTackleIfDontHave`: Cheats to spawn items if you run out.
* `InfiniteBait` / `InfiniteTackle`: Prevents bait and tackles from being consumed.
* `StartWithFishingRod`: Automatically gives you a specific fishing rod on Day 1.

### Cheats & Difficulty Customizers
* `SkipFishingMiniGame`: Instantly catch the fish without playing the minigame.
* `InstantFishBite`: Fish bite the moment the bobber hits the water.
* `PreferFishAmount` / `PreferFishQuality`: Force multiple fish or higher quality catches.
* `AlwaysPerfect` / `AlwaysMaxFishSize`: Forces perfect catches and maximum sizes.
* `FishDifficultyMultiplier` / `FishDifficultyAdditive`: Tweak the actual difficulty of the minigame.
* `InstantCatchTreasure` / `TreasureChance` / `GoldenTreasureChance`: Control treasure spawn rates.

### UI Previews
* `DisplayFishPreview` / `ShowFishName` / `ShowTreasure`: Toggles for the X-Ray preview UI.
* `ShowUncaughtFishSpecies`: Shows previews even for fish you've never caught before.
* `AlwaysShowLegendaryFish`: Highlights legendary fish on the preview.

### Auto-Enchantments
* `AddAutoHookEnchantment` / `AddEfficientEnchantment` / `AddMasterEnchantment` / `AddPreservingEnchantment`: Automatically applies these enchantments to your rod.

---

## 🙏 Credits
Special thanks to the original creators of Fishing Assistant 2 and the Stardew Valley modding community. This modified version builds upon their amazing work to add friendly auto-navigation features!
