using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GetChatIdBot;

public sealed class ChatIdBot(ITelegramBotClient telegramBotClient, ILogger<ChatIdBot> logger) : BackgroundService
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;
    private readonly ILogger<ChatIdBot> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ChatIdBot started.");
        await StartReceivingUpdatesAsync(stoppingToken);
    }

    private async Task StartReceivingUpdatesAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
            },
            cancellationToken: cancellationToken
        );

        await Task.Delay(-1, cancellationToken);
    }

    public async Task HandleUpdateAsync(
        ITelegramBotClient telegramBotClient,
        Update update, CancellationToken cancellationToken)
    {
        if (update.Message?.Chat?.Id is null)
            return;

        var chatId = update.Message.Chat.Id;
        _logger.LogInformation("Received message from chat ID: {ChatId}", chatId);

        await _telegramBotClient.SendMessage(
            chatId, $"Chat ID: `{chatId}`", ParseMode.Markdown, cancellationToken: cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An error occurred while receiving updates.");
        return Task.CompletedTask;
    }
}