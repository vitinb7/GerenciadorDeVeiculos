using System.ComponentModel.DataAnnotations;

namespace ApiCarro.ViewModels
{
    public class CarroViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public int Ano { get; set; }
    }
}
