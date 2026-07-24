AutoFishing_Bot là một bản mod hỗ trợ câu cá dành cho Stardew Valley, được tuỳ biến và phát triển lại từ nền tảng của bản mod Fishing Assistant 2 ban đầu. Mục tiêu của bản mod là loại bỏ hoàn toàn các tính năng gian lận (cheat) để giữ lại trải nghiệm công bằng, đồng thời bổ sung thêm tính năng di chuyển tự động (Auto-Navigation) giúp nhân vật tự động tìm đường đi câu và về ngủ.

- Tự động di chuyển (Auto-Navigation): Tích hợp thuật toán tìm đường (A*) để nhân vật có thể tự động tránh vật cản và di chuyển qua lại giữa các bản đồ (ví dụ: từ nhà FarmHouse ra Bãi biển).
- Ghi nhớ tuyến đường: Đứng cạnh giường ngủ, bấm phím F9 để bắt đầu ghi hình. Đi bộ đến điểm câu cá, bấm phím F9 một lần nữa để lưu lại tuyến đường.
- Tự động về ngủ: Khi đến "Giờ nghỉ ngơi" do bạn thiết lập, nhân vật sẽ ngừng câu cá và tự động đi ngược lại tuyến đường đã lưu để về nhà ngủ. Sáng hôm sau, nhân vật sẽ tự động thức dậy và quay lại điểm câu cá.
- Menu tùy chỉnh lưới (Grid Editor): Bấm phím F10 để hiển thị lưới bản đồ, giúp bạn xem được những ô nào nhân vật có thể đi qua và những ô nào bị chặn.
- Tự động câu cá: Tự động quăng cần, giật cá và hoàn thành minigame câu cá một cách cơ bản, không có tính năng cheat.
- Quản lý đồ đạc: Tự động nhận rương kho báu, tự động nạp mồi và phao câu. Cho phép tùy chọn vứt bỏ các vật phẩm rẻ tiền khi túi đồ bị đầy.
- Tự động ăn: Tự động ăn thức ăn bạn mang theo khi thể lực xuống thấp.

## Hướng dẫn cài đặt & Biên dịch (Dành cho tất cả)
Kho lưu trữ này chỉ cung cấp mã nguồn gốc của bản mod. Để sử dụng, bạn bắt buộc phải tự biên dịch (build) mã nguồn thành bản mod hoàn chỉnh theo các bước sau:
1. Đảm bảo bạn đã cài đặt [SMAPI](https://smapi.io/) để chơi mod Stardew Valley.
2. Cài đặt .NET 6 SDK (hoặc mới hơn) trên máy tính của bạn.
3. Tải mã nguồn này về máy bằng cách bấm nút Code -> Download ZIP hoặc dùng lệnh:
```bash
git clone https://github.com/thienwu/AutoFishing_Bot.git
```
4. Mở thư mục mã nguồn vừa tải về bằng File Explorer (thư mục chứa file `FishingAssistant2.csproj`).
Nhấp chuột vào thanh địa chỉ (Address bar) ở trên cùng của cửa sổ thư mục, gõ `cmd` hoặc `powershell` rồi bấm Enter.
Một cửa sổ gõ lệnh sẽ hiện ra ngay tại đúng thư mục đó. Bạn hãy chạy lệnh:
```bash
dotnet build
```
5. Bản mod sau khi build thành công sẽ tạo ra một thư mục mang tên `FishingAssistant2` nằm tại đường dẫn: `bin/Debug/net6.0/FishingAssistant2`.
Bạn hãy copy nguyên thư mục `FishingAssistant2` này và dán vào thư mục Mods của game Stardew Valley (đường dẫn thường gặp trên Windows là: `C:\Program Files (x86)\Steam\steamapps\common\Stardew Valley\Mods`).
6. (Tuỳ chọn) Bạn có thể điều chỉnh các phím tắt, giờ đi ngủ và cấu hình tính năng trong game thông qua [Generic Mod Config Menu](https://www.nexusmods.com/stardewvalley/mods/5098).

## Nguồn gốc
Dự án này được xây dựng và phát triển dựa trên mã nguồn của bản mod Fishing Assistant 2. Các tính năng gian lận (cheat) mang tính mất cân bằng như hack mồi/phao vô hạn, câu cá không cần chờ, tự động max size, v.v... đã được loại bỏ hoàn toàn để đảm bảo tính công bằng. Thêm vào đó, tính năng dò đường và tự động hóa chu kỳ câu cá-về ngủ được viết mới hoàn toàn.
Chúc bạn có những chuyến đi câu nhàn rỗi và hiệu quả tại Stardew Valley!
