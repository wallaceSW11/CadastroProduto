using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace api.Models
{
  public class ProdutoDois
  {
    [Key]
    public string Identificador { get; set; }
    [Required(ErrorMessage = "Por favor, informe a descrição do produto")]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Por favor, informe o valor de venda do produto")]
    public decimal ValorVenda { get; set; }

    public bool PossuiComposicao { get; set; }

    public IEnumerable<Insumo> Insumos { get; set; }

    public IEnumerable<CustoReposicaoProduto> CustoReposicao { get; set; }

    public CustoMontagemProduto CustoMontagem { get; set; }

  }
}