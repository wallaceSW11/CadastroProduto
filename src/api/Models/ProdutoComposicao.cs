namespace api.Models
{
    public class ProdutoComposicao
    {
        public int Identificador { get; set; }

        public decimal Quantidade { get; set; }

        public Produto Produto {get; set;}

        public decimal ObterValorTotal()
        {
            return Quantidade * Produto.ValorVenda;
        }         
    }
}