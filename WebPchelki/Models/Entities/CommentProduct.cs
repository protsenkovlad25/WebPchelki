using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebPchelki.Models.Entities
{
    public class CommentProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        //public int ClientId { get; set; }
        //public Client Client { get; set; }

        [Required]
        [MaxLength(200, ErrorMessage = "Много букав")]
        public string Description { get; set; }

        public DateTime Date { get; set; }
    }
}
