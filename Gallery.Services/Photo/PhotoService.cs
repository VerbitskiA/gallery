using Gallery.Abstractions.Repositories;
using Gallery.Abstractions.Services;
using Gallery.Models.Photos.Output;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.Services.Photo
{
    /// <summary>
    /// Класс сервиса для работы с фото.
    /// </summary>
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        /// <summary>
        /// Метод добавит новое фото.
        /// </summary>
        /// <param name="tag">Тег.</param>
        /// <param name="imagePath">Путь к изображению.</param>
        /// <returns>Добавленное фото.</returns>
        public async Task<PhotoOutput> AddPhotoAsync(string tag, string imagePath)
        {
            try
            {
                var result = await _photoRepository.AddPhotoAsync(tag, imagePath);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Метод вернет все фото.
        /// </summary>
        /// <returns>Список фото.</returns>
        public async Task<IEnumerable<PhotoOutput>> GetAllPhotosAsync()
        {
            try
            {
                var result = await _photoRepository.GetAllPhotosAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Метод вернёт фото с учётом пагинации.
        /// </summary>
        /// <param name="tag">Тег.</param>
        /// <param name="page">Номер страницы.</param>        
        /// <param name="photosPerPage">Количество фото на странице.</param>
        /// <returns>Список фото с данными для пагинации.</returns>
        public async Task<PaginationPhotoOutput> GetPaginationPhotosAsync(string tag, int page, int photosPerPage = 6)
        {
            try
            {
                var result = await _photoRepository.GetPaginationPhotosAsync(tag, page, photosPerPage);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
