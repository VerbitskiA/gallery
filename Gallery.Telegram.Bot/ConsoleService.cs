using Gallery.Abstractions.Services;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


namespace Gallery.Telegram.Bot
{
    public class ConsoleService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IPhotoService _photoService;
        private readonly IFtpService _ftpService;

        static ITelegramBotClient bot = new TelegramBotClient("5292912936:AAGKysvtHG68am-XYA1jk9OQZIdAc7zlTJs");

        public ConsoleService(IHostApplicationLifetime appLifetime, IPhotoService photoService, IFtpService ftpService)
        {
            _appLifetime = appLifetime;
            _photoService = photoService;
            _ftpService = ftpService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    try
                    {
                        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

                        var cts = new CancellationTokenSource();
                        var cancellationToken = cts.Token;
                        var receiverOptions = new ReceiverOptions
                        {
                            AllowedUpdates = { }, // receive all update types
                        };
                        bot.StartReceiving(
                            HandleUpdateAsync,
                            HandleErrorAsync,
                            receiverOptions,
                            cancellationToken
                        );
                        Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {

                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));

            if (update.Type == UpdateType.ChannelPost)
            {
                var message = update.ChannelPost;

                if (message.Photo is not null)
                {
                    var path = await _ftpService.DownloadFileFromTelegramAsync(bot, message.Photo[^1].FileId);

                    if (String.IsNullOrEmpty(path))
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Что-то пошло не так :( Пожалуйста повторите попытку и следуйте всем указаниям.");

                        return;
                    }

                    var res = await _photoService.AddPhotoAsync(message.Caption, path);

                    if (res is null)
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Что-то пошло не так :( Пожалуйста повторите попытку и следуйте всем указаниям.");

                        return;
                    }

                    await botClient.SendTextMessageAsync(message.Chat, "Ваше фото успешно загружено!");

                    return;
                }
            }
 }
        
        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
