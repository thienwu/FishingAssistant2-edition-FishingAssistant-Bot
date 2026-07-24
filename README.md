# 🎣 Fishing Assistant Bot 

![FishingAssistant2](Image/FishingAssistant2.gif)

Một người bạn đồng hành thông minh và thân thiện dành cho bạn trong thế giới `Stardew Valley`! Được phát triển dựa trên bản gốc Fishing Assistant 2, phiên bản đặc biệt này mang đến một chú **Bot tự dò đường** cực kỳ thông minh cho trang trại của bạn. 

Không chỉ giúp bạn tự động câu cá từ A-Z, chú Bot này còn biết tự động đi bộ đến điểm câu cá, câu cật lực cả ngày, và quan trọng nhất là: tự động mò đường về tận giường ngủ an toàn trước khi bạn bị ngất xỉu giữa đêm!

---

## 🌟 Có gì mới trong bản Bot này?

### 🤖 Tự Động Dò Đường (Smart Navigation)
Bot được trang bị "mắt thần", có khả năng nhận diện không gian xung quanh để né tránh cây cối, nhà cửa, và cả các NPC. Nó có thể dễ dàng tự chạy qua nhiều bản đồ khác nhau (ví dụ: chạy từ trong nhà FarmHouse ra tít ngoài Bãi Biển).

### 🗺️ Hệ thống Ghi hình Tuyến đường
Bạn có thể tự tay "dạy" cho Bot cách đi đến điểm câu cá yêu thích của bạn một cách cực kỳ đơn giản!
- Đứng ngay cạnh giường ngủ của bạn và bấm phím `F9` để bắt đầu ghi hình.
- Chạy bộ bình thường đến điểm câu cá bạn muốn.
- Khi đến sát mép nước, bấm phím `F9` một lần nữa để chốt và lưu lại tuyến đường này (được đặt tên là `AutoFishing`).

### 🛏️ Tự Động Đi Ngủ & Tiếp Tục Vào Hôm Sau
Tạm biệt những ngày bị phạt tiền vì ngủ gật ngoài đường lúc 2:00 sáng!
- Khi đồng hồ trong game điểm đúng "Giờ nghỉ ngơi" do bạn cài đặt (ví dụ 1:00 AM), Bot sẽ tự động cất cần câu.
- Nó sẽ tự chạy ngược lại tuyến đường cũ, mở cửa vào nhà và chui thẳng lên giường.
- **Tuyệt vời hơn nữa:** Sáng hôm sau khi thức dậy, Bot sẽ tự động chạy thẳng ra đúng điểm câu cá đó để tiếp tục công việc!

### 🛠️ Menu Tùy Chỉnh Lưới Dò Đường (Grid Editor)
Bấm phím `F10` để mở menu cài đặt của NavTool. Tại đây, bạn có thể bật hiển thị "lưới tàng hình" để xem chính xác Bot đang nhìn thấy những ô nào là đi được (màu xanh) và những ô nào bị chặn (màu đỏ).

---

## 🐟 Các tính năng câu cá cốt lõi

Tất cả những tính năng tuyệt vời của bản Fishing Assistant 2 gốc vẫn được giữ nguyên:

- **Rảnh tay 100%:** Tự động quăng cần, tự động giật cá, và tự động chơi minigame hoàn hảo.
- **Quản lý túi đồ thông minh:** Tự động nhặt rương kho báu. Nếu túi đầy, nó có thể vứt bỏ hoặc tự động phi tang những món rác rẻ tiền.
- **Tự động ăn uống:** Tự động phát hiện khi thể lực cạn kiệt và ăn thức ăn bạn mang theo để hồi sức.
- **Tự gắn Mồi & Phao:** Tự động nạp loại mồi và phao câu yêu thích của bạn.
- **Tính năng "Thấu thị":** Hiển thị trước cho bạn biết con cá nào hoặc kho báu gì đang cắn câu ngay cả khi chưa giật cần lên.
- **Tùy chỉnh độ khó:** Ép cá cắn câu ngay lập tức, bỏ qua minigame, ép ra cá kích thước lớn nhất, hoặc hack mồi và phao vô hạn.

---

## ⚙️ Hướng dẫn Cài đặt & Sử dụng

1. **Cài đặt** mod thông qua SMAPI. Bạn rất nên cài thêm [Generic Mod Config Menu (GMCM)](https://www.nexusmods.com/stardewvalley/mods/5098) để có thể chỉnh sửa cài đặt trực tiếp trong game.
2. **Vào game** và thức dậy trong nhà (FarmHouse hoặc Cabin).
3. **Ghi hình tuyến đường (Chỉ làm 1 lần):**
   - Đứng sát giường ngủ.
   - Bấm `F9` để bắt đầu ghi.
   - Chạy đến điểm câu cá (ví dụ: Bãi Biển).
   - Đến mép nước, bấm `F9` để dừng ghi.
4. **Bắt đầu câu cá:** Bấm phím `F5` để bật Bot. Từ giờ nó sẽ tự lo mọi thứ!
5. **Tự động đi ngủ:** Nhớ chỉnh phần `AutoPauseFishing` thành `WarnAndPause` trong menu cài đặt. Khi hết giờ, Bot sẽ tự chạy về nhà đi ngủ.

### 🎮 Các phím tắt mặc định
- `F5`: Bật/Tắt Bot câu cá tự động.
- `F6`: Bật/Tắt nhặt rương kho báu.
- `F9`: Bật/Tắt tính năng Ghi hình tuyến đường.
- `F10`: Mở bảng Cài đặt Dò đường (Bật/Tắt Grid, Xóa tuyến đường).

---

## 📜 Toàn bộ thông số cài đặt (Config)

Bạn có thể chỉnh sửa các thông số này qua file `config.json` hoặc bằng GMCM.

### Chung & Tự động
* `EnableAutomationButton` / `CatchTreasureButton` / `OpenConfigMenuButton`: Tùy chỉnh các phím tắt.
* `ModStatusPosition`: Vị trí hiển thị trạng thái của mod (`Left` - Trái hoặc `Right` - Phải).
* `AutoCastFishingRod` / `AutoHookFish` / `AutoPlayMiniGame`: Bật/tắt các tính năng tự quăng cần, tự giật cá và tự chơi minigame.
* `AutoClosePopup`: Tự động đóng bảng thông báo khi câu được cá.

### Quản lý Rác & Túi đồ
* `AutoLootTreasure`: Tự động nhặt đồ trong rương báu.
* `ActionIfInventoryFull`: Hành động khi túi đầy (`Stop` - Dừng lại, `Drop` - Vứt ra đất, `Discard` - Xóa sổ).
* `AutoTrashJunk`: Tự động vứt các món đồ rác rẻ tiền khi cần chỗ trống.
* `JunkHighestPrice`: Món đồ có giá bán từ mức này trở xuống sẽ bị coi là rác.
* `AllowTrashFish`: Cho phép vứt luôn cả cá nếu giá của nó quá rẻ.
* `JunkIgnoreList`: Danh sách mã các món đồ KHÔNG BAO GIỜ được vứt.

### Quản lý Ban đêm & Thể lực
* `AutoPauseFishing`: Xử lý khi trời tối (`Off` - Tắt, `WarnOnly` - Chỉ cảnh báo, `WarnAndPause` - Cảnh báo và ngưng câu). *Lưu ý: Bạn PHẢI chọn WarnAndPause để Bot tự động chạy về giường!*
* `PauseFishingTime`: Thời điểm Bot ngưng câu cá (ví dụ: `24` là 12:00 Đêm).
* `AutoEatFood`: Tự động ăn thức ăn khi mệt.
* `EnergyPercentToEat`: Mức phần trăm thể lực (VD: 5%) để kích hoạt việc ăn uống.
* `AllowEatingFish`: Cho phép Bot ăn cá sống vừa câu được nếu không có thức ăn nào khác.

### Mồi, Phao & Cần câu
* `AutoAttachBait` / `AutoAttachTackles`: Tự động gắn mồi/phao câu.
* `PreferBait` / `PreferTackle` / `PreferAdvIridiumTackle`: Cài đặt loại mồi/phao cụ thể muốn dùng.
* `SpawnBaitIfDontHave` / `SpawnTackleIfDontHave`: Hack tự động đẻ ra mồi/phao nếu bạn xài hết.
* `InfiniteBait` / `InfiniteTackle`: Dùng mồi và phao vĩnh viễn không bao giờ hỏng/hết.
* `StartWithFishingRod`: Tự động cho bạn một cây cần câu tùy chọn vào Ngày 1.

### Hack & Tùy chỉnh độ khó
* `SkipFishingMiniGame`: Câu được cá ngay lập tức mà không cần chơi minigame.
* `InstantFishBite`: Cá cắn câu ngay khoảnh khắc phao vừa chạm mặt nước.
* `PreferFishAmount` / `PreferFishQuality`: Ép câu được nhiều cá cùng lúc hoặc cá chất lượng cao hơn.
* `AlwaysPerfect` / `AlwaysMaxFishSize`: Luôn luôn câu hoàn hảo "Perfect" và kích thước cá to nhất.
* `FishDifficultyMultiplier` / `FishDifficultyAdditive`: Chỉnh sửa độ khó thực tế của minigame.
* `InstantCatchTreasure` / `TreasureChance` / `GoldenTreasureChance`: Ép tỷ lệ ra rương kho báu.

### Giao diện hiển thị
* `DisplayFishPreview` / `ShowFishName` / `ShowTreasure`: Bật/tắt giao diện soi cá/rương.
* `ShowUncaughtFishSpecies`: Cho phép xem trước cả những loài cá mà bạn chưa từng câu được.
* `AlwaysShowLegendaryFish`: Luôn hiển thị giao diện báo hiệu nếu con cá đó là Cá Huyền Thoại.

### Tự động Cường hóa (Enchantments)
* `AddAutoHookEnchantment` / `AddEfficientEnchantment` / `AddMasterEnchantment` / `AddPreservingEnchantment`: Tự động buff các thuộc tính cường hóa này lên cần câu của bạn.

---

## 🙏 Lời cảm ơn
Gửi lời cảm ơn chân thành đến tác giả gốc của Fishing Assistant 2 và cộng đồng mod Stardew Valley. Phiên bản này được xây dựng trên nền tảng tuyệt vời của họ để mang đến những tính năng dò đường thông minh và thân thiện nhất cho người chơi!
