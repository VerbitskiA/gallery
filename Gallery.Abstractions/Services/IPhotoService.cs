using Gallery.Models.Photos.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.Abstractions.Services
{
    /// <summary>
    /// Абстрация сервиса фото.
    /// </summary>
    public interface IPhotoService
    {
        /// <summary>
        /// Метод вернет все фото.
        /// </summary>
        /// <returns>Все фото.</returns>
        Task<IEnumerable<PhotoOutput>> GetAllPhotosAsync();

        /// <summary>
        /// Метод вернёт фото с учётом пагинации.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="photosPerPage">Количество фото на странице.</param>
        /// <returns>Список фото с данными для пагинации.</returns>
        Task<PaginationPhotoOutput> GetPaginationPhotosAsync(string tag, int page, int photosPerPage = 6);

        /// <summary>
        /// Метод добавит новое фото.
        /// </summary>
        /// <param name="tag">Тег.</param>
        /// <param name="imagePath">Путь к изображению.</param>
        /// <returns>Добавленное фото.</returns>
        Task<PhotoOutput> AddPhotoAsync(string tag, string imagePath);
    }
}
