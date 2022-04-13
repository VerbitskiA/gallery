using System.Threading.Tasks;
using Telegram.Bot;

namespace Gallery.Abstractions.Services
{
    /// <summary>
    /// Абстракция сервиса для работы с файлами на локальной машине.
    /// </summary>
    public interface IFtpService
    {
        /// <summary>
        /// Метод загрузит файл из чата с ботом.
        /// </summary>
        /// <param name="_bot">Телеграм бот.</param>
        /// <param name="fileId">Идентификатор файла.</param>
        /// <returns>Короткий путь к загруженному файлу.</returns>
        Task<string> DownloadFileFromTelegramAsync(ITelegramBotClient _bot, string fileId);

        /// <summary>
        /// Метод удалит файл.
        /// </summary>
        /// <param name="path">Путь к файлу.</param>
        /// <returns>Статус удаления.</returns>
        Task<bool> DeleteFileAsync(string path);
    }
}
