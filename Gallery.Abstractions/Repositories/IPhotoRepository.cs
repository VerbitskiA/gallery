using Gallery.Models.Photos.Output;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.Abstractions.Repositories
{
    /// <summary>
    /// Абстракция репозитория фото.
    /// </summary>
    public interface IPhotoRepository
    {

        /// <summary>
        /// Метод найдёт фото по id.
        /// </summary>
        /// <param name="photoId">Идентификатор фото.</param>
        /// <returns>Фото.</returns>
        Task<PhotoOutput> GetPhotoByIdAsync(long photoId);

        /// <summary>
        /// Метод вернет все фото.
        /// </summary>
        /// <returns>Все фото.</returns>
        Task<IEnumerable<PhotoOutput>> GetAllPhotosAsync();

        /// <summary>
        /// Метод вернёт фото с учётом пагинации.
        /// </summary>
        /// <param name="tag">Тег.</param>
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

        /// <summary>
        /// Метод удалит фото.
        /// </summary>
        /// <param name="photoId">Идентфикатор фото.</param>
        /// <returns>Статус удаления.</returns>
        Task<bool> DeletePhotoAsync(long photoId);
    }
}
