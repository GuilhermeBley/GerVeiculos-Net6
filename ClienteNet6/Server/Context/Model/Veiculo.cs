using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteNet6.Server.Context.Model
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Key]
        [Column(TypeName = "int(17)")]
        public int Renavam { get; set; }

        [Required]
        [Column(TypeName = "char(17)")]
        public string Chassi { get; set; }

        [Column(TypeName = "char(7)")]
        [Required]
        public string Placa { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string Cor { get; set; }

        [Column(TypeName = "varchar(45)")]
        public string Modelo { get; set; }


        public ICollection<Infracao> Infracoes { get; set; }
    }
}
