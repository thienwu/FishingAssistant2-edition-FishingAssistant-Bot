using ChibiKyu.StardewMods.Common;
using FishingAssistant2;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;
using StardewValley.Enchantments; // Thư viện được thêm vào để nhận diện Bùa Auto-Hook gốc
using Object = StardewValley.Object;
using System;
using System.Collections.Generic;

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    internal class SFishingRod(FishingRod instance, Func<ModConfig> modConfig)
    {
        internal float SmartCastPower;
        internal bool SmartCastPowerSaved;
        internal float UnlockCastPowerTimer = modConfig().ParsedUnlockCastPowerTime;
        internal FishingRod Instance { get; set; } = instance;

        internal void ResetSmartCastPower()
        {
            UnlockCastPowerTimer = modConfig().ParsedUnlockCastPowerTime;
            SmartCastPower = 0;
            SmartCastPowerSaved = false;
        }

        internal void AutoAttachBait()
        {
            if (!IsRodNotInUse() || Game1.isFestival()) return;
            IList<Item> items = Game1.player.Items;
            if (Instance.UpgradeLevel < 2) return;

            if (Instance.attachments[0] != null && Instance.attachments[0].Stack != Instance.attachments[0].maximumStackSize())
            {
                foreach (Item item in items)
                {
                    if (item?.Category != Object.baitCategory || !item.Name.Equals(Instance.attachments[0].Name)) continue;
                    int stackAdd = Math.Min(Instance.attachments[0].getRemainingStackSpace(), item.Stack);
                    Instance.attachments[0].Stack += stackAdd;
                    item.Stack -= stackAdd;
                    if (item.Stack == 0) Game1.player.removeItemFromInventory(item);
                    CommonHelper.PushWarning(Instance, I18n.HudMessage_AutoAttach(), item.DisplayName, Instance.DisplayName);
                }
            }
            else if (Instance.attachments[0] == null)
            {
                foreach (Item item in items)
                {
                    if (item?.Category != Object.baitCategory || (modConfig().PreferredBait != "Any" && item.QualifiedItemId != modConfig().PreferredBait)) continue;
                    Instance.attachments[0] = (Object)item;
                    Game1.player.removeItemFromInventory(item);
                    CommonHelper.PushWarning(Instance, I18n.HudMessage_AutoAttach(), item.DisplayName, Instance.DisplayName);
                    break;
                }
            }
        }

        internal void AutoAttachTackles()
        {
            if (!IsRodNotInUse() || Game1.isFestival()) return;
            IList<Item> items = Game1.player.Items;
            if (Instance.UpgradeLevel >= 3) AttachTackleAtSlot(1, modConfig().PreferredTackle);
            if (Instance.UpgradeLevel == 4) AttachTackleAtSlot(2, modConfig().PreferredAdvIridiumTackle);

            void AttachTackleAtSlot(int attachmentSlot, string preferredTackle = "Any", string fallbackTackle = "(O)686")
            {
                if (Instance.attachments[attachmentSlot] != null) return;
                foreach (Item item in items)
                {
                    if (item?.Category != Object.tackleCategory || (preferredTackle != "Any" && item.QualifiedItemId != preferredTackle)) continue;
                    Instance.attachments[attachmentSlot] = (Object)item;
                    Game1.player.removeItemFromInventory(item);
                    CommonHelper.PushWarning(Instance, I18n.HudMessage_AutoAttach(), item.DisplayName, Instance.DisplayName);
                    break;
                }
            }
        }

        internal void AutoHook()
        {
            if (!IsRodCanHook()) return;
            Instance.timePerBobberBob = 1f;
            Instance.timeUntilFishingNibbleDone = FishingRod.maxTimeToNibble;
            Instance.DoFunction(Game1.player.currentLocation, (int)Instance.bobber.X, (int)Instance.bobber.Y, 1, Game1.player);
            Rumble.rumble(0.95f, 200f);
        }

        internal void OverrideCastPower()
        {
            if (Instance.isTimingCast && UnlockCastPowerTimer-- <= 0 && modConfig().UnlockCastPowerTime < 3f)
            {
                UnlockCastPowerTimer = 0;
                SmartCastPower = Instance.castingPower;
                SmartCastPowerSaved = true;
            }

            if (Instance.castedButBobberStillInAir) UnlockCastPowerTimer = modConfig().ParsedUnlockCastPowerTime;
            
            // Removed: Instance.castingPower = SmartCastPowerSaved ? SmartCastPower : modConfig().DefaultCastPower / 100.0f + 0.01f;
        }

        // BẢN VÁ LỖI 1: Hàm khôi phục logic gốc của Mồi Thử Thách và Mồi Tự Nhiên
        internal void UpdateVanillaBaitMechanics(BobberBar bar)
        {
            int caughtCount = Instance.numberOfFishCaught;

            // Phục hồi cơ chế gốc của Wild Bait (Mồi tự nhiên) - Tỉ lệ x2 cá dựa vào độ may mắn
            bool isLucky = Instance.GetBait()?.QualifiedItemId == "(O)774" && Game1.random.NextDouble() < 0.25 + Game1.player.DailyLuck / 2.0;
            caughtCount = Game1.isFestival() || bar.bossFish ? 1 : isLucky ? 2 : caughtCount;

            // Phục hồi cơ chế gốc của Challenge Bait (Mồi thử thách) - Tối đa 3 cá nếu hoàn hảo
            if (bar.challengeBaitFishes > 0)
            {
                caughtCount = bar.challengeBaitFishes;
            }

            Instance.numberOfFishCaught = caughtCount;
        }

        internal bool IsRodNotInUse() => Context.CanPlayerMove && !Instance.inUse();
        internal bool IsRodCanCast() => IsRodNotInUse() && !Game1.player.isMoving() && !Game1.player.isRidingHorse();
        
        // BẢN VÁ LỖI 2: Check thêm bùa Auto-Hook gốc của game để tránh xung đột
        private bool IsRodCanHook() => Instance is { isNibbling: true, hit: false, isReeling: false, pullingOutOfWater: false, fishCaught: false, showingTreasure: false } && !Instance.hasEnchantmentOfType<AutoHookEnchantment>();
        
        internal bool IsRodShowingFish() => !Context.CanPlayerMove && Instance.fishCaught && Instance.inUse() && Instance is { isCasting: false, isTimingCast: false, isReeling: false, pullingOutOfWater: false, showingTreasure: false };
    }
}