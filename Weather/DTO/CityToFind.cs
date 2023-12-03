using System.ComponentModel.DataAnnotations;

namespace Weather.ViewModels
{
    public class CityToFind
    {
        [Required (ErrorMessage = "Необходимо указать наименование н.п.")]
        [MinLength(2, ErrorMessage = "Минимальная длина 2 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        [Display(Name = "Населенный пункт")]
        public string City { get; set; } = string.Empty;
    }
}
