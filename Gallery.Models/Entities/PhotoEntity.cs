using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gallery.Models.Entities
{
    /// <summary>
    /// Класс соответсвует таблице dbo.Photos
    /// </summary>
    [Table("Photos", Schema = "dbo")]
    public class PhotoEntity
    {
        /// <summary>
        /// Идентификатор фото.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PhotoId { get; set; }

        /// <summary>
        /// Тег фото.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Путь к изображению.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Дата загрузки.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
