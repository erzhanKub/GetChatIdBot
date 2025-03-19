# Telegram Chat ID Bot  

Это простой Telegram-бот для получения chat ID

В файле `Program` замените:  

```csharp
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")  
        ?? throw new ArgumentNullException("Environment variable 'TELEGRAM_BOT_TOKEN' is not set")));
```
на:

```csharp
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("YOUR_TOKEN"));
```
