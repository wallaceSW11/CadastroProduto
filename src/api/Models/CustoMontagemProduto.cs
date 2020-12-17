namespace api.Models
{
  public class CustoMontagemProduto
  {
    public int TempoMontagem { get; set; }

    public float ValorCustoMinuto { get; set; }

    public float ValorCustoMontagem => this.TempoMontagem * this.ValorCustoMinuto;
  }
}