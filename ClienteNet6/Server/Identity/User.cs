using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteNet6.Server.Identity
{
    /// <summary>
    /// Tabela de usuários
    /// </summary>
    public class User : IdentityUser<int>
    {
        [Required, Column(TypeName = "varchar(100)")]
        public string NomeEmpresa { get; set; }
    }
}
