using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Leigos.Models
{
    [Table("Servicos", Schema = "public")]
    public class Servico
    {
        [Key]
        public int ServicoId { get; set; }

        [Required]
        [Display(Name = "Nome do Serviço")]
        public string? NomeServico { get; set; }

        [Display(Name = "Breve Descrição")]
        public string? DescricacaoServico { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Display(Name = "Prestador")]
        public string? EmailPessoa { get; set; }

        [Display(Name = "Avaliação")]
        public float NotaServico { get; set; }

        [Display(Name = "Imagem")]
        public string? Imagem { get; set; }
    }
}
