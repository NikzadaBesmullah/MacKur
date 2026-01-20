# ⚽ Halı Saha Randevu Sistemi

Modern, hızlı ve kullanıcı dostu bir halı saha rezervasyon ve yönetim platformu. Bu proje, futbolseverlerin kolayca saha bulmasını ve işletme sahiplerinin randevularını profesyonelce yönetmesini sağlar.

---

## 🔥 Temel Özellikler

### 👤 Kullanıcılar İçin
- **Gelişmiş Arama:** Şehir bazlı halı saha arama ve filtreleme.
- **Harita Entegrasyonu:** Sahaların tam konumunu harita üzerinde görüntüleme.
- **Detaylı İnceleme:** Saha fotoğrafları, puanlar ve kullanıcı yorumları.
- **Favorilerim:** Beğendiğiniz sahaları listenize ekleyerek hızlı erişim.
- **Hızlı Randevu:** Saniyeler içinde randevu oluşturma ve ödeme bildirimi.

### 🏟️ Saha Sahipleri İçin
- **İstatistik Paneli:** Günlük, haftalık ve toplam kazanç takibi.
- **Saha Yönetimi:** Saha bilgilerini, fiyatları ve fotoğrafları güncelleme.
- **Randevu Yönetimi:** Gelen randevu taleplerini onaylama veya reddetme.
- **Finansal Bilgi:** IBAN ve ödeme bilgilerini yönetme.

### 🛡️ Admin Paneli
- **Onay Sistemi:** Yeni kayıt edilen halı sahaların kalite kontrolü ve onayı.
- **Kullanıcı Yönetimi:** Tüm kullanıcıların ve rollerin yönetimi.
- **Veri Yönetimi:** Şehir listeleri ve sistem genelindeki verilerin kontrolü.

---

## 🚀 Kullanılan Teknolojiler

- **Backend:** .NET 8 / ASP.NET Core MVC
- **Database:** Entity Framework Core & SQLite
- **Security:** Microsoft Identity (Rol Tabanlı Yetkilendirme)
- **Frontend:** HTML5, CSS3 (Vanilla CSS), JavaScript, Bootstrap
- **Design:** Modern UI/UX, Glassmorphism, Responsive Design
- **Tools:** Leaflet.js (Harita için), FontAwesome / Bootstrap Icons

---

## 🛠️ Kurulum

Projeyi yerel makinenizde çalıştırmak için aşağıdaki adımları izleyebilirsiniz:

1. **Repoyu Klonlayın:**
   ```bash
   git clone https://github.com/kullaniciadi/haliSahaRandevu.git
   cd haliSahaRandevu
   ```

2. **Bağımlılıkları Yükleyin:**
   ```bash
   dotnet restore
   ```

3. **Veritabanını Güncelleyin:**
   ```bash
   dotnet ef database update
   ```

4. **Projeyi Çalıştırın:**
   ```bash
   dotnet run
   ```

---

## 📸 Ekran Görüntüleri

| Ana Sayfa | Saha Detayları | Yönetim Paneli |
| :---: | :---: | :---: |
| ![Ana Sayfa](https://via.placeholder.com/400x250?text=Ana+Sayfa) | ![Detaylar](https://via.placeholder.com/400x250?text=Saha+Detayları) | ![Panel](https://via.placeholder.com/400x250?text=İstatistikler) |

---

## 📁 Proje Yapısı

- `Controllers/`: İş mantığının yönetildiği kontrolcüler.
- `Models/`: Veritabanı tabloları ve veri modelleri.
- `Views/`: Kullanıcı arayüzü dosyaları (Razor Pages).
- `Data/`: `DbContext` ve veritabanı tohumlama (`SeedData`) sınıfları.
- `wwwroot/`: CSS, JS ve resim gibi statik dosyalar.

---

## 🤝 Katkıda Bulunma

1. Bu projeyi fork edin.
2. Yeni bir branch oluşturun (`git checkout -b feature/yenilik`).
3. Değişikliklerinizi yapın ve commit atın (`git commit -m 'Yeni özellik eklendi'`).
4. Branch'inizi push edin (`git push origin feature/yenilik`).
5. Bir Pull Request oluşturun.

---

## 📄 Lisans
Bu proje **MIT Lisansı** ile lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına bakabilirsiniz.

---

**Geliştirici:** [Besmullah](https://github.com/besmullah)  
**Tarih:** 2026
