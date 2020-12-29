using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
  public class Montagem
  {
    [Range(1, Double.MaxValue, ErrorMessage = "Por favor, informe o tempo de montagem, no mínimo 1.")]
    public int TempoMontagem { get; set; }

    [Range(0.01, Double.MaxValue, ErrorMessage = "Por favor, informe o valor do custo de montagem, no mínimo 0,01.")]
    public double ValorCustoMontagem { get; set; }

    public double ValorTotalCustoMontagem =>
      this.TempoMontagem * this.ValorCustoMontagem;

    public Montagem() { }
    public Montagem(Montagem montagem)
    {
      this.TempoMontagem = montagem.TempoMontagem;
      this.ValorCustoMontagem = montagem.ValorCustoMontagem;
    }

  }

}