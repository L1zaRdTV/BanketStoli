# БанкeтСтоли — WPF система банкетных комнат

Проект содержит WPF-приложение для просмотра клиентами банкетных комнат и управления справочником комнат менеджерами.

## Запуск

1. Откройте `BanketStoli.sln` в Visual Studio.
2. Выполните скрипт `src/BanketStoli/Data/DatabaseSchema.sql` в SQL Server LocalDB или SQL Server.
3. При необходимости измените строку подключения `BanketStoliDb` в `src/BanketStoli/App.config`.
4. Запустите проект `BanketStoli`.

## Тестовые учетные записи

- Клиент: `client1` / `client1`
- Менеджер: `manager1` / `manager1`
