using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPchelki.Models.Entities
{
    public class Beehive
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [MaxLength(50, ErrorMessage = "Много букав")]
        public string Name { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Много букав")]
        public string Size { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Много букав")]
        public string FrameSize { get; set; }

        [Required]
        public int FrameNumber { get; set; }

        public ICollection<CommentBeehive> CommentBeehives { get; set; }
    }
}
