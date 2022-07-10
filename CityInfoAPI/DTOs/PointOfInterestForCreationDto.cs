using System.ComponentModel.DataAnnotations;

namespace CityInfoAPI.DTOs
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a name value")]
        [MaxLength(50)]
        public string NameofPoi { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
    }
}
