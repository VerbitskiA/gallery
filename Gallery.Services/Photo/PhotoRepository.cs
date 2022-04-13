using Gallery.Abstractions.Repositories;
using Gallery.Core.Data;
using Gallery.Models.Entities;
using Gallery.Models.Photos.Output;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gallery.Services.Photo
{
    /// <summary>
    /// Класс репозитория фото.
    /// </summary>
    public class PhotoRepository : IPhotoRepository
    {
        private readonly LocalMsContext _localMsContext;

        public PhotoRepository(LocalMsContext localMsContext)
        {
            _localMsContext = localMsContext;
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
                var utcNow = DateTime.UtcNow;

                PhotoEntity photo = new PhotoEntity()
                {
                    Tag = String.IsNullOrEmpty(tag) ? utcNow.ToString() : tag,
                    ImagePath = imagePath,
                    Date = utcNow
                };

                await _localMsContext.AddAsync(photo);
                await _localMsContext.SaveChangesAsync();

                var result = await _localMsContext.Photos
                    .Where(p => p.Tag == photo.Tag && p.ImagePath == photo.ImagePath && p.Date == photo.Date)
                    .Select(p => new PhotoOutput
                    {
                        PhotoId = p.PhotoId,
                        Tag = p.Tag,
                        ImagePath = p.ImagePath,
                        Date = p.Date.ToString()
                    })
                    .FirstOrDefaultAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Метод удалит фото.
        /// </summary>
        /// <param name="photoId">Идентфикатор фото.</param>
        /// <returns>Статус удаления.</returns>
        public async Task<bool> DeletePhotoAsync(long photoId)
        {
            try
            {
                var findedPhoto = await _localMsContext.Photos.FirstOrDefaultAsync(p => p.PhotoId.Equals(photoId));

                if (findedPhoto is null)
                {
                    return false;
                }

                _localMsContext.Remove(findedPhoto);
                await _localMsContext.SaveChangesAsync();

                return true;
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
                var result = await _localMsContext.Photos
                    .Select(u => new PhotoOutput
                    {
                        PhotoId = u.PhotoId,
                        Tag = u.Tag,
                        ImagePath = u.ImagePath,
                        Date = u.Date.ToShortDateString()
                    })
                    .ToListAsync();

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
            var queryPhotos = _localMsContext.Photos.AsQueryable();

            if (!String.IsNullOrEmpty(tag))
            {
                queryPhotos = queryPhotos.Where(u => u.Tag.StartsWith(tag));
            }

            var photos = await queryPhotos.OrderByDescending(u => u.Date)
                .Select(u => new PhotoOutput
                {
                    PhotoId = u.PhotoId,
                    ImagePath = u.ImagePath,
                    Tag = u.Tag,
                    Date = u.Date.ToShortDateString()
                })
                .ToListAsync();

            int countPhotos = photos.Count;

            int start = (page - 1) * photosPerPage;

            var resultPhotos = photos.Skip(start).Take(photosPerPage);

            PaginationPhotoOutput paginationPhotoOutput = new PaginationPhotoOutput()
            {
                Photos = resultPhotos,
                CurrentPage = page,
                PhotosPerPage = photosPerPage,
                PagesCount = Convert.ToInt32(Math.Ceiling(countPhotos / (double)photosPerPage)),
                PhotosCount = countPhotos,
                Tag = tag,
                MaxVisiblePage = 5
            };

            return paginationPhotoOutput;
        }

        /// <summary>
        /// Метод найдёт фото по id.
        /// </summary>
        /// <param name="photoId">Идентификатор фото.</param>
        /// <returns>Фото.</returns>
        public async Task<PhotoOutput> GetPhotoByIdAsync(long photoId)
        {
            try
            {
                var findedPhoto = await _localMsContext.Photos
                    .Select(p=> new PhotoOutput
                    {
                        PhotoId=p.PhotoId,
                        Tag = p.Tag,
                        ImagePath = p.ImagePath,
                        Date = p.Date.ToShortDateString()
                    })
                    .FirstOrDefaultAsync(p => p.PhotoId.Equals(photoId));

                return findedPhoto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
