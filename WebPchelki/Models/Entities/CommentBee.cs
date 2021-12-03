using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPchelki.Models.Entities
{
    public class CommentBee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BeeId { get; set; }
        public Bee Bee { get; set; }

        //public int ClientId { get; set; }
        //public Client Client { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Много букав")]
        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
