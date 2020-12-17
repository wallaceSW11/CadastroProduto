using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class CustoReposicaoProduto
  {
    [Key]
    public string Identificador { get; set; }
    public string IdentificadorProduto { get; set; }

    public DateTime DataCompra { get; set; }
    public string UnidadeCompra { get; set; }
    public float QuantidadeEmbalagem { get; set; }
    public float ValorCompra { get; set; }
    public float ValorFrete { get; set; }
    public float ValorAcrescimo { get; set; }
    public float ValorDesconto { get; set; }
    public float ValorCustoReposicao { get; set; }
    public float ValorCustoReposicaoUnitario { get; set; }

  }
}