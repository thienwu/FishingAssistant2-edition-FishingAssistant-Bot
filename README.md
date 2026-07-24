# 🎣 Fishing Assistant Bot 

![FishingAssistant2](Image/FishingAssistant2.gif)

Một bản mod hỗ trợ câu cá dành cho `Stardew Valley`, được phát triển dựa trên mã nguồn của mod Fishing Assistant 2. Bản mod này được tối giản hóa, **loại bỏ hoàn toàn các tính năng gian lận (cheat)** của bản gốc, đồng thời bổ sung thêm tính năng di chuyển tự động (Auto-Navigation), giúp nhân vật tự động đi đến điểm câu cá và tự quay về nhà khi đến giờ ngủ.

---

## 🌟 Tính năng chính

### 🤖 Tự động di chuyển (Auto-Navigation)
Mod tích hợp thuật toán tìm đường (A*) để nhân vật có thể tự động tránh vật cản và di chuyển qua lại giữa các bản đồ (ví dụ: từ nhà FarmHouse ra Bãi biển).

### 🗺️ Ghi nhớ tuyến đường
Bạn có thể hướng dẫn cho nhân vật cách đi đến các điểm câu cá:
- Đứng cạnh giường ngủ và bấm phím `F9` để bắt đầu ghi hình.
- Đi bộ đến điểm câu cá bạn muốn.
- Bấm phím `F9` một lần nữa để lưu lại tuyến đường (tuyến đường này sẽ có tên là `AutoFishing`).

### 🛏️ Tự động về ngủ
- Khi thời gian trong game đến mốc "Giờ nghỉ ngơi" do bạn cài đặt (ví dụ: 1:00 AM), nhân vật sẽ ngừng câu cá.
- Nhân vật sẽ tự động đi ngược lại tuyến đường đã lưu để về nhà và tự đi ngủ.
- Sáng hôm sau, nhân vật sẽ tự động thức dậy và quay lại điểm câu cá để tiếp tục.

### 🛠️ Menu tùy chỉnh lưới (Grid Editor)
Bạn có thể bấm phím `F10` để mở menu NavTool. Chức năng này hiển thị các ô trên bản đồ, giúp bạn xem được những ô nào nhân vật có thể đi qua và những ô nào bị chặn.

---

## 🐟 Các tính năng câu cá tự động

Bản mod này chỉ giữ lại các tính năng tự động hóa cơ bản và hợp lý nhất, **không có tính năng cheat**:

- **Tự động câu cá:** Tự động quăng cần, giật cá và hoàn thành minigame câu cá như một người chơi bình thường.
- **Quản lý đồ đạc:** Tự động nhận đồ từ rương kho báu. Cho phép tùy chọn vứt bỏ hoặc xóa bớt các vật phẩm rẻ tiền khi túi đồ bị đầy.
- **Tự động ăn:** Tự động ăn thức ăn bạn mang theo khi thể lực xuống thấp.
- **Gắn mồi & phao:** Tự động gắn loại mồi và phao câu bạn đã cài đặt sẵn (nếu bạn có trong túi đồ).

---

## ⚙️ Hướng dẫn biên dịch & sử dụng (Dành cho Mã nguồn)

Vì kho lưu trữ này chứa **mã nguồn (source code)** chưa được biên dịch sẵn, bạn cần tự build mod trước khi đưa vào game.

### 1. Tải và Biên dịch (Build) Mod từ mã nguồn
- Đảm bảo bạn đã cài đặt [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).
- Bạn có thể tải mã nguồn bằng cách bấm nút **Code -> Download ZIP** và giải nén, hoặc dùng lệnh Git:
  ```bash
  git clone https://github.com/thienwu/FishingAssistant2-edition-FishingAssistant-Bot.git
  ```
- **Quan trọng:** Hãy mở thư mục bạn vừa tải về (thư mục có chứa file `FishingAssistant2.csproj`). Tại thư mục đó, nhấp chuột phải chọn "Open in Terminal" (hoặc mở Command Prompt và dùng lệnh `cd` trỏ chính xác đến đường dẫn của thư mục này).
- Chạy lệnh sau để tiến hành biên dịch:
  ```bash
  dotnet build
  ```
