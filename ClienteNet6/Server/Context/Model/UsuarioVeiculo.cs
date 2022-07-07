using ClienteNet6.Server.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteNet6.Server.Context.Model
{
    [Table("UsuarioVeiculo")]
    public class UsuarioVeiculo
    {
        [Key]
        public int IdUser { get; set; }
        [Key]
        public int IdVeiculo { get; set; }

        public Veiculo Veiculo { get; set; }
        public User User { get; set; }
    }
}
