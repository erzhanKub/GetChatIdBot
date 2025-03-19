# Telegram Chat ID Bot  

Simple telegram bot for receiving chat Id

In the `Program` file, replace:

```csharp
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN")  
        ?? throw new ArgumentNullException("Environment variable 'TELEGRAM_BOT_TOKEN' is not set")));
```
with:

```csharp
builder.Services.AddSingleton<ITelegramBotClient>(new TelegramBotClient("YOUR_TOKEN"));
```
