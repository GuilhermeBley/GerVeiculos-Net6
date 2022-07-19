using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClienteNet6.Server.Context.Model
{
    /// <summary>
    /// Page log table
    /// </summary>
    [Table("Log")]
    public class Log
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string User { get; set; }

        [Required, MaxLength(200)]
        public string PageUrl { get; set; }

        [Required]
        public DateTime DateAcess { get; set; } = DateTime.Now;
    }
}
