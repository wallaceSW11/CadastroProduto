using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class Insumo
  {
    [Key]
    public string Identificador { get; set; }


    public string IdentificadorProdutoPrincipal { get; set; }


    public string IdentificadorProdutoInsumo { get; set; }

    public string Descricao { get; set; }

    [Required(ErrorMessage = "Por favor, informe a quantidade do produto insumo.")]
    public float Quantidade { get; set; }

    [Required(ErrorMessage = "Por favor, informe o valor do produto insumo.")]
    public float Valor { get; set; }

    [Required(ErrorMessage = "Por favor, informe o valor total do prduto insumo.")]
    public float ValorTotal { get; set; }
  }
}