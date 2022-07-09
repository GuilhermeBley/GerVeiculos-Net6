using System.ComponentModel.DataAnnotations;

namespace ClienteNet6.Shared.Dto
{
    public class InfracaoDto
    {
        [Required]
        public int Renavam { get; set; }

        [Required]
        public string Ait { get; set; }

        [Required]
        public string Local { get; set; }

        [Required]
        public DateTime Emissao { get; set; }

        [Required]
        public DateTime Validade { get; set; }
    }
}