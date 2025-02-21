using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace GetChatIdBot;

public sealed class ChatIdBot(ITelegramBotClient telegramBotClient) : BackgroundService
{
    private readonly ITelegramBotClient _telegramBotClient = telegramBotClient;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        => await StartReceivingUpdatesAsync(stoppingToken);

    private async Task StartReceivingUpdatesAsync(CancellationToken cancellationToken)
    {
        _telegramBotClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            new ReceiverOptions
            {
                AllowedUpdates = [UpdateType.Message],
            }
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

        await _telegramBotClient.SendMessage(
            chatId, $"Chat ID: `{chatId}`", ParseMode.Markdown, cancellationToken: cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient telegramBotClient, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
