using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leigos.Models
{
    [Table("Generos", Schema = "public")]
    public class Genero
    {
        [Key]
        public int GeneroId { get; set; }

        [Display(Name = "Genero")]
        public string? GeneroNome { get; set; }
    }
}