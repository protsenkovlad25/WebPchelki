using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPchelki.Models.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string SecondName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string FatherName { get; set; }

        public DateTime DateBirth { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Много букав")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Много букав")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string Town { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string Street { get; set; }

        [Required]
        public int Home { get; set; }

        public ICollection<CommentProduct> CommentProducts { get; set; }
    }
}
