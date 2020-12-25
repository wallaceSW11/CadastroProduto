using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class CustoReposicao
  {
    [Range(1, Double.MaxValue, ErrorMessage = "Por favor, informe a quantidade, no mínimo 1.")]
    public double QuantidadeEmbalagem { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Por favor, informe o valor de compra.")]
    public double ValorCompra { get; set; }

    public double ValorFrete { get; set; }

    public double ValorAcrescimo { get; set; }

    public double ValorDesconto { get; set; }

    public double ValorCustoReposicao =>
      Math.Round(this.QuantidadeEmbalagem / (ValorCompra + ValorFrete + ValorAcrescimo), 2);

    public double ValorCustoReposicaoUnitario =>
      Math.Round(this.ValorCustoReposicao / this.QuantidadeEmbalagem, 2);

    public CustoReposicao() { }
    public CustoReposicao(CustoReposicao custoReposicao)
    {
      this.QuantidadeEmbalagem = custoReposicao.QuantidadeEmbalagem;
      this.ValorCompra = custoReposicao.ValorCompra;
      this.ValorFrete = custoReposicao.ValorFrete;
      this.ValorAcrescimo = custoReposicao.ValorAcrescimo;
      this.ValorDesconto = custoReposicao.ValorDesconto;
    }
  }
}