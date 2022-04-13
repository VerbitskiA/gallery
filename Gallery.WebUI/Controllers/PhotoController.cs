using Gallery.Abstractions.Services;
using Gallery.Models.Photos.Output;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gallery.WebUI.Controllers
{
    /// <summary>
    /// Контроллер фото.
    /// </summary>
    public class PhotoController : Controller
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        /// <summary>
        /// Метод получит все фото.
        /// </summary>
        /// <returns>Список фото.</returns>
        public async Task<ActionResult<IEnumerable<PhotoOutput>>> AllPhotos()
        {
            var photos = await _photoService.GetAllPhotosAsync();

            return View("AllPhotos", photos);
        }

        /// <summary>
        /// Метод вернёт список фото с учётом фильтров и пагинации.
        /// </summary>
        /// <param name="tag">Тег.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="photoPerPage">Число фото на странице.</param>
        /// <returns>Список фото с учётом пагинации.</returns>
        public async Task<ActionResult<PaginationPhotoOutput>> Photos(string tag, int page = 1, int photoPerPage = 4)
        {
            var result = await _photoService.GetPaginationPhotosAsync(tag, page, photoPerPage);

            return View("Photos", result);
        }
    }
}
