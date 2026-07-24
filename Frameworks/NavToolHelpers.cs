using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using ChibiKyu.StardewMods.FishingAssistant2; 

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    public class Waypoint
    {
        public string MapName { get; set; } = "";
        public Point? SpecificTile { get; set; } 

        public Waypoint(string mapName, Point? tile = null)
        {
            MapName = mapName;
            SpecificTile = tile;
        }
    }

    public class RouteSaveData
    {
        public string FarmName { get; set; } = "";
        public string CharacterName { get; set; } = "";
        public List<Waypoint> Waypoints { get; set; } = new List<Waypoint>();
    }

    public class MapGridSaveData
    {
        public List<Vector2> Obstacles { get; set; } = new List<Vector2>();
        public List<Vector2> Warps { get; set; } = new List<Vector2>();
        public List<Vector2> Walkables { get; set; } = new List<Vector2>(); 
    }

    public class RouteDashboardMenu : IClickableMenu
    {
        private ModEntry mod;
        private bool justRecorded;
        
        public int CurrentTab = 0; 
        public ClickableComponent TabRoutes = null!, TabSettings = null!;

        public TextBox NameTextBox = null!;
        public List<ClickableComponent> RouteSlots = new List<ClickableComponent>();
        public ClickableComponent RunBtn = null!, RunRevBtn = null!, ActionBtn = null!, DeleteBtn = null!, SaveBtn = null!, CancelBtn = null!;
        public string SelectedRoute = "";

        public ClickableComponent MasterToggleBtn = null!, GridToggleBtn = null!, SaveGridBtn = null!, LoadGridBtn = null!, ClearMapBtn = null!;

        public RouteDashboardMenu(ModEntry mod, bool justRecorded)
            : base(Game1.uiViewport.Width / 2 - 400, Game1.uiViewport.Height / 2 - 300, 800, 600, true)
        {
            this.mod = mod;
            this.justRecorded = justRecorded;
            this.initializeUpperRightCloseButton();

            this.CurrentTab = justRecorded ? 0 : this.mod.LastF10Tab;

            TabRoutes = new ClickableComponent(new Rectangle(this.xPositionOnScreen + 64, this.yPositionOnScreen - 55, 220, 60), this.mod.Helper.Translation.Get("navtool.tab.routes"));
            TabSettings = new ClickableComponent(new Rectangle(this.xPositionOnScreen + 294, this.yPositionOnScreen - 55, 220, 60), this.mod.Helper.Translation.Get("navtool.tab.settings"));

            Texture2D tbTexture = Game1.content.Load<Texture2D>("LooseSprites\\textBox");
            this.NameTextBox = new TextBox(tbTexture, null, Game1.smallFont, Game1.textColor) { Height = 48 };

            if (justRecorded)
            {
                this.CurrentTab = 0; 
                this.NameTextBox.X = this.xPositionOnScreen + this.width / 2 - 155;
                this.NameTextBox.Y = this.yPositionOnScreen + 260;
                this.NameTextBox.Width = 310;
                this.NameTextBox.Text = "AutoFishing"; 
                this.NameTextBox.SelectMe();
                Game1.keyboardDispatcher.Subscriber = this.NameTextBox;
                
                this.SaveBtn = new ClickableComponent(new Rectangle(this.xPositionOnScreen + this.width / 2 - 165, this.yPositionOnScreen + 350, 150, 55), this.mod.Helper.Translation.Get("navtool.btn.save-recorded"));
                this.CancelBtn = new ClickableComponent(new Rectangle(this.xPositionOnScreen + this.width / 2 + 15, this.yPositionOnScreen + 350, 150, 55), this.mod.Helper.Translation.Get("navtool.btn.cancel"));
            }
            else
            {
                int rightPanelX = this.xPositionOnScreen + 384;
                this.RunBtn = new ClickableComponent(new Rectangle(rightPanelX + 30, this.yPositionOnScreen + 160, 300, 55), this.mod.Helper.Translation.Get("navtool.btn.run"));
                this.RunRevBtn = new ClickableComponent(new Rectangle(rightPanelX + 30, this.yPositionOnScreen + 230, 300, 55), this.mod.Helper.Translation.Get("navtool.btn.run-rev"));
                this.NameTextBox.X = rightPanelX + 25;
                this.NameTextBox.Y = this.yPositionOnScreen + 360;
                this.NameTextBox.Width = 310;
                this.ActionBtn = new ClickableComponent(new Rectangle(rightPanelX + 20, this.yPositionOnScreen + 430, 155, 55), this.mod.Helper.Translation.Get("navtool.btn.rename"));
                this.DeleteBtn = new ClickableComponent(new Rectangle(rightPanelX + 185, this.yPositionOnScreen + 430, 155, 55), this.mod.Helper.Translation.Get("navtool.btn.delete"));

                int centerCol = this.xPositionOnScreen + this.width / 2 - 175;
                this.MasterToggleBtn = new ClickableComponent(new Rectangle(centerCol, this.yPositionOnScreen + 100, 350, 65), this.mod.Helper.Translation.Get("navtool.btn.master-on"));
                
                int startY = this.yPositionOnScreen + 200;
                this.GridToggleBtn = new ClickableComponent(new Rectangle(centerCol, startY, 350, 55), this.mod.Helper.Translation.Get("navtool.btn.grid-toggle"));
                this.SaveGridBtn = new ClickableComponent(new Rectangle(centerCol, startY + 75, 350, 55), this.mod.Helper.Translation.Get("navtool.btn.save-grid"));
                this.LoadGridBtn = new ClickableComponent(new Rectangle(centerCol, startY + 150, 350, 55), this.mod.Helper.Translation.Get("navtool.btn.load-grid"));
                this.ClearMapBtn = new ClickableComponent(new Rectangle(centerCol, startY + 225, 350, 55), this.mod.Helper.Translation.Get("navtool.btn.clear-map"));
            }
            this.RefreshRouteSlots();
        }

        public void RefreshRouteSlots()
        {
            this.RouteSlots.Clear();
            int startY = this.yPositionOnScreen + 100;
            if (this.mod.SavedRoutes != null)
            {
                foreach (var kvp in this.mod.SavedRoutes)
                {
                    string routeName = kvp.Key;
                    string display = $"{routeName} ({kvp.Value.CharacterName}/{kvp.Value.FarmName})";
                    this.RouteSlots.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + 48, startY, 310, 52), routeName) { label = display });
                    startY += 56;
                }
            }
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            base.receiveLeftClick(x, y, playSound); 

            if (!justRecorded)
            {
                if (TabRoutes.bounds.Contains(x, y) && CurrentTab != 0) { CurrentTab = 0; this.mod.LastF10Tab = 0; Game1.playSound("smallSelect"); return; }
                if (TabSettings.bounds.Contains(x, y) && CurrentTab != 1) { CurrentTab = 1; this.mod.LastF10Tab = 1; Game1.playSound("smallSelect"); return; }
            }

            if (CurrentTab == 0)
            {
                Rectangle tbRect = new Rectangle(this.NameTextBox.X, this.NameTextBox.Y, this.NameTextBox.Width, this.NameTextBox.Height);
                if (tbRect.Contains(x, y)) { this.NameTextBox.SelectMe(); Game1.keyboardDispatcher.Subscriber = this.NameTextBox; }
                else { this.NameTextBox.Selected = false; if (Game1.keyboardDispatcher.Subscriber == this.NameTextBox) Game1.keyboardDispatcher.Subscriber = null; }

                foreach (var slot in this.RouteSlots)
                {
                    if (slot.bounds.Contains(x, y) && !this.justRecorded)
                    {
                        Game1.playSound("smallSelect"); this.SelectedRoute = slot.name; this.NameTextBox.Text = slot.name; return;
                    }
                }

                if (!this.justRecorded && !string.IsNullOrEmpty(this.SelectedRoute))
                {
                    if (this.RunBtn.bounds.Contains(x, y)) { Game1.playSound("select"); this.mod.RunRoute(this.SelectedRoute); this.exitThisMenu(); }
                    else if (this.RunRevBtn.bounds.Contains(x, y)) { Game1.playSound("select"); this.mod.RunRouteReverse(this.SelectedRoute); this.exitThisMenu(); }
                    else if (this.DeleteBtn.bounds.Contains(x, y)) { Game1.playSound("trashcan"); this.mod.DeleteRoute(this.SelectedRoute); this.SelectedRoute = ""; this.NameTextBox.Text = ""; this.RefreshRouteSlots(); }
                    else if (this.ActionBtn.bounds.Contains(x, y))
                    {
                        string inputText = this.NameTextBox.Text.Trim();
                        if (!string.IsNullOrEmpty(inputText)) { Game1.playSound("coin"); this.mod.RenameRoute(this.SelectedRoute, inputText); this.SelectedRoute = inputText; this.RefreshRouteSlots(); }
                    }
                }
                else if (this.justRecorded)
                {
                    string inputText = this.NameTextBox.Text.Trim();
                    if (this.SaveBtn.bounds.Contains(x, y) && !string.IsNullOrEmpty(inputText)) { Game1.playSound("money"); this.mod.SaveRecordedRoute(inputText); this.exitThisMenu(); }
                    else if (this.CancelBtn.bounds.Contains(x, y)) { Game1.playSound("bigDeSelect"); this.mod.CancelRecording(); this.exitThisMenu(); }
                }
            }
            else if (CurrentTab == 1)
            {
                if (this.MasterToggleBtn.bounds.Contains(x, y))
                {
                    Game1.playSound("button1");
                    this.mod.IsGridEditMode = !this.mod.IsGridEditMode;
                    if (!this.mod.IsGridEditMode) { this.mod.isGridVisible = false; }
                    return;
                }

                if (this.mod.IsGridEditMode)
                {
                    if (this.GridToggleBtn.bounds.Contains(x, y)) { Game1.playSound("coin"); this.mod.isGridVisible = !this.mod.isGridVisible; }
                    else if (this.SaveGridBtn.bounds.Contains(x, y)) { Game1.playSound("money"); this.mod.SaveAllGridData(); Game1.addHUDMessage(new HUDMessage(this.mod.Helper.Translation.Get("navtool.msg.saved-grid"), 1)); }
                    else if (this.LoadGridBtn.bounds.Contains(x, y)) { Game1.playSound("dwop"); this.mod.LoadGridData(); Game1.addHUDMessage(new HUDMessage(this.mod.Helper.Translation.Get("navtool.msg.loaded-grid"), 1)); }
                    else if (this.ClearMapBtn.bounds.Contains(x, y)) { Game1.playSound("trashcan"); this.mod.ClearCurrentMapGrid(); }
                }
            }
        }

        private void DrawNativeButton(SpriteBatch b, ClickableComponent btn, Color baseColor, bool isHovered, bool isToggled = false)
        {
            if (btn == null) return;
            Color boxTint = isHovered ? Color.White : baseColor;
            if (isToggled) boxTint = Color.LimeGreen; 
            
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(432, 439, 9, 9), btn.bounds.X + 2, btn.bounds.Y + 2, btn.bounds.Width, btn.bounds.Height, Color.Black * 0.3f, 4f, true);
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(432, 439, 9, 9), btn.bounds.X, btn.bounds.Y, btn.bounds.Width, btn.bounds.Height, boxTint, 4f, true);
            
            Vector2 textSize = Game1.smallFont.MeasureString(btn.name);
            Vector2 textPos = new Vector2(btn.bounds.X + (btn.bounds.Width - textSize.X) / 2f, btn.bounds.Y + (btn.bounds.Height - textSize.Y) / 2f);
            if (isHovered) textPos.Y -= 2; 
            
            Color textColor = (isToggled && !isHovered) ? Color.White : Game1.textColor;
            Utility.drawTextWithShadow(b, btn.name, Game1.smallFont, textPos, textColor);
        }

        private void DrawTab(SpriteBatch b, ClickableComponent tab, bool isActive)
        {
            if (tab == null) return;
            int yOffset = isActive ? 0 : 10;
            int heightExt = isActive ? 15 : 0;
            Color boxColor = isActive ? Color.White : new Color(180, 180, 180);
            
            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), tab.bounds.X, tab.bounds.Y + yOffset, tab.bounds.Width, tab.bounds.Height + heightExt, boxColor, 1f, false);
            
            if (isActive)
            {
                 b.Draw(Game1.staminaRect, new Rectangle(tab.bounds.X + 4, tab.bounds.Y + tab.bounds.Height + 5, tab.bounds.Width - 8, 12), Color.White);
            }

            Vector2 textSize = Game1.smallFont.MeasureString(tab.name);
            Vector2 textPos = new Vector2(tab.bounds.X + (tab.bounds.Width - textSize.X) / 2f, tab.bounds.Y + yOffset + (tab.bounds.Height - textSize.Y) / 2f);
            Utility.drawTextWithShadow(b, tab.name, Game1.smallFont, textPos, isActive ? Game1.textColor : Color.DimGray * 0.9f);
        }

        public override void draw(SpriteBatch b)
        {
            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.6f);
            
            if (!this.justRecorded)
            {
                if (CurrentTab == 1) DrawTab(b, TabRoutes, false); else DrawTab(b, TabSettings, false);
                if (CurrentTab == 0) DrawTab(b, TabRoutes, true); else DrawTab(b, TabSettings, true);
            }

            Game1.drawDialogueBox(this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, false, true);

            if (this.justRecorded)
            {
                string title = this.mod.Helper.Translation.Get("navtool.title.recorded");
                Utility.drawTextWithShadow(b, title, Game1.dialogueFont, new Vector2(this.xPositionOnScreen + this.width / 2 - Game1.dialogueFont.MeasureString(title).X / 2, this.yPositionOnScreen + 100), Game1.textColor);
                this.NameTextBox.Draw(b);
                DrawNativeButton(b, this.SaveBtn, new Color(150, 250, 150), this.SaveBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                DrawNativeButton(b, this.CancelBtn, new Color(250, 150, 150), this.CancelBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
            }
            else if (CurrentTab == 0) 
            {
                int leftPanelX = this.xPositionOnScreen + 32; int leftPanelY = this.yPositionOnScreen + 85;
                int leftPanelW = 340; int leftPanelH = this.height - 120;
                
                b.Draw(Game1.staminaRect, new Rectangle(leftPanelX + 10, leftPanelY + 10, leftPanelW - 20, leftPanelH - 20), Color.Black * 0.05f);
                IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), leftPanelX, leftPanelY, leftPanelW, leftPanelH, Color.White, 1f, true);
                
                foreach (var slot in this.RouteSlots)
                {
                    bool isHovered = slot.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY());
                    bool isSelected = slot.name == this.SelectedRoute;

                    if (isSelected) 
                    {
                        b.Draw(Game1.staminaRect, slot.bounds, new Color(100, 200, 255) * 0.4f);
                        IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(379, 357, 3, 3), slot.bounds.X, slot.bounds.Y, slot.bounds.Width, slot.bounds.Height, Color.Blue, 3f, false);
                    }
                    else if (isHovered) b.Draw(Game1.staminaRect, slot.bounds, Color.Black * 0.15f);

                    string textToDraw = !string.IsNullOrEmpty(slot.label) ? slot.label : slot.name;
                    Utility.drawTextWithShadow(b, textToDraw, Game1.smallFont, new Vector2(slot.bounds.X + 16, slot.bounds.Y + 12), isSelected ? Color.White : Game1.textColor);
                }

                int rightPanelX = this.xPositionOnScreen + 384;
                IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), rightPanelX, leftPanelY, this.width - 340 - 80, leftPanelH, Color.White, 1f, true);

                if (!string.IsNullOrEmpty(this.SelectedRoute))
                {
                    Utility.drawTextWithShadow(b, this.SelectedRoute, Game1.dialogueFont, new Vector2(rightPanelX + (this.width - 340 - 80) / 2 - Game1.dialogueFont.MeasureString(this.SelectedRoute).X / 2, leftPanelY + 30), Color.DarkRed);
                    DrawNativeButton(b, this.RunBtn, new Color(180, 255, 180), this.RunBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                    DrawNativeButton(b, this.RunRevBtn, new Color(255, 230, 150), this.RunRevBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                    b.Draw(Game1.staminaRect, new Rectangle(rightPanelX + 20, this.yPositionOnScreen + 305, this.width - 340 - 120, 2), Color.Black * 0.15f);
                    this.NameTextBox.Draw(b);
                    DrawNativeButton(b, this.ActionBtn, new Color(180, 220, 255), this.ActionBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                    DrawNativeButton(b, this.DeleteBtn, new Color(255, 180, 180), this.DeleteBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                }
                else
                {
                    string hint = this.mod.Helper.Translation.Get("navtool.hint.select-route");
                    Utility.drawTextWithShadow(b, hint, Game1.smallFont, new Vector2(rightPanelX + (this.width - 340 - 80) / 2 - Game1.smallFont.MeasureString(hint).X / 2, leftPanelY + 200), Color.DimGray);
                }
            }
            else if (CurrentTab == 1) 
            {
                this.MasterToggleBtn.name = this.mod.IsGridEditMode ? this.mod.Helper.Translation.Get("navtool.btn.master-on") : this.mod.Helper.Translation.Get("navtool.btn.master-off");
                DrawNativeButton(b, this.MasterToggleBtn, Color.LightGray, this.MasterToggleBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()), this.mod.IsGridEditMode);
                
                b.Draw(Game1.staminaRect, new Rectangle(this.xPositionOnScreen + 100, this.yPositionOnScreen + 180, this.width - 200, 2), Color.Black * 0.15f);

                if (this.mod.IsGridEditMode)
                {
                    DrawNativeButton(b, this.GridToggleBtn, Color.White, this.GridToggleBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()), this.mod.isGridVisible);
                    DrawNativeButton(b, this.SaveGridBtn, new Color(180, 240, 255), this.SaveGridBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                    DrawNativeButton(b, this.LoadGridBtn, new Color(255, 240, 180), this.LoadGridBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                    DrawNativeButton(b, this.ClearMapBtn, new Color(255, 180, 180), this.ClearMapBtn.bounds.Contains(Game1.getOldMouseX(), Game1.getOldMouseY()));
                }
                else
                {
                    string warn = this.mod.Helper.Translation.Get("navtool.warn.activate");
                    Utility.drawTextWithShadow(b, warn, Game1.smallFont, new Vector2(this.xPositionOnScreen + this.width / 2 - Game1.smallFont.MeasureString(warn).X / 2, this.yPositionOnScreen + 250), Color.DimGray);
                }
            }

            base.draw(b); 
            this.drawMouse(b); 
        }

        public override void emergencyShutDown()
        {
            base.emergencyShutDown();
            if (Game1.keyboardDispatcher.Subscriber == this.NameTextBox) Game1.keyboardDispatcher.Subscriber = null; 
        }
    }
}