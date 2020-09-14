using System.ComponentModel.DataAnnotations;

namespace BandApi.DataTransferObjects
{
    public class AlbumCreationDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}
