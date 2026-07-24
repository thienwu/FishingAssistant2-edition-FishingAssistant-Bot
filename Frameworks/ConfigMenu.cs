using ChibiKyu.StardewMods.Common;
using FishingAssistant2;
using StardewModdingAPI;
using System;

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    internal class ConfigMenu(
        IModRegistry modRegistry,
        IManifest modManifest,
        Func<ModConfig> config,
        Action reset,
        Action save,
        Action onConfigSavedCallback)
    {
        internal readonly Action OnConfigSavedCallback = onConfigSavedCallback;
        private IGenericModConfigMenuApi? _configMenu;
        private ConfigUtil? _configUtil;

        public void RegisterModConfigMenu()
        {
            _configMenu = modRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (_configMenu is null) return;

            _configMenu.Register(modManifest, reset, save);
            _configUtil = new ConfigUtil(_configMenu, modManifest, config);

            AddSectionTitle(I18n.ConfigMenu_Title_GoToPage);
            AddPageLink(I18n.ConfigMenu_Page_General, "General");
            AddPageLink(I18n.ConfigMenu_Page_FishingRod, "FishingRod");

            AddPage(I18n.ConfigMenu_Page_General, "General");
            AddSectionTitle(I18n.ConfigMenu_Title_KeyBinding);
            _configUtil.AddKeyBind(_configUtil.ToggleAutomation);
            _configUtil.AddKeyBind(_configUtil.CatchTreasure);
            _configUtil.AddKeyBind(_configUtil.OpenConfigMenu);

            AddSectionTitle(I18n.ConfigMenu_Title_Hud);
            _configUtil.AddDropDown(_configUtil.HudPosition);

            AddSectionTitle(I18n.ConfigMenu_Title_Automation);
            _configUtil.AddBool(_configUtil.AutoCastFishingRod);
            _configUtil.AddBool(_configUtil.AutoHookFish);
            _configUtil.AddBool(_configUtil.AutoPlayMiniGame);
            _configUtil.AddBool(_configUtil.AutoClosePopup);
            _configUtil.AddBool(_configUtil.AutoLootTreasure);
            _configUtil.AddDropDown(_configUtil.ActionIfInventoryFull);
            _configUtil.AddBool(_configUtil.AutoTrashJunk);
            _configUtil.AddNumber(_configUtil.JunkHighestPrice);
            _configUtil.AddBool(_configUtil.AllowTrashFish);
            _configUtil.AddText(_configUtil.JunkIgnoreList);
            _configUtil.AddDropDown(_configUtil.AutoPauseFishing);
            _configUtil.AddNumber(_configUtil.TimeToPause);
            _configUtil.AddNumber(_configUtil.WarnCount);
            _configUtil.AddBool(_configUtil.AutoEatFood);
            _configUtil.AddNumber(_configUtil.EnergyPercentToEat);
            _configUtil.AddBool(_configUtil.AllowEatingFish);
            _configUtil.AddBool(_configUtil.AutoAttachBait);
            _configUtil.AddDropDown(_configUtil.PreferBait);
            _configUtil.AddBool(_configUtil.AutoAttachTackles);
            _configUtil.AddDropDown(_configUtil.PreferredTackle);
            _configUtil.AddDropDown(_configUtil.PreferredAdvIridiumTackle);

            AddPage(I18n.ConfigMenu_Page_General, "FishingRod");
            AddSectionTitle(I18n.ConfigMenu_Title_FishingRod);
            _configUtil.AddDropDown(_configUtil.StartWithFishingRod);
            _configUtil.AddNumber(_configUtil.DefaultCastPower);
            _configUtil.AddNumber(_configUtil.UnlockCastPowerTime);
        }

        internal void OpenModMenu() => _configMenu?.OpenModMenu(modManifest);
        private void AddPage(Func<string> text, string pageTitle) => _configMenu?.AddPage(modManifest, $"Custom.FishingAssistant.{pageTitle}", text);
        private void AddPageLink(Func<string> text, string pageTitle) => _configMenu?.AddPageLink(modManifest, $"Custom.FishingAssistant.{pageTitle}", text);
        private void AddSectionTitle(Func<string> text) => _configMenu?.AddSectionTitle(modManifest, text);
    }
}