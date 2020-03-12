using System;
using System.ComponentModel.DataAnnotations;

namespace MyStage2.Models
{
    public class Announsment
    {
        public int Id { get; set; }


        [Required]
        public int Number { get; set; }


        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }


        [Required(ErrorMessage = "Не указан текст объявления")]
        [MaxLength(300)] 
        public string TextAnnounsment { get; set; }


        [Required(ErrorMessage = "Необходимо число от 1 до 10")]
        [Range(typeof(int), "1", "10")]
        public int Rating { get; set; }


        public User User { get; set; }
    }
}
