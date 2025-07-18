# MarketplaceDataSync

Интеграционный сервис на .NET Core для синхронизации данных между маркетплейсом и системой учёта.

## Технологии

- ASP.NET Core Web API
- Entity Framework Core + SQLite
- MongoDB
- BackgroundService
- Serilog
- Swagger

## Функции

- Получение данных из внешнего API
- Сохранение в SQL и MongoDB
- REST API для работы с локальными данными
- Логирование в файл

## Запуск

```bash
dotnet ef database update
dotnet run
