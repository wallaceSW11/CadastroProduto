using System.ComponentModel.DataAnnotations;
using System;

namespace api.Models
{
  public class Insumo
  {
    [Key]
    public string Identificador { get; }
    public string IdentificadorProdutoPrincipal { get; set; }
    public string IdentificadorProdutoInsumo { get; set; }
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Por favor, informe a quantidade do produto insumo.")]
    public float Quantidade { get; set; }

    [Required(ErrorMessage = "Por favor, informe o valor do produto insumo.")]
    public double Valor { get; set; }
    public double ValorTotal =>
      Math.Round(this.Valor * this.Quantidade, 2);
  }
}