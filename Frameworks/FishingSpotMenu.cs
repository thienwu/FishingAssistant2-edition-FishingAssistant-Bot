using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using StardewModdingAPI;
using System.Collections.Generic;
using System.Linq;
using ChibiKyu.StardewMods.Common;
using StardewValley.BellsAndWhistles;

namespace ChibiKyu.StardewMods.FishingAssistant2.Frameworks
{
    internal class FishingSpotMenu : IClickableMenu
    {
        private readonly ModConfig _config;
        private readonly IModHelper _helper;
        private readonly ModEntry _modEntry;
        private List<string> _spotNames = new();
        private int _currentPage = 0;
        private const int ItemsPerPage = 4;

        private ClickableTextureComponent _upButton = null!;
        private ClickableTextureComponent _downButton = null!;
        private List<ClickableComponent> _spotSlots = new();
        
        private List<ClickableComponent> _renameButtons = new();
        private List<ClickableComponent> _deleteButtons = new();

        internal FishingSpotMenu(ModConfig config, IModHelper helper, ModEntry modEntry)
            : base(Game1.uiViewport.Width / 2 - 400, Game1.uiViewport.Height / 2 - 300, 800, 600, true)
        {
            _config = config;
            _helper = helper;
            _modEntry = modEntry;
            
            this.initializeUpperRightCloseButton();
            RefreshSpotList();
        }

        private void RefreshSpotList()
        {
            _spotNames = _config.SavedSpots.Keys.ToList();
            SetupComponents();
        }

        private void SetupComponents()
        {
            _spotSlots.Clear();
            _renameButtons.Clear();
            _deleteButtons.Clear();

            int insetX = xPositionOnScreen + 32;
            int insetY = yPositionOnScreen + 85;
            int insetWidth = width - 64;
            int slotHeight = 100; 

            for (int i = 0; i < ItemsPerPage; i++)
            {
                int currentY = insetY + 16 + (i * (slotHeight + 8));
                _spotSlots.Add(new ClickableComponent(new Rectangle(insetX + 16, currentY, insetWidth - 32, slotHeight), i.ToString()));

                int btnWidth = 110;
                int btnHeight = 48;
                int btnY = currentY + 26;

                _renameButtons.Add(new ClickableComponent(
                    new Rectangle(insetX + insetWidth - 32 - btnWidth * 2 - 16, btnY, btnWidth, btnHeight), "rename"));
                _deleteButtons.Add(new ClickableComponent(
                    new Rectangle(insetX + insetWidth - 32 - btnWidth, btnY, btnWidth, btnHeight), "delete"));
            }

            _upButton = new ClickableTextureComponent(new Rectangle(insetX + insetWidth + 4, insetY + 32, 44, 48), Game1.mouseCursors, new Rectangle(421, 459, 11, 12), 4f);
            _downButton = new ClickableTextureComponent(new Rectangle(insetX + insetWidth + 4, insetY + height - 128 - 64, 44, 48), Game1.mouseCursors, new Rectangle(421, 472, 11, 12), 4f);
        }

        public override void performHoverAction(int x, int y)
        {
            base.performHoverAction(x, y);
            _upButton.tryHover(x, y);
            _downButton.tryHover(x, y);
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {
            base.receiveLeftClick(x, y, playSound);
            if (_upButton.containsPoint(x, y) && _currentPage > 0)
            {
                _currentPage--;
                Game1.playSound("shwip"); 
                return;
            }
            if (_downButton.containsPoint(x, y) && (_currentPage + 1) * ItemsPerPage < _spotNames.Count)
            {
                _currentPage++;
                Game1.playSound("shwip"); 
                return;
            }

            for (int i = 0; i < ItemsPerPage; i++)
            {
                int actualIndex = (_currentPage * ItemsPerPage) + i;
                if (actualIndex >= _spotNames.Count) break;
                string spotName = _spotNames[actualIndex];

                if (_renameButtons[i].containsPoint(x, y))
                {
                    Game1.playSound("smallSelect");
                    Game1.activeClickableMenu = new NamingMenu(newName =>
                    {
                        if (!string.IsNullOrWhiteSpace(newName) && newName != spotName && _config.SavedSpots.TryGetValue(spotName, out var spotData))
                        {
                            _config.SavedSpots.Remove(spotName);
                            _config.SavedSpots[newName] = spotData;
                            
                            if (_config.ActiveSpotName == spotName) 
                                _config.ActiveSpotName = newName;
                            
                            _helper.WriteConfig(_config);
                            CommonHelper.PushWarning(_helper.Translation.Get("hud-message.spot-renamed").ToString(), spotName, newName);
                        }
                        
                        Game1.exitActiveMenu();
                        _modEntry._shouldOpenSpotMenu = true;
                        
                    }, _helper.Translation.Get("spot-menu.rename-prompt").ToString(), spotName);
                }
                else if (_deleteButtons[i].containsPoint(x, y))
                {
                    Game1.playSound("trashcan");
                    _config.SavedSpots.Remove(spotName);
                    if (_config.ActiveSpotName == spotName) _config.ActiveSpotName = "";
                    _helper.WriteConfig(_config);
                    
                    CommonHelper.PushWarning(_helper.Translation.Get("hud-message.spot-deleted").ToString(), spotName);
                    RefreshSpotList();
                }
                else if (_spotSlots[i].containsPoint(x, y))
                {
                    _config.ActiveSpotName = spotName;
                    _helper.WriteConfig(_config);
                    
                    Game1.playSound("coin");
                    CommonHelper.PushWarning(_helper.Translation.Get("hud-message.spot-active").ToString(), spotName);

                    exitThisMenu();
                }
            }
        }

        private void DrawStardewButton(SpriteBatch b, Rectangle bounds, string text, bool isHovered, Color hoverColor)
        {
            Color bgColor = isHovered ? hoverColor : Color.White;
            
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(432, 439, 9, 9), bounds.X, bounds.Y, bounds.Width, bounds.Height, bgColor, 4f, false);
            Vector2 textSize = Game1.dialogueFont.MeasureString(text);
            Vector2 textPos = new Vector2(
                bounds.X + (bounds.Width - textSize.X) / 2f,
                bounds.Y + (bounds.Height - textSize.Y) / 2f
            );
            Utility.drawTextWithShadow(b, text, Game1.dialogueFont, textPos, Game1.textColor);
        }

