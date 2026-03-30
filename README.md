# 🎬 Film Diary API

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-blue.svg)
![Entity Framework Core](https://img.shields.io/badge/EF_Core-green)
![SQL Server](https://img.shields.io/badge/SQL_Server-red)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D)

ASP.NET Core Web API ile geliştirilmiş kapsamlı ve profesyonel bir film takip/koleksiyon yönetimi uygulamasıdır. Kullanıcıların film verilerini yönetmelerini (CRUD) sağlarken, aynı zamanda CSV dosyaları üzerinden geniş veri setlerini sisteme toplu olarak entegre etme yeteneğine sahiptir.

## 🌟 Özellikler

- **RESTful API Tasarımı:** Standartlara uygun, anlaşılır ve esnek endpoint mimarisi.
- **Kapsamlı CRUD İşlemleri:** Filmleri ekleme, okuma, güncelleme ve silme imkanı.
- **Toplu Veri İçe Aktarımı (CSV Import):** `CsvHelper` kütüphanesi kullanılarak dış veri kaynaklarından (CSV) hızlı ve güvenilir veri aktarımı.
- **DTO (Data Transfer Object) ve Validasyon:** Veri güvenliğini sağlamak ve istemci-sunucu arasındaki iletişimi optimize etmek için DTO kullanımı ve katı veri doğrulama kuralları.
- **Canlı API Dokümantasyonu:** Swagger entegrasyonu ile anlık olarak test edilebilir ve okunabilir API belgeleri.

## 🚀 Kullanılan Teknolojiler

- **Backend Çatısı:** ASP.NET Core Web API
- **ORM / Veri Erişimi:** Entity Framework Core
- **Veritabanı:** SQL Server
- **Veri İşleme:** CsvHelper (CSV okuma/yazma operasyonları için)
- **Dokümantasyon:** Swagger / OpenAPI

## 🛠️ Kurulum ve Çalıştırma Adımları

Projeyi kendi bilgisayarınızda çalıştırmak için aşağıdaki adımları sırasıyla uygulayabilirsiniz:

### Ön Koşullar
- [.NET SDK](https://dotnet.microsoft.com/download) (Uyumlu sürüm)
- SQL Server (LocalDB veya tam sürüm)

### Kurulum

1. **Projeyi Klonlayın:**
   ```bash
   git clone https://github.com/kullanici-adiniz/film-diary-api.git
   cd film-diary-api
   ```

2. **Bağlantı Dizesini (Connection String) Ayarlayın:**
   `appsettings.json` dosyası içerisindeki `DefaultConnection` değerini, kendi yerel SQL Server veritabanı bilgilerinize göre güncelleyin.

3. **Veritabanını Oluşturun (Migration):**
   Terminal veya komut satırında proje dizinindeyken aşağıdaki komutu çalıştırarak Entity Framework üzerinden tabloları oluşturun:
   ```bash
   dotnet ef database update
   ```

4. **Projeyi Başlatın:**
   ```bash
   dotnet run
   ```

5. **API'yi Test Edin (Swagger):**
   Proje çalıştıktan sonra, ekranda beliren adrese (örneğin `https://localhost:5001/swagger`) tarayıcınızdan giderek tüm API metotlarını listeleyebilir ve test edebilirsiniz.

## 💡 CSV İmport İşlemi Hakkında İpucu

Toplu film eklemek için kullanacağınız dataset içerisindeki sütun başlıklarının, API tarafında beklenen DTO yapısıyla eşleştiğinden emin olun. Örnek bir CSV yapısı ve API endpoint detayları için Swagger ara yüzündeki dökümantasyonu inceleyebilirsiniz.

---
Merve AKDENİZ
