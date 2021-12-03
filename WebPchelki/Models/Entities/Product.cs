using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPchelki.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TypeProductId { get; set; }
        public TypeProduct TypeProduct { get; set; } 

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string Name { get; set; }

        [Required]
        public int Number { get; set; }

        public ICollection<CommentProduct> CommentProducts { get; set; }
    }
}
