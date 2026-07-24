# Fishing Assistant Bot (Fishing Assistant 2 - Bot Edition)

An advanced, fully autonomous Fishing Bot for `Stardew Valley`. Not only does it automatically catch fish, but it also features a powerful **A* Pathfinding and Route Recording System** that allows the bot to physically walk to fishing spots, fish all day, and automatically walk back to bed before you pass out!

![FishingAssistant2](Image/FishingAssistant2.gif)

## 🌟 New Bot Features

* **Autonomous Pathfinding (A* Navigation):** The bot can intelligently navigate around obstacles, trees, buildings, and NPCs across multiple maps to reach its destination.
* **Route Recording:** Simply press `F9` to start recording a route from your bed to your favorite fishing spot. Press `F9` again at the water to save the route.
* **Auto-Sleep & Next Day Resume:** When the time reaches the configured "Pause time" (e.g., 1:00 AM), the bot will pack up its fishing rod and automatically navigate back along the recorded route, open the door, and jump into bed. The next morning, it will wake up and run straight back to the fishing spot!
* **Smart Route Correction:** If the bot gets stuck, it will attempt to recalculate paths or gracefully stop. It even understands transitions between different house types (Cabin vs FarmHouse) and handles multi-map journeys effortlessly.
* **Visual Grid Editor:** Press `F10` to open the NavTool menu and toggle the grid view to see the walkable and blocked tiles as perceived by the bot.

## 🎣 Core Fishing Features

* **Auto Cast, Auto Hook, and Auto Minigame:** 100% hands-free fishing.
* **Inventory Management:** Automatically loot treasures, drop/discard items if inventory is full, and auto-trash predefined junk items.
* **Auto Eat:** Configurable to eat food when energy falls below a certain percentage.
* **Smart Bait & Tackle:** Automatically attach your preferred bait and tackle. Can even spawn them if you run out!
* **Fish & Treasure Preview:** Displays UI previews of what fish or treasure is on the hook before you even catch it.
* **Difficulty Customization:** Tweak fishing minigame difficulty, ensure perfect catches, or force max-size fish.

## ⚙️ Configuration & Key Bindings

The configuration file is automatically created in the mod's folder the first time you run the game. You can also install the **Generic Mod Config Menu (GMCM)** for easy in-game tweaking.

### 🎮 Hotkeys
* `F5`: Toggle Fishing Automation (Start/Stop the bot)
* `F6`: Toggle Catch or Ignore Treasure
* `F9`: Toggle Route Recording (Start recording from Bed, walk to water, stop recording)
* `F10`: Open the Navigation Settings Menu (View Grid, Edit Routes)

### 🗺️ How to setup the Auto-Fishing Bot
1. **Start Recording:** Stand next to your bed inside your FarmHouse or Cabin and press `F9`.
2. **Walk the Route:** Walk normally to your desired fishing spot (e.g., the Beach or Mountain lake).
3. **Finish Recording:** Once you reach the water's edge, press `F9` again. The route will be saved as `AutoFishing`.
4. **Relax:** The bot will now automatically fish! When it's late at night (configured in GMCM), it will stop fishing, walk back to bed, sleep, and automatically restart the route the next morning.

## 🙏 Thank you
Enjoy your endless fishing empire!
