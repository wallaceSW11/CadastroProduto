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

    [Required(ErrorMessage = "Por favor, informe o valor de venda do produto")]
    public decimal ValorVenda { get; set; }

    public int TempoMontagem { get; set; }

    public float ValorCustoMontagem { get; set; }

    public float ValorTotalCustoMontagem { get; set; }

    public bool PossuiComposicao { get; set; }

    public IEnumerable<Insumo> Insumos { get; set; }

    public IEnumerable<CustoReposicaoProduto> CustoReposicao { get; set; }



  }
}