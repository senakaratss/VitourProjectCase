# Vitour Project Case

## 🔹 Proje Hakkında
Vitour Project Case, seyahat ve tur rezervasyonları için geliştirilmiş modern ve kullanıcı dostu bir web uygulamasıdır. Proje, **MongoDB tabanlı veritabanı** ile dinamik içerik yönetimi sunmakta ve hem kullanıcı hem admin tarafında tamamen işlevsel bir sistem sağlamaktadır.

**Projenin temel amacı:**
- Kullanıcıların tur listesini görebilmesi ve rezervasyon yapabilmesi  
- Admin panel üzerinden içerik yönetimi ve raporlama yapılabilmesi  
- Projede **çoklu dil desteği** sağlamak

---

## 🔹 Kullanıcı Arayüzü (UI)
- **Tur Listeleme ve Sayfalama:**  
  Tüm tur verileri, kategoriler ve lokasyon bilgileri MongoDB’den dinamik olarak çekilir. Listeleme sayfasında **her sayfada 6 tur** gösterilir ve sayfalama desteklenir.

- **Tur Detayları:** Tur sayfası **5 sekme içerir** ve her sekme dinamik olarak MongoDB verilerinden yüklenir:  
  1. **Açıklama:** Turun genel bilgileri, kapasitesi, süresi ve fiyat detayları gösterilir.  
  2. **Tur Planı:** Gün gün tur programı (Day 1, Day 2…) detaylı metinlerle listelenir; **TourId ilişkisi** ile doğru tur verisi çekilir.  
  3. **Rota Konumu (AI Harita Görseli):** Google Harita kullanılmaz; yerine **Gemini/AI ile oluşturulmuş Pixar tarzı statik harita görselleri** sunulur.  
  4. **Yorumlar:** Kullanıcılar rehber, konaklama, ulaşım ve fiyat gibi parametreler üzerinden **5 yıldız sistemi** ile değerlendirme yapabilir.  
  5. **Galeri:** Sadece ilgili tura ait fotoğraflar gösterilir.

- **Rezervasyon Akışı ve Modern UI:**  
  Rezervasyon sayfası tamamen **Claude AI ile tasarlanmış modern bir arayüz** sunar. Rezervasyon sırasında turun mevcut kapasitesi kontrol edilir ve kontenjan doluysa işlem engellenir.  Rezervasyon tamamlandığında kullanıcıya **otomatik bilgilendirme maili** gönderilir.

---

## 🔹 Admin Paneli
- **Panel Tasarımı:** Sıfırdan AI desteği ile tasarlanmış, modern ve kullanıcı dostu bir arayüz.  
- **CRUD İşlemleri:** Turlar, Kategoriler, Tur Planları, Yorumlar, Galeri ve Rezervasyonlar koleksiyonları için tam yetkili yönetim ekranları mevcuttur.  
- **Raporlama:** Her entity için **Excel/PDF indirme** desteği vardır. Ayrıca, aylık istatistikler sayfası hazırlanmış ve aylık hedeflere ulaşım veya geçen aya göre yüzdeler görüntülenir.

---

## 🔹 Teknik Altyapı ve Standartlar
- **Modüler Mimari:** `Services`, `Entities` ve `DTOs` klasörleri ile **interface segregasyon prensibine uygun** yapı.  
- **Çoklu Dil (Localization):** Sistem menüler ve arayüz metinlerinde **Türkçe ve İngilizce** dil desteği sunar.  
- **Asenkron Veri Yönetimi:** Tüm veri erişim süreçleri `MongoDB.Driver` üzerinden **async/await** ile yönetilmektedir.

---

## 🔹 Kullanılan Teknolojiler
- **Backend:** .NET Core / C#  
- **Veritabanı:** MongoDB  
- **Frontend:** HTML, CSS, JavaScript  
- **Localization:** Cookie tabanlı dil yönetimi  
- **AI Tasarım Araçları:** Claude AI, Gemini AI (harita görselleri)
- **Mail Servisi:** SMTP / .NET MailKit (Rezervasyon sonrası otomatik bilgilendirme)

---

## 🔹 Screenshotlar

