namespace api.Repositories.Scripts
{
  public class ProdutoScripts
  {

    public static string SELECT_ALL_PRODUTOS = @"
      SELECT
        Identificador,
        Descricao,
        ValorVenda,
        Possuicomposicao
      FROM
        Produto (NoLock) ";
    public static string SELECT_PRODUTO_POR_IDENTIFICADOR = @"
          SELECT
            Identificador,
            Descricao,
            ValorVenda,
            PossuiComposicao
          FROM
            Produto (NoLock)
          WHERE
            Identificador = @identificador";

    public static string INSERT_PRODUTO = @"
      Insert into
        Produto 
        Values (
          @Descricao,
          @ValorVenda,
          @PossuiComposicao
       );
       Select Cast(SCOPE_IDENTITY() as varchar)";

    public static string INSERT_INSUMO = @"
      Insert into
        Insumo 
        Values (
          @IdentificadorProdutoPrincipal,
          @IdentificadorProdutoInsumo,
          @Quantidade,
          @Valor,
          @ValorTotal)";

    public static string DELETE_INSUMO = @"
      Delete From Insumo Where ProdutoPrincipalId = @identificador";
      
    public const string SELECT_PRODUTO_INSUMO_POR_IDENTIFICADOR = @"
          SELECT
            I.Identificador,
            I.ProdutoPrincipalId as IdentificadorProdutoPrincipal,
            I.ProdutoInsumoId as IdentificadorProdutoInsumo,
            P.Descricao,
            I.Quantidade,
            I.Valor,
            I.ValorTotal
          FROM
            Insumo I Inner Join Produto P on (P.Identificador = I.ProdutoInsumoId)
          WHERE
            I.ProdutoPrincipalId = @identificador";

    public const string UPDATE_PRODUTO = @"
      UPDATE
        Produto
      SET
        Descricao = @descricao,
        ValorVenda = @valorVenda,
        PossuiComposicao = @possuiComposicao
      WHERE
        Identificador = @identificador";



  }
}