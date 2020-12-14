using System;
using System.Data;
using api.Factories;
using api.Models;
using Dapper;

namespace api.Repositories
{
  public class ProdutoRepositorio : IDisposable
  {
    private static string SELECT = @"
          SELECT
            Identificador,
            Descricao,
            ValorVenda,
            PossuiComposicao
          FROM
            Produto (NoLock)
          WHERE
            Identificador = @identificador
        ";
    private readonly IDbConnection _connection;
    private readonly ProdutoComposicaoRepositorio _produtoComposicaoRepositorio;

    public ProdutoRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
      _produtoComposicaoRepositorio = new ProdutoComposicaoRepositorio(factory);
    }

    public void Dispose()
    {
      _connection?.Dispose();
      _produtoComposicaoRepositorio?.Dispose();
    }

    public Produto Obter(int identificador)
    {
      var produto = _connection.QuerySingle<Produto>(SELECT, new { identificador });
      if (produto.PossuiComposicao)
      {
        produto.Composicoes = _produtoComposicaoRepositorio.Obter(produto);
      }

      return produto;
    }
  }
}