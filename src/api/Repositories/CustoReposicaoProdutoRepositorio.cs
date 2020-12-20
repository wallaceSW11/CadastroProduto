using System.Collections.Generic;
using System.Data;
using System.Linq;
using api.Factories;
using api.Models;
using api.Repositories.Scripts;
using Dapper;

namespace api.Repositories
{
  public class CustoReposicaoProdutoRepositorio
  {
    private readonly IDbConnection _connection;

    internal CustoReposicaoProdutoRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
    }

    public void Dispose()
    {
      _connection?.Dispose();
    }
    public IEnumerable<CustoReposicaoProduto> Obter(int identificador)
    {
      var custosReposicaoProduto = _connection.Query<CustoReposicaoProduto>(
          ProdutoScripts.SELECT_CUSTO_REPOSICAO_POR_IDENTIFICADOR_PRODUTO,
          new { identificador }
      )
      .ToList();

      return custosReposicaoProduto;
    }
  }
}