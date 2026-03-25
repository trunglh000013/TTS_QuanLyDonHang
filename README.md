# ProductTest

`ProductTest` là một dự án ASP.NET Core Web API dùng để quản lý sản phẩm. Dự án được tổ chức theo kiến trúc nhiều tầng (`Presentation`, `Application`, `Infrastructure`, `Domain`), sử dụng `MediatR` để tách command/query, `Entity Framework Core` để làm việc với SQL Server, `Serilog` để ghi log và hỗ trợ `localization` cho tiếng Anh và tiếng Việt.

## Mục tiêu chính

- Quản lý danh sách sản phẩm bằng API.
- Hỗ trợ CRUD, tìm kiếm, lọc và phân trang.
- Hỗ trợ thông điệp đa ngôn ngữ (`en-US`, `vi-VN`).
- Ghi log request và log ứng dụng để theo dõi vận hành.

## Công nghệ sử dụng

- `.NET 10`
- `ASP.NET Core Web API`
- `Entity Framework Core`
- `SQL Server`
- `MediatR`
- `Swagger / OpenAPI`
- `Serilog`

## Cấu trúc dự án

```text
ProductTest/
|-- ProductTest.Presentation/   # API, controller, cấu hình app, resource localization
|-- ProductTest.Application/    # Use cases, DTOs, commands, queries, abstractions
|-- ProductTest.Infrastructure/ # EF Core, repository, DbContext, khởi tạo dữ liệu
|-- ProductTest.Domain/         # Entity domain
```

## Tính năng hiện có

- Tạo sản phẩm mới
- Lấy danh sách sản phẩm có phân trang
- Lấy chi tiết sản phẩm theo `Id`
- Cập nhật sản phẩm
- Xóa sản phẩm
- Tìm kiếm sản phẩm theo từ khóa
- Lọc sản phẩm theo danh mục, khoảng giá, trạng thái hoạt động
- Trả thông điệp theo ngôn ngữ thông qua query string hoặc header `Accept-Language`

## Mô hình dữ liệu sản phẩm

Mỗi sản phẩm gồm các trường chính:

- `Id`
- `Name`
- `Description`
- `Category`
- `Price`
- `Stock`
- `IsActive`
- `CreatedAtUtc`
- `UpdatedAtUtc`

## Yêu cầu môi trường

Để chạy dự án, cần chuẩn bị:

- Cài `NET 10 SDK`
- Cài `SQL Server`
- Có một SQL Server đang chạy và cho phép kết nối theo connection string trong `ProductTest.Presentation/appsettings.json`

Connection string mặc định:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost,1433;Database=ProductTestDb;User Id=sa;Password=sa;TrustServerCertificate=True;MultipleActiveResultSets=true"
}
```

Nếu môi trường của bạn khác cấu hình trên, hãy chỉnh lại giá trị `DefaultConnection` trong:

- `ProductTest.Presentation/appsettings.json`
- `ProductTest.Presentation/appsettings.Development.json`

## Cách chạy dự án

Từ thư mục gốc của dự án, chạy:

```powershell
dotnet restore .\ProductTest.Presentation\ProductTest.Presentation.csproj
dotnet run --project .\ProductTest.Presentation\ProductTest.Presentation.csproj
```

Ứng dụng mặc định chạy ở:

- `http://localhost:5145`
- `https://localhost:7240`

Swagger UI:

- [http://localhost:5145/swagger](http://localhost:5145/swagger)
- [https://localhost:7240/swagger](https://localhost:7240/swagger)

Khi ứng dụng khởi động:

- Cơ sở dữ liệu sẽ được tạo nếu chưa tồn tại bằng `EnsureCreated`
- Hệ thống sẽ seed sẵn một số dữ liệu mẫu nếu bảng `Products` chưa có dữ liệu

## Dữ liệu mẫu khởi tạo

Ứng dụng hiện seed sẵn 2 sản phẩm mẫu:

- `Laptop Pro 14`
- `Office Chair Comfort`

## Danh sách API chính

Base route:

```text
/api/product
```

Các endpoint hiện có:

- `GET /api/product/localization`: kiểm tra thông điệp localization theo culture hiện tại
- `POST /api/product`: tạo sản phẩm
- `GET /api/product?pageNumber=1&pageSize=10`: lấy danh sách sản phẩm
- `GET /api/product/{id}`: lấy chi tiết sản phẩm
- `PUT /api/product/{id}`: cập nhật sản phẩm
- `DELETE /api/product/{id}`: xóa sản phẩm
- `GET /api/product/search?searchTerm=laptop&pageNumber=1&pageSize=5`: tìm kiếm sản phẩm
- `GET /api/product/filter?category=Electronics&minPrice=100&maxPrice=2000&isActive=true&pageNumber=1&pageSize=5`: lọc sản phẩm

## Ví dụ request tạo sản phẩm

```json
{
  "name": "Mechanical Keyboard",
  "description": "Compact keyboard for developers",
  "category": "Electronics",
  "price": 99.9,
  "stock": 20,
  "isActive": true
}
```

## Phân trang

Các API danh sách sử dụng 2 query parameter:

- `pageNumber`: số trang, mặc định `1`
- `pageSize`: số phần tử trên trang, mặc định `10`

Giới hạn hiện tại:

- `pageNumber < 1` sẽ được chuẩn hóa về `1`
- `pageSize < 1` sẽ được chuẩn hóa về `10`
- `pageSize > 100` sẽ được giới hạn về `100`

## Localization

Dự án hỗ trợ hai ngôn ngữ:

- `en-US`
- `vi-VN`

Có thể đổi ngôn ngữ theo 2 cách:

1. Query string:

```text
?culture=vi-VN&ui-culture=vi-VN
```

1. Header:

```http
Accept-Language: vi-VN
```

Ví dụ:

```text
GET /api/product/localization?culture=vi-VN&ui-culture=vi-VN
```

Resource localization được đặt trong thư mục:

- `ProductTest.Presentation/Resources`

## Logging

Dự án dùng `Serilog` để ghi log:

- Log ra console
- Log ra file theo ngày

File log mặc định:

```text
ProductTest.Presentation/Logs/product-test.log
```

Ngoài ra, ứng dụng còn ghi log cho từng request với thông tin như:

- HTTP method
- Request path
- Query string
- Status code
- Thời gian xử lý

## Test nhanh API

Bạn có thể test nhanh bằng một trong các cách sau:

- Mở `Swagger UI`
- Dùng file `ProductTest.Presentation/ProductTest.Presentation.http`
- Dùng Postman hoặc curl

## Ghi chú triển khai

- Dự án hiện dùng `EnsureCreated`, phù hợp cho môi trường demo hoặc bài test nhanh.
- Nếu phát triển dài hạn, nên chuyển sang dùng `EF Core Migrations`.
- Connection string đang để trực tiếp trong file cấu hình; khi triển khai thực tế nên chuyển sang `User Secrets`, biến môi trường hoặc hệ thống secret manager.

## Hướng phát triển thêm

- Bổ sung validation cho request
- Thêm unit test và integration test
- Chuẩn hóa error response
- Áp dụng migrations và pipeline CI/CD
- Thêm authentication/authorization nếu cần bảo vệ API
