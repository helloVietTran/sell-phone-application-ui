# SellPhoneApplication

Ứng dụng bán điện thoại được phát triển bằng .NET MAUI và C#, tập trung vào giao diện UI hiện đại, trải nghiệm responsive trên nhiều nền tảng (Android, iOS, Windows, Mac Catalyst, Tizen).

## Nội dung đã thực hiện

- Tích hợp UI với cấu trúc màn hình rõ ràng: `Views`, `ViewModels`, `Shared`.
- Thiết kế responsive để giao diện tự điều chỉnh trên các kích thước màn hình khác nhau.
- Xử lý logic ứng dụng bằng `ViewModels` và `Services`.
- Quản lý dữ liệu sản phẩm, giỏ hàng, yêu thích, đơn hàng, đánh giá.

## Công nghệ sử dụng

- .NET MAUI
- C#
- XAML
- MVVM

## Cấu trúc thư mục chính

- `Views/`: màn hình và trang giao diện.
- `ViewModels/`: logic điều khiển dữ liệu và sự kiện UI.
- `Services/`: dịch vụ gọi API và quản lý dữ liệu.
- `Models/`: định nghĩa mô hình dữ liệu.
- `Shared/`: thành phần dùng chung như header, footer, popup.
- `Platforms/`: cấu hình nền tảng cụ thể.
- `Resources/`: font, ảnh, biểu tượng, style.

## Hướng dẫn chạy dự án

1. Mở `SellPhoneApplication.sln` bằng Visual Studio.
2. Chọn cấu hình `Debug` và nền tảng mong muốn.
3. Khởi chạy ứng dụng bằng nút `Run` hoặc `F5`.

> Lưu ý: cần cài đặt môi trường .NET MAUI và SDK tương ứng để chạy trên thiết bị hoặc trình giả lập.
