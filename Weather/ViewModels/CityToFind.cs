using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class CityToFind
    {
        [Required, MinLength(2), MaxLength(50), Display(Name = "Населенный пункт")]
        public string City { get; set; } = String.Empty;
    }
}
