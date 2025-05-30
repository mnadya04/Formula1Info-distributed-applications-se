# Formula1InfoMVC

**Факултетен номер:** 2301321051  
**ИМЕ:** Надя Мазълова  
**Име на проекта:** Formula1InfoMVC  
**Тип:** ASP.NET Core MVC + Web API  
**Технологии:** ASP.NET Core, Entity Framework Core, SQL Server, REST API, Bootstrap

## 📝 Описание на проекта

Formula1InfoMVC е уеб приложение, което предоставя информация за Формула 1 – отбори, пилоти и състезания. Проектът е разделен на две части:
- **API** (ASP.NET Core Web API) – управлява данните и извършва CRUD операции.
- **MVC клиент** (ASP.NET Core MVC) – уеб интерфейс, който консумира API-то чрез HTTP заявки.

Функционалности:
- Списъци с отбори, пилоти и състезания с търсене, филтриране и странициране.
- CRUD операции (достъпни само за администратори).
- Показване на бъдещи/минали състезания.
- Аутентикация с роли (`Admin`, `User`).

## ⚙️ Инсталация и стартиране

### 1. Изисквания

- .NET 7 SDK или по-нова версия  
- SQL Server (или SQL Server Express)  
- Visual Studio 2022+ или Visual Studio Code 

### 2. Стъпки за инсталация

1. **Клонирай проекта или изтегли**  
   
   git clone https://github.com/yourusername/Formula1InfoMVC.git


2. **Обнови connection string. Конфигурирай връзката към базата
Във appsettings.json на проекта Formula1Info**

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=Formula1Db;Trusted_Connection=True;MultipleActiveResultSets=true"
}

3. **Създай и обнови базата с миграции**

cd Formula1Info
dotnet ef database update

4. **Стартирай проекта**
📌 Препоръчителен начин:
Използвай "Multiple startup projects" опцията във Visual Studio:
Project 1: Formula1Info (Web API)
Project 2: Formula1InfoMVC (MVC клиент)
