using System;

namespace Gallery.Models.Photos.Output
{
    /// <summary>
    /// Класс выходной модели фото.
    /// </summary>
    public class PhotoOutput
    {
        /// <summary>
        /// Идентификатор фото.
        /// </summary>
        public long PhotoId { get; set; }

        /// <summary>
        /// Тег.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Путь к изображению.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Дата загрузки.
        /// </summary>
        public string Date { get; set; }

    }
}
