using System.Collections.Generic;
using System.Linq;

namespace api.Models
{
    public class Produto
    {
        public string Identificador { get; set; }

        public string Descricao { get; set; }

        public decimal ValorVenda { get; set; }

        public bool PossuiComposicao { get; set; }

        public IEnumerable<ProdutoComposicao> Composicoes { get; set; }

        public decimal ValorFinalVenda => this.ValorVenda + this.Composicoes?.Sum(c => c.ObterValorTotal()) ?? 0;
    }
}