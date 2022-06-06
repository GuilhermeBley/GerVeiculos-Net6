using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteNet6.Shared.EntityFramework
{
    [Table("Infracao")]
    public class Infracao
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string Ait { get; set; }

        // Foreign key
        [ForeignKey(nameof(Veiculo))]
        public int Renavam { get; set; }

        public Veiculo Veiculo { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Local { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime Validade { get; set; }
    }
}
