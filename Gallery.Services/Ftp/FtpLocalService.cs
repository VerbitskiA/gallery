using Gallery.Abstractions.Services;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Gallery.Services.Ftp
{
    /// <summary>
    /// Класс фтп сервиса для работы на локальной машине.
    /// </summary>
    public class FtpLocalService : IFtpService
    {
        private readonly string pathToImagesDirectory = "../../../../Gallery.WebUI/wwwroot";

        /// <summary>
        /// Метод загрузит файл из чата с ботом.
        /// </summary>
        /// <param name="_bot">Телеграм бот.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Короткий путь к загруженному файлу.</returns>
        public async Task<string> DownloadFileFromTelegramAsync(ITelegramBotClient _bot, string fileId)
        {
            try
            {
                var file = await _bot.GetFileAsync(fileId);

                string shortpath = "/images/" + Guid.NewGuid().ToString().Replace("-", "") + ".jpg";

                string path = pathToImagesDirectory+shortpath;               

                using (var saveImageStream = new FileStream(path, FileMode.Create))
                {
                    await _bot.DownloadFileAsync(file.FilePath, saveImageStream);

                    return shortpath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading: " + ex.Message);
                return String.Empty;
            }
        }
    }
}