- Sau khi build thành công, trình biên dịch sẽ tự động copy mod vào thư mục `Mods` của Stardew Valley nếu bạn đã cài đặt SMAPI chuẩn. Khuyến khích cài thêm mod [Generic Mod Config Menu (GMCM)](https://www.nexusmods.com/stardewvalley/mods/5098) để dễ dàng cấu hình.

### 2. Hướng dẫn sử dụng trong game
- **Vào game** và thức dậy trong nhà (FarmHouse hoặc Cabin).
- **Ghi hình tuyến đường (Làm 1 lần):**
  - Đứng cạnh giường ngủ, bấm `F9` để bắt đầu.
  - Đi bộ đến mép nước tại điểm câu cá, bấm `F9` để lưu lại.
- **Bắt đầu câu cá:** Bấm phím `F5` để bật chế độ tự động.
- **Tự động đi ngủ:** Trong menu cài đặt của GMCM, hãy chọn dòng `AutoPauseFishing` là `WarnAndPause`. Khi đến giờ, nhân vật sẽ tự động ngừng câu và chạy về nhà ngủ.

### 🎮 Phím tắt mặc định
- `F5`: Bật/Tắt chế độ câu cá tự động.
- `F6`: Bật/Tắt tính năng nhặt kho báu.
- `F9`: Bật/Tắt ghi hình tuyến đường.
- `F10`: Mở bảng Menu đường đi (Bật lưới, quản lý đường đi).

---

## 📜 Chi tiết các thông số cài đặt (Config)

Bạn có thể thay đổi các tùy chọn này trong file `config.json` hoặc thông qua menu của GMCM. (Lưu ý: Tất cả các cài đặt cheat đều đã bị loại bỏ khỏi phiên bản này).

### Chung & Tự động
* `EnableAutomationButton` / `CatchTreasureButton` / `OpenConfigMenuButton`: Đổi các phím tắt.
* `ModStatusPosition`: Chuyển vị trí hiển thị trạng thái mod (`Left` - Trái hoặc `Right` - Phải).
* `AutoCastFishingRod` / `AutoHookFish` / `AutoPlayMiniGame`: Các tùy chọn tự động quăng cần, giật cá và chơi minigame.
* `AutoClosePopup`: Tự động đóng bảng thông báo sau khi câu xong.

### Quản lý túi đồ
* `AutoLootTreasure`: Tự động lấy đồ trong rương.
* `ActionIfInventoryFull`: Chọn hành động khi túi đầy (`Stop` - Dừng lại, `Drop` - Vứt ra ngoài, `Discard` - Xóa đi).
* `AutoTrashJunk`: Tự động vứt các đồ vật rẻ tiền khi cần chỗ trống.
* `JunkHighestPrice`: Mức giá tối đa để một món đồ bị coi là đồ bỏ đi.
* `AllowTrashFish`: Cho phép vứt luôn cá nếu giá trị thấp.
* `JunkIgnoreList`: Danh sách mã các món đồ bạn không muốn bị vứt đi.

### Năng lượng & Thời gian
* `AutoPauseFishing`: Hành động khi trời khuya (`Off` - Tắt, `WarnOnly` - Cảnh báo, `WarnAndPause` - Cảnh báo và dừng). *Hãy chọn WarnAndPause nếu muốn nhân vật tự động về nhà.*
* `PauseFishingTime`: Thời điểm ngừng câu (Ví dụ: `24` tương đương 12:00 đêm).
* `AutoEatFood`: Bật tính năng tự động ăn.
* `EnergyPercentToEat`: Mức thể lực (phần trăm) để bắt đầu ăn.
* `AllowEatingFish`: Cho phép ăn cá sống nếu không có thức ăn nào khác.

### Mồi, Phao & Cần câu
* `AutoAttachBait` / `AutoAttachTackles`: Tự động nạp mồi và phao từ túi đồ của bạn.
* `PreferredBait` / `PreferredTackle` / `PreferredAdvIridiumTackle`: Chọn loại mồi/phao ưu tiên sử dụng.
* `StartWithFishingRod`: Tặng một cây cần câu tuỳ chọn vào Ngày 1 (chỉ áp dụng nếu khởi tạo nhân vật mới).

---

## 🙏 Lời cảm ơn
Xin chân thành cảm ơn tác giả gốc của Fishing Assistant 2 và cộng đồng mod Stardew Valley đã tạo ra một nền tảng tuyệt vời. 

Cần lưu ý rằng mã nguồn của mod Fishing Assistant 2 gốc chứa rất nhiều tính năng mang tính chất gian lận (cheat) như: hack mồi và phao vô hạn, cá cắn câu ngay lập tức, bỏ qua minigame luôn câu được cá lớn nhất, tự động nhận phù phép vũ khí, hiển thị trước loại cá... Để đảm bảo một trải nghiệm công bằng và tự động hóa thuần túy, **tất cả những tính năng cheat này đã được loại bỏ hoàn toàn trong dự án này**. 

Phiên bản này được xây dựng lại dựa trên mã nguồn gốc với mục đích duy nhất: giữ lại sự tự động hoá cơ bản và tích hợp thêm khả năng di chuyển (Navigation/Bot) thông minh cho nhân vật.
