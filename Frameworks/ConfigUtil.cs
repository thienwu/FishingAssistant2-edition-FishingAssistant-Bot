using System;
using System.Collections.Generic;
using System.Linq;
using ChibiKyu.StardewMods.Common;
using FishingAssistant2;
using StardewModdingAPI;
using StardewValley;
using Object = StardewValley.Object;

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    public class ConfigUtil(IGenericModConfigMenuApi? configMenu, IManifest modManifest, Func<ModConfig> config)
    {
        internal class KeyBindOption { internal Func<SButton> GetValue = null!; internal Func<string> Name = null!; internal Action<SButton> SetValue = null!; internal Func<string> Tooltip = null!; }
        internal class DropdownOption { internal string[] AllowedValue = null!; internal Func<string, string> FormatAllowedValue = null!; internal Func<string> GetValue = null!; internal Func<string> Name = null!; internal Action<string> SetValue = null!; internal Func<string> Tooltip = null!; }
        internal class BoolOption { internal Func<bool> GetValue = null!; internal Func<string> Name = null!; internal Action<bool> SetValue = null!; internal Func<string> Tooltip = null!; }
        internal class IntOption { internal Func<int, string>? FormatValue; internal Func<int> GetValue = null!; internal int? Interval; internal int? Max; internal int? Min; internal Func<string> Name = null!; internal Action<int> SetValue = null!; internal Func<string> Tooltip = null!; }
        internal class FloatOption { internal Func<float, string>? FormatValue; internal Func<float> GetValue = null!; internal float? Interval; internal float? Max; internal float? Min; internal Func<string> Name = null!; internal Action<float> SetValue = null!; internal Func<string> Tooltip = null!; }
        internal class TextOption { internal Func<string> GetValue = null!; internal Action<string> SetValue = null!; internal Func<string> Name = null!; internal Func<string> Tooltip = null!; }

        internal readonly KeyBindOption CatchTreasure = new() { Name = I18n.ConfigMenu_Option_CatchTreasure, GetValue = () => config().CatchTreasureButton, SetValue = button => config().CatchTreasureButton = button, Tooltip = I18n.Tooltip_CatchTreasureButton };
        internal readonly KeyBindOption OpenConfigMenu = new() { Name = I18n.ConfigMenu_Option_OpenConfigMenu, GetValue = () => config().OpenConfigMenuButton, SetValue = button => config().OpenConfigMenuButton = button, Tooltip = I18n.Tooltip_OpenConfigMenu };
        internal readonly KeyBindOption ToggleAutomation = new() { Name = I18n.ConfigMenu_Option_ToggleAutomation, GetValue = () => config().EnableAutomationButton, SetValue = button => config().EnableAutomationButton = button, Tooltip = I18n.Tooltip_EnableAutomationButton };
        internal readonly DropdownOption HudPosition = new() { Name = I18n.ConfigMenu_Option_HudPosition, GetValue = () => config().ModStatusPosition, SetValue = option => config().ModStatusPosition = option, AllowedValue = HudPositionOptions(), FormatAllowedValue = ParseHudPosition, Tooltip = I18n.ConfigMenu_Option_HudPosition };
        internal readonly DropdownOption ActionIfInventoryFull = new() { Name = I18n.ConfigMenu_Option_ActionIfInventoryFull, GetValue = () => config().ActionIfInventoryFull, SetValue = option => config().ActionIfInventoryFull = option, AllowedValue = ActionOnInventoryFullOptions(), FormatAllowedValue = ParseActionOnInventoryFull, Tooltip = I18n.Tooltip_ActionIfInventoryFull };
        internal readonly DropdownOption AutoPauseFishing = new() { Name = I18n.ConfigMenu_Option_AutoPauseFishing, GetValue = () => config().AutoPauseFishing, SetValue = option => config().AutoPauseFishing = option, AllowedValue = PauseFishingOptions(), FormatAllowedValue = ParsePauseFishing, Tooltip = I18n.Tooltip_AutoPauseFishing };
        internal readonly DropdownOption PreferBait = new() { Name = I18n.ConfigMenu_Option_PreferBait, GetValue = () => config().PreferredBait, SetValue = option => config().PreferredBait = option, AllowedValue = PreferredBaitOptions(), FormatAllowedValue = ParseItemName, Tooltip = I18n.Tooltip_PreferredBait };
        internal readonly DropdownOption PreferredTackle = new() { Name = I18n.ConfigMenu_Option_PreferTackle, GetValue = () => config().PreferredTackle, SetValue = option => config().PreferredTackle = option, AllowedValue = PreferredTackleOptions(), FormatAllowedValue = ParseItemName, Tooltip = I18n.Tooltip_PreferredTackle };
        internal readonly DropdownOption PreferredAdvIridiumTackle = new() { Name = I18n.ConfigMenu_Option_PreferAdvancedIridiumTackle, GetValue = () => config().PreferredAdvIridiumTackle, SetValue = option => config().PreferredAdvIridiumTackle = option, AllowedValue = PreferredTackleOptions(), FormatAllowedValue = ParseItemName, Tooltip = I18n.Tooltip_PreferredAdvIridiumTackle };
        internal readonly DropdownOption StartWithFishingRod = new() { Name = I18n.ConfigMenu_Option_StartWithFishingRod, GetValue = () => config().StartWithFishingRod, SetValue = option => config().StartWithFishingRod = option, AllowedValue = StartWithFishingRodOptions(), FormatAllowedValue = ParseItemName, Tooltip = I18n.Tooltip_StartWithFishingRod };
        
        internal readonly BoolOption AutoCastFishingRod = new() { Name = I18n.ConfigMenu_Option_AutoCastFishingRod, GetValue = () => config().AutoCastFishingRod, SetValue = value => config().AutoCastFishingRod = value, Tooltip = I18n.Tooltip_AutoCastFishingRod };
        internal readonly BoolOption AutoHookFish = new() { Name = I18n.ConfigMenu_Option_AutoHookFish, GetValue = () => config().AutoHookFish, SetValue = value => config().AutoHookFish = value, Tooltip = I18n.Tooltip_AutoHookFish };
        internal readonly BoolOption AutoPlayMiniGame = new() { Name = I18n.ConfigMenu_Option_AutoPlayMiniGame, GetValue = () => config().AutoPlayMiniGame, SetValue = value => config().AutoPlayMiniGame = value, Tooltip = I18n.Tooltip_AutoPlayMiniGame };
        internal readonly BoolOption AutoClosePopup = new() { Name = I18n.ConfigMenu_Option_AutoClosePopup, GetValue = () => config().AutoClosePopup, SetValue = value => config().AutoClosePopup = value, Tooltip = I18n.Tooltip_AutoClosePopup };
        internal readonly BoolOption AutoLootTreasure = new() { Name = I18n.ConfigMenu_Option_AutoLootTreasure, GetValue = () => config().AutoLootTreasure, SetValue = value => config().AutoLootTreasure = value, Tooltip = I18n.Tooltip_AutoLootTreasure };
        internal readonly BoolOption AutoTrashJunk = new() { Name = I18n.ConfigMenu_Option_AutoTrashJunk, GetValue = () => config().AutoTrashJunk, SetValue = value => config().AutoTrashJunk = value, Tooltip = I18n.Tooltip_AutoTrashJunk };
        internal readonly BoolOption AllowTrashFish = new() { Name = I18n.ConfigMenu_Option_AllowTrashFish, GetValue = () => config().AllowTrashFish, SetValue = value => config().AllowTrashFish = value, Tooltip = I18n.Tooltip_AllowTrashFish };
        internal readonly BoolOption AutoEatFood = new() { Name = I18n.ConfigMenu_Option_AutoEatFood, GetValue = () => config().AutoEatFood, SetValue = value => config().AutoEatFood = value, Tooltip = I18n.Tooltip_AutoEatFood };
        internal readonly BoolOption AllowEatingFish = new() { Name = I18n.ConfigMenu_Option_AllowEatingFish, GetValue = () => config().AllowEatingFish, SetValue = value => config().AllowEatingFish = value, Tooltip = I18n.Tooltip_AllowEatingFish };
        internal readonly BoolOption AutoAttachBait = new() { Name = I18n.ConfigMenu_Option_AutoAttachBait, GetValue = () => config().AutoAttachBait, SetValue = value => config().AutoAttachBait = value, Tooltip = I18n.Tooltip_AutoAttachBait };
        internal readonly BoolOption AutoAttachTackles = new() { Name = I18n.ConfigMenu_Option_AutoAttachTackles, GetValue = () => config().AutoAttachTackles, SetValue = value => config().AutoAttachTackles = value, Tooltip = I18n.Tooltip_AutoAttachTackles };
        
        internal readonly IntOption JunkHighestPrice = new() { Name = I18n.ConfigMenu_Option_JunkHighestPrice, Tooltip = I18n.Tooltip_JunkHighestPrice, GetValue = () => config().JunkHighestPrice, SetValue = value => config().JunkHighestPrice = value, Min = 0 };
        internal readonly IntOption TimeToPause = new() { Name = I18n.ConfigMenu_Option_TimeToPause, Tooltip = I18n.Tooltip_TimeToPause, GetValue = () => config().TimeToPause, SetValue = value => config().TimeToPause = value, Min = 6, Max = 25, FormatValue = value => Game1.getTimeOfDayString(value * 100) };
        internal readonly IntOption WarnCount = new() { Name = I18n.ConfigMenu_Option_WarnCount, Tooltip = I18n.Tooltip_WarnCount, GetValue = () => config().WarnCount, SetValue = value => config().WarnCount = value, Min = 1, Max = 5, Interval = 1 };
        internal readonly IntOption EnergyPercentToEat = new() { Name = I18n.ConfigMenu_Option_EnergyPercentToEat, Tooltip = I18n.Tooltip_EnergyPercentToEat, GetValue = () => config().EnergyPercentToEat, SetValue = value => config().EnergyPercentToEat = value, Min = 5, Max = 95, Interval = 5 };
        //internal readonly IntOption PreferFishAmount = new() { Name = I18n.ConfigMenu_Option_PreferFishAmount, Tooltip = I18n.Tooltip_PreferFishAmount, GetValue = () => config().PreferFishAmount, SetValue = value => config().PreferFishAmount = value, Min = 1, Max = 3, Interval = 1 };
        internal readonly IntOption DefaultCastPower = new() { Name = I18n.ConfigMenu_Option_DefaultCastPower, Tooltip = I18n.Tooltip_DefaultCastPower, GetValue = () => config().DefaultCastPower, SetValue = value => config().DefaultCastPower = value, Min = 0, Max = 100, Interval = 5 };
        internal readonly FloatOption UnlockCastPowerTime = new() { Name = I18n.ConfigMenu_Option_UnlockCastPowerTime, Tooltip = I18n.Tooltip_UnlockCastPowerTime, GetValue = () => config().UnlockCastPowerTime, SetValue = value => config().UnlockCastPowerTime = value, Min = 0f, Max = 3f, Interval = 1f, FormatValue = ParseUnlockCastPowerTime };
        
        internal readonly TextOption JunkIgnoreList = new() { Name = I18n.ConfigMenu_Option_JunkIgnoreList, GetValue = () => string.Join(',', config().JunkIgnoreList), SetValue = value => config().JunkIgnoreList = value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList(), Tooltip = I18n.Tooltip_JunkIgnoreList };
        
        internal void AddKeyBind(KeyBindOption option) => configMenu?.AddKeybind(modManifest, name: option.Name, getValue: option.GetValue, setValue: option.SetValue, tooltip: option.Tooltip);
        internal void AddDropDown(DropdownOption option) => configMenu?.AddTextOption(modManifest, name: option.Name, getValue: option.GetValue, setValue: option.SetValue, allowedValues: option.AllowedValue, formatAllowedValue: option.FormatAllowedValue, tooltip: option.Tooltip);
        internal void AddBool(BoolOption option) => configMenu?.AddBoolOption(modManifest, name: option.Name, getValue: option.GetValue, setValue: option.SetValue, tooltip: option.Tooltip);
        internal void AddNumber(IntOption option) => configMenu?.AddNumberOption(modManifest, option.GetValue, option.SetValue, option.Name, min: option.Min, max: option.Max, interval: option.Interval, formatValue: option.FormatValue, tooltip: option.Tooltip);
        internal void AddNumber(FloatOption option) => configMenu?.AddNumberOption(modManifest, option.GetValue, option.SetValue, option.Name, min: option.Min, max: option.Max, interval: option.Interval, formatValue: option.FormatValue, tooltip: option.Tooltip);
        internal void AddText(TextOption option) => configMenu?.AddTextOption(modManifest, getValue: option.GetValue, setValue: option.SetValue, name: option.Name, tooltip: option.Tooltip);
        
        private static string[] StartWithFishingRodOptions() { List<string> availableBaits = ["None", "(T)TrainingRod", "(T)BambooPole"]; return availableBaits.ToArray(); }
        private static string[] PreferredBaitOptions() { List<string> availableBaits = ["Any"]; availableBaits.AddRange(from item in Game1.objectData where item.Value.Category == Object.baitCategory select ItemRegistry.QualifyItemId(item.Key)); return availableBaits.ToArray(); }
        private static string[] PreferredTackleOptions() { List<string> availableTackles = ["Any"]; availableTackles.AddRange(from item in Game1.objectData where item.Value.Category == Object.tackleCategory select ItemRegistry.QualifyItemId(item.Key)); return availableTackles.ToArray(); }
        private static string[] PauseFishingOptions() => Enum.GetNames(typeof(PauseFishingBehaviour));
        private static string[] ActionOnInventoryFullOptions() => Enum.GetNames(typeof(ActionOnInventoryFull));
        private static string[] HudPositionOptions() => Enum.GetNames(typeof(HudPosition));
        
        private static string ParseUnlockCastPowerTime(float value) => value switch { <= 0.0f => I18n.InstantUnlock(), >= 3.0f => I18n.NeverUnlock(), _ => string.Format(I18n.Second(), value) };
        private static string ParsePauseFishing(string rawText) { if (!Enum.TryParse(rawText, out PauseFishingBehaviour text)) return rawText; return text switch { PauseFishingBehaviour.Off => I18n.Off(), PauseFishingBehaviour.WarnOnly => I18n.WarnOnly(), PauseFishingBehaviour.WarnAndPause => I18n.WarnAndPause(), _ => text.ToString() }; }
        private static string ParseItemName(string rawText) => rawText switch { "Any" => I18n.Any(), "None" => I18n.None(), _ => ItemRegistry.GetData(rawText).DisplayName };
        private static string ParseActionOnInventoryFull(string rawText) { if (!Enum.TryParse(rawText, out ActionOnInventoryFull text)) return rawText; return text switch { ActionOnInventoryFull.Stop => I18n.StopLoot(), ActionOnInventoryFull.Drop => I18n.DropRemaining(), ActionOnInventoryFull.Discard => I18n.DiscardRemaining(), _ => text.ToString() }; }
        private static string ParseHudPosition(string rawText) { if (!Enum.TryParse(rawText, out HudPosition text)) return rawText; return text switch { Frameworks.HudPosition.Left => I18n.Left(), Frameworks.HudPosition.Right => I18n.Right(), _ => text.ToString() }; }
    }
}