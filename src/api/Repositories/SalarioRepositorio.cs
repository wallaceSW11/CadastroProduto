using System;
using System.Data;
using api.Factories;
using api.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Repositories
{

  public class SalarioRepositorio : IDisposable
  {
    private readonly IDbConnection _connection;

    public SalarioRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
    }

    public void Dispose()
    {
      _connection?.Dispose();
    }

    private const string SELECT = @"
        Select
          ValorCustoPorMinuto
        From
          Salario";
    public Salario Obter()
    {
      var valorCustoMinuto = _connection.QueryFirst<Salario>(SELECT);

      return valorCustoMinuto;
    }
  }
}