📦 Dự án API Cá Nhân - ASP.NET Core 8.0
🛠️ Công nghệ & Kiến trúc sử dụng
ASP.NET Core 8.0
JWT Authentication
AutoMapper
Repository Pattern
Service Layer
Custom Middleware
Unit Test: XUnit

Clean Architecture
API
Service
Repository
DTO
Enum
Helper
TestCannabis

🚀 Mục tiêu dự án
Xây dựng một kiến trúc chuẩn cho các ứng dụng ASP.NET Core hiện đại:
Dễ mở rộng
Dễ viết unit test
Có khả năng tái sử dụng cao
Tuân thủ nguyên tắc SOLID & Clean Code

🔐 Xác thực người dùng
Sử dụng JWT Bearer Token.
Token được kiểm tra qua Middleware tùy chỉnh.
Áp dụng cho các route cần xác thực.

⚙️ AutoMapper
Cấu hình tại project MyProject.Mapping
Mapping giữa DTOs và Entities
Đăng ký AutoMapper tại Program.cs

🧩 Repository & Service Layer
Repository Pattern: Tách biệt logic truy cập dữ liệu khỏi nghiệp vụ.
Service Layer: Xử lý các nghiệp vụ phức tạp, dễ dàng test và tái sử dụng.
Hỗ trợ Dependency Injection toàn diện.

🧪 Hướng dẫn chạy thử local
Bước 1: Cài đặt môi trường
.NET 8.0 SDK
Visual Studio 2022+ hoặc VS Code

Bước 2: Cấu hình chuỗi kết nối
Sửa appsettings.Development.json:
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=...;User Id=...;Password=..."
}
Bước 3: Migration & Database
Chạy lệnh trong Package Manager Console (PMC):
Add-Migration InitialCreate -Project DAL -Startup-Project Cannabis.Server
Update-Database -Project DAL -Startup-Project Cannabis.Server

Bước 4: Chạy API
Mở project Cannabis.Server → Run

🧪 Test bổ sung
Dự án có cấu trúc thư mục ApiCannabisTest
Sử dụng XUnit cho Unit Test và Integration Test
Có thể mock Service, Repository để đảm bảo test độc lập
Test middleware, controller, service bằng dependency injection

🧰 Ghi chú thêm
Hỗ trợ mở rộng: Swagger, Serilog, Health Checks,...
Có thể triển khai CI/CD nếu cần
Phù hợp làm nền tảng phát triển các ứng dụng API quy mô vừa và lớn

📬 Liên hệ
Mọi góp ý, thảo luận hoặc hợp tác xin vui lòng gửi về:
📧 vuvietanhsp@gmail.com

