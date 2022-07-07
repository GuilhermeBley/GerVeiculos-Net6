using System.ComponentModel.DataAnnotations;

namespace ClienteNet6.Shared.Dto
{
    public class VeiculoDto
    {
        [Required]
        public int Renavam { get; set; }

        [Required]
        public string Chassi { get; set; }

        [Required]
        public string Placa { get; set; }

        public string Cor { get; set; }

        public string Modelo { get; set; }
    }
}
