using System.Collections.Generic;
using StardewModdingAPI;
using Microsoft.Xna.Framework; 
using ChibiKyu.StardewMods.FishingAssistant2.Frameworks;

namespace ChibiKyu.StardewMods.FishingAssistant2
{
    public class FishingSpot
    {
        public string MapName { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int FacingDirection { get; set; }
    }

    public class ModConfig
    {
        public string ActiveSpotName { get; set; } = "";
        public Dictionary<string, FishingSpot> SavedSpots { get; set; } = new Dictionary<string, FishingSpot>();
        public SButton EnableAutomationButton { get; set; } = SButton.F5;
        public SButton CatchTreasureButton { get; set; } = SButton.F6;
        public SButton OpenConfigMenuButton { get; set; } = SButton.None;
        public string ModStatusPosition { get; set; } = "Left";
        
        public bool AutoCastFishingRod { get; set; } = true;
        public bool AutoHookFish { get; set; } = true;
        public bool AutoPlayMiniGame { get; set; } = true;
        public bool AutoClosePopup { get; set; } = true;
        public bool AutoLootTreasure { get; set; } = true;
        public string ActionIfInventoryFull { get; set; } = "Stop";
        public bool AutoTrashJunk { get; set; } = false;
        public int JunkHighestPrice { get; set; } = 0;
        public bool AllowTrashFish { get; set; } = false;
        public List<string> JunkIgnoreList { get; set; } = new List<string>();
        
        public string AutoPauseFishing { get; set; } = "WarnAndPause";
        public int TimeToPause { get; set; } = 24;
        public int WarnCount { get; set; } = 1;
        public bool AutoEatFood { get; set; } = false;
        public int EnergyPercentToEat { get; set; } = 5;
        public bool AllowEatingFish { get; set; } = false;
        
        public bool AutoAttachBait { get; set; } = false;
        public string PreferredBait { get; set; } = "Any";
        public bool AutoAttachTackles { get; set; } = false;
        public string PreferredTackle { get; set; } = "Any";
        public string PreferredAdvIridiumTackle { get; set; } = "Any";
        
        public string StartWithFishingRod { get; set; } = "None";
        public int DefaultCastPower { get; set; } = 100;
        public float UnlockCastPowerTime { get; set; } = 1f;
        internal float ParsedUnlockCastPowerTime => UnlockCastPowerTime * 60f;
    }
}