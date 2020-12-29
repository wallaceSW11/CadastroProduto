using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace api.Models
{
  public class ProdutoDois
  {
    [Key]
    public string Identificador { get; }

    [Required(ErrorMessage = "Por favor, informe a descrição do produto")]
    public string Descricao { get; set; }
    public double ValorAcrescimoVenda { get; set; }

    public double ValorDescontoVenda { get; set; }

    public double PorcentagemMargemDeLucro { get; set; }

    [Required(ErrorMessage = "Por favor, informe o valor de venda do produto")]
    public double ValorVenda { get; set; }

    public int TempoMontagem { get; set; }

    public double ValorCustoMontagem { get; set; }

    public double ValorTotalCustoMontagem { get; set; }

    public bool PossuiComposicao { get; set; }

    public IEnumerable<Insumo> Insumos { get; set; }

    public CustoReposicaoProduto CustoReposicao { get; set; }

    public ProdutoDois() { }
    public ProdutoDois(ProdutoDois produto)
    {
      this.CustoReposicao = produto.CustoReposicao;
      this.ValorTotalCustoMontagem = produto.ValorTotalCustoMontagem;
      this.PorcentagemMargemDeLucro = produto.PorcentagemMargemDeLucro;
      this.ValorAcrescimoVenda = produto.ValorAcrescimoVenda;
      this.ValorDescontoVenda = produto.ValorDescontoVenda;
      this.Insumos = produto.Insumos;

      double totalInsumos = 0;

      if (produto.PossuiComposicao)
      {
        totalInsumos = this.Insumos.Sum(c => c.ValorTotal);
      }

      this.ValorVenda =
        Math.Round(
          (this.CustoReposicao.ValorCustoReposicaoUnitario +
           this.ValorTotalCustoMontagem + totalInsumos) * (1 + (this.PorcentagemMargemDeLucro / 100)) +
           this.ValorAcrescimoVenda - this.ValorDescontoVenda, 2);
    }

  }
}