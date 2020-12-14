using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using api.Factories;
using api.Models;
using Dapper;
using api.Repositories.Scripts;

namespace api.Repositories
{
  internal class InsumoRepositorio : IDisposable
  {
    private readonly IDbConnection _connection;

    internal InsumoRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
    }

    public void Dispose()
    {
      _connection?.Dispose();
    }

    public IEnumerable<Insumo> Obter(int identificador)
    {
      var insumos = _connection.Query<Insumo>(
        ProdutoScripts.SELECT_PRODUTO_INSUMO_POR_IDENTIFICADOR,
        new { identificador })
        .ToList();

      return insumos;
    }


  }
}