using BandApi.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace BandApi.DataTransferObjects
{
    [TitleAndDescriptionValidation]
    public class AlbumCreationDto
    {
        [Required(ErrorMessage = "Each album must have a title")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
