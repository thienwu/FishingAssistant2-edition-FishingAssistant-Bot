/*using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using System;

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    internal class SBobberBar(BobberBar bobberBar)
    {
        internal BobberBar Instance { get; set; } = bobberBar;

        internal bool AlreadyCaughtFish(int minCaught = 1)
        {
            if (Instance == null) return false;
            ItemMetadata? metadata = ItemRegistry.GetMetadata(Instance.whichFish);
            Game1.player.fishCaught.TryGetValue(metadata.QualifiedItemId, out int[] numArray);
            return numArray != null && numArray.Length > 0 && numArray[0] >= minCaught;
        }
    }
}*/