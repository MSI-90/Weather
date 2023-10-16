using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class CityToFind
    {
        [Required (ErrorMessage = "Необходимо указать наименование н.п."), MinLength(2), MaxLength(50), Display(Name = "Населенный пункт")]
        public string City { get; set; } = string.Empty;
    }
}
