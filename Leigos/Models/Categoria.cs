using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leigos.Models
{
    [Table("Categorias", Schema = "public")]
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Nome")]
        public string? CategoriaNome { get; set; }
    }
}
