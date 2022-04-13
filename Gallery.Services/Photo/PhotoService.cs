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
        private readonly IFtpService _ftpService;

        public PhotoService(IPhotoRepository photoRepository, IFtpService ftpService)
        {
            _photoRepository = photoRepository;
            _ftpService = ftpService;
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
        /// Метод удалит фото из БД и файл с фото.
        /// </summary>
        /// <param name="photoId">Идентфикатор фото.</param>
        /// <returns>Статус удаления.</returns>
        public async Task<bool> DeletePhotoAsync(long photoId)
        {
            try
            {
                bool statusFile = false;

                bool statusPhoto = false;
                
                var deletedPhoto = await _photoRepository.GetPhotoByIdAsync(photoId);

                if (deletedPhoto is null)
                {
                    return statusFile && statusPhoto;
                }

                statusFile = await _ftpService.DeleteFileAsync(deletedPhoto.ImagePath);                            

                statusPhoto = await _photoRepository.DeletePhotoAsync(deletedPhoto.PhotoId);                

                return statusFile && statusPhoto;
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
