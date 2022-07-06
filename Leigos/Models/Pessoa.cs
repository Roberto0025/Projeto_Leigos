using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leigos.Models
{
    [Table("Pessoas", Schema = "public")]
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string? NomePessoa { get; set; }

        [Display(Name = "Telefone")]
        public string? TelefonePessoa { get; set; }

        [Display(Name = "Email")]
        public string? EmailPessoa { get; set; }

        [Display(Name = "Senha")]
        public string? SenhaPessoa { get; set; }

        [Display(Name = "Genêro")]
        public int GeneroId { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public string? Cep { get; set; }

        [Display(Name = "Rua")]
        public string? Rua { get; set; }

        [Display(Name = "Nº")]
        public int Numero { get; set; }

        [Display(Name = "Bairro")]
        public string? Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string? Cidade { get; set; }

        [Display(Name = "UF")]
        public string? UF { get; set; }
    }
}