### Tur Listesi
<img width="1680" height="2352" alt="Screenshot_2026-04-02_10-52-26" src="https://github.com/user-attachments/assets/3adf7761-6dd0-425a-9af8-a71929a6a58d" />

---

<img width="1680" height="2240" alt="Tour List 2" src="https://github.com/user-attachments/assets/0aef14b1-974d-4510-9224-39edc46fb1a7" />

---

<img width="1680" height="2145" alt="Tour List 3" src="https://github.com/user-attachments/assets/edeff79f-b28d-4c97-802d-65e7a60ea367" />

---

<img width="1680" height="3214" alt="Tour List 4" src="https://github.com/user-attachments/assets/c415104a-280f-41a3-af95-e6042773487a" />

---

<img width="1680" height="2138" alt="Tour List 5" src="https://github.com/user-attachments/assets/3dc2ec19-7e90-460e-ae60-4442c74d504c" />

---

<img width="1680" height="2340" alt="Tour List 6" src="https://github.com/user-attachments/assets/5f15bad8-2d1f-4023-95a8-a8c152eededd" />

### Rezervasyon Akışı
<img width="1920" height="1303" alt="Create Reservation 1" src="https://github.com/user-attachments/assets/e34e4032-4371-4b20-afc2-47f0f1e840c5" />

---

<img width="1920" height="1303" alt="Create Reservation 2" src="https://github.com/user-attachments/assets/c0eebc46-2eeb-4501-a79a-129ca8296bdc" />

---

<img width="1920" height="1377" alt="screencapture-localhost-7029-DefaultReservation-ReservationConfirm-2026-04-02-01_53_04" src="https://github.com/user-attachments/assets/cda6d0f3-5333-4143-9040-dd8a17f368c3" />

---

<img width="1193" height="733" alt="Reservation Summary" src="https://github.com/user-attachments/assets/c3957b00-9c02-446b-bbc7-46cceeea3cac" />

### Admin Paneli
<img width="1920" height="1041" alt="Tour List Admin" src="https://github.com/user-attachments/assets/a1dca482-7e3f-4984-9e8e-91b605dbaf1a" />

---

<img width="1920" height="2038" alt="Update Tour Admin" src="https://github.com/user-attachments/assets/45fc8b75-a662-4463-9bcc-5f9327d26e1e" />

---

<img width="1920" height="1571" alt="All Tour Plan Admin" src="https://github.com/user-attachments/assets/0fffbd7f-962b-46c7-bfde-efe84d992a9b" />

---

<img width="1920" height="1064" alt="Tour Plan List Admin" src="https://github.com/user-attachments/assets/06a637cd-dc82-416a-869b-28af40ad3401" />

---

<img width="1920" height="1104" alt="Create Tour Plan" src="https://github.com/user-attachments/assets/1b8e9a0b-5bd0-4c4f-b6d2-078a3d0f0d40" />

---

<img width="1920" height="1098" alt="Reservation List Admin" src="https://github.com/user-attachments/assets/d84f576c-3621-4ead-969b-680c6db6c910" />

---

<img width="1920" height="1028" alt="Report List Admin" src="https://github.com/user-attachments/assets/9e930d7d-7099-4617-a9b8-72573381130c" />

---

<img width="1920" height="1277" alt="Review List Admin" src="https://github.com/user-attachments/assets/b63b97a4-f22a-4e60-90c6-4617fcffcdd0" />

---

<img width="1694" height="1144" alt="Screenshot_2026-04-02_11-31-29" src="https://github.com/user-attachments/assets/7867fd58-faca-4735-9d75-aafadc83dddd" />


### Excel / PDF Raporlama
<img width="1898" height="979" alt="Report Export" src="https://github.com/user-attachments/assets/9a38ddab-9568-40a6-aa91-0d517bc9b9ab" />

---

<img width="788" height="888" alt="PDF Export" src="https://github.com/user-attachments/assets/c52e538c-4ff5-484c-b11e-76e79d8ca123" />

---

<img width="1193" height="433" alt="Excel Export" src="https://github.com/user-attachments/assets/6dff4eca-8447-4be4-b71a-13bea5eef7ad" />