        public override void draw(SpriteBatch b)
        {
            if (!Game1.options.showMenuBackground) b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);
            Game1.drawDialogueBox(xPositionOnScreen, yPositionOnScreen, width, height, false, true);
            SpriteText.drawStringWithScrollCenteredAt(b, _helper.Translation.Get("spot-menu.title").ToString(), xPositionOnScreen + width / 2, yPositionOnScreen + 24);
            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), xPositionOnScreen + 32, yPositionOnScreen + 85, width - 64, height - 110, Color.White, 1f, false);
            if (_spotNames.Count == 0)
            {
                Utility.drawTextWithShadow(b, _helper.Translation.Get("spot-menu.empty").ToString(), Game1.dialogueFont, new Vector2(xPositionOnScreen + 100, yPositionOnScreen + 200), Game1.textColor);
            }
            else
            {
                for (int i = 0; i < ItemsPerPage; i++)
                {
                    int actualIndex = (_currentPage * ItemsPerPage) + i;
                    if (actualIndex >= _spotNames.Count) break;

                    string spotName = _spotNames[actualIndex];
                    FishingSpot spotData = _config.SavedSpots[spotName];
                    bool isActive = spotName == _config.ActiveSpotName;
                    int mouseX = Game1.getOldMouseX();
                    int mouseY = Game1.getOldMouseY();
                    
                    bool isRenameHovered = _renameButtons[i].containsPoint(mouseX, mouseY);
                    bool isDeleteHovered = _deleteButtons[i].containsPoint(mouseX, mouseY);
                    bool isHoveringSlot = _spotSlots[i].containsPoint(mouseX, mouseY) && !isRenameHovered && !isDeleteHovered;
                    
                    if (isActive)
                        IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), _spotSlots[i].bounds.X, _spotSlots[i].bounds.Y, _spotSlots[i].bounds.Width, _spotSlots[i].bounds.Height, Color.Wheat, 4f, false);
                    else if (isHoveringSlot)
                        IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), _spotSlots[i].bounds.X, _spotSlots[i].bounds.Y, _spotSlots[i].bounds.Width, _spotSlots[i].bounds.Height, Color.LightGray, 4f, false);
                    else
                        IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), _spotSlots[i].bounds.X, _spotSlots[i].bounds.Y, _spotSlots[i].bounds.Width, _spotSlots[i].bounds.Height, Color.White, 4f, false);

                    Utility.drawTextWithShadow(b, spotName, Game1.dialogueFont, new Vector2(_spotSlots[i].bounds.X + 24, _spotSlots[i].bounds.Y + 16), isActive ? Color.DarkGreen : Game1.textColor);
                    if (isActive)
                    {
                        Vector2 textSize = Game1.dialogueFont.MeasureString(spotName);
                        b.Draw(Game1.mouseCursors, new Vector2(_spotSlots[i].bounds.X + 32 + textSize.X, _spotSlots[i].bounds.Y + 20), new Rectangle(346, 392, 8, 8), Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 1f);
                    }

                    Utility.drawTextWithShadow(b, $"{spotData.MapName} ({spotData.X}, {spotData.Y})", Game1.smallFont, new Vector2(_spotSlots[i].bounds.X + 24, _spotSlots[i].bounds.Y + 56), Color.DimGray);
                    
                    string renameStr = "Đổi tên";
                    string deleteStr = "Xóa";
                    if (_helper.Translation.Get("spot-menu.btn-rename").HasValue()) renameStr = _helper.Translation.Get("spot-menu.btn-rename").ToString();
                    if (_helper.Translation.Get("spot-menu.btn-delete").HasValue()) deleteStr = _helper.Translation.Get("spot-menu.btn-delete").ToString();
                    
                    DrawStardewButton(b, _renameButtons[i].bounds, renameStr, isRenameHovered, Color.Wheat);
                    DrawStardewButton(b, _deleteButtons[i].bounds, deleteStr, isDeleteHovered, Color.LightPink);
                }
            }

            if (_currentPage > 0) _upButton.draw(b);
            if ((_currentPage + 1) * ItemsPerPage < _spotNames.Count) _downButton.draw(b);

            base.draw(b); 
            drawMouse(b); 
        }
    }
}