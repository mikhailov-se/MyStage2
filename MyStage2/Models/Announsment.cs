using System;
using System.ComponentModel.DataAnnotations;

namespace MyStage2.Models
{
    public class Announsment
    {
        public int Id { get; set; }
        public int Number { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [MaxLength(300)] 
        public string TextAnnounsment { get; set; }


        [Range(typeof(int), "1", "10")]
        public int Rating { get; set; }

        public User User { get; set; }
    }
}
