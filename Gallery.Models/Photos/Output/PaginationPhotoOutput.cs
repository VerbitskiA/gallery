using System.Collections.Generic;

namespace Gallery.Models.Photos.Output
{
    /// <summary>
    /// Класс выходной модели фото с пагинацией.
    /// </summary>
    public class PaginationPhotoOutput
    {
        /// <summary>
        /// Список фото.
        /// </summary>
        public IEnumerable<PhotoOutput> Photos;

        /// <summary>
        /// Фото на странице.
        /// </summary>
        public int PhotosPerPage { get; set; }

        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Всего страниц.
        /// </summary>
        public int PagesCount { get; set; }

        /// <summary>
        /// Всего фото.
        /// </summary>
        public int PhotosCount { get; set; }

        /// <summary>
        /// Тег.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Максимальное количество отображаемых страниц.
        /// </summary>
        public int MaxVisiblePage { get; set; }
    }
}
