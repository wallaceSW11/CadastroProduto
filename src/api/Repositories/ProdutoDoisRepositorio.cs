using System;
using System.Data;
using api.Factories;
using api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Dapper.Contrib;
using api.Repositories.Scripts;
using System.Collections.Generic;

namespace api.Repositories
{
  public class ProdutoDoisRepositorio : IDisposable
  {
    private readonly IDbConnection _connection;
    private readonly InsumoRepositorio _InsumoRepositorio;

    public ProdutoDoisRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
      _InsumoRepositorio = new InsumoRepositorio(factory);
    }

    public void Dispose()
    {
      _connection?.Dispose();
      _InsumoRepositorio?.Dispose();
    }

    public IEnumerable<ProdutoDois> Listar()
    {
      return _connection.Query<ProdutoDois>(ProdutoScripts.SELECT_ALL_PRODUTOS);
    }

    public ProdutoDois Obter(int identificador)
    {
      var produtoDois = _connection.QuerySingle<ProdutoDois>(ProdutoScripts.SELECT_PRODUTO_POR_IDENTIFICADOR, new { identificador });
      if (produtoDois.PossuiComposicao)
      {
        produtoDois.Insumos = _InsumoRepositorio.Obter(identificador);
      }

      return produtoDois;
    }

    public string Criar(ProdutoDois produto)
    {
      try
      {
        var idNovo = _connection.QuerySingle<string>(ProdutoScripts.INSERT_PRODUTO, produto);

        if (produto.PossuiComposicao)
        {
          foreach (Insumo insumo in produto.Insumos)
          {
            insumo.IdentificadorProdutoPrincipal = idNovo;
          }
          _connection.Execute(ProdutoScripts.INSERT_INSUMO, produto.Insumos);
        }

        return idNovo;
      }
      catch (SqlException ex)
      {
        return ex.Message;
      }
    }

    public string Atualizar(ProdutoDois produto)
    {
      try
      {
        var produtosAtualizados = _connection
          .Execute(
            ProdutoScripts.UPDATE_PRODUTO,
            new
            {
              identificador = produto.Identificador,
              descricao = produto.Descricao,
              valorVenda = produto.ValorVenda,
              possuiComposicao = produto.PossuiComposicao
            });

        if (produto.PossuiComposicao)
        {
          _connection.Execute(ProdutoScripts.DELETE_INSUMO, new { identificador = produto.Identificador });

          // foreach (Insumo insumo in produto.Insumos)
          // {
          //   insumo.IdentificadorProdutoPrincipal = produto.Identificador;
          // }
          _connection.Execute(ProdutoScripts.INSERT_INSUMO, produto.Insumos);
        }

        if (produtosAtualizados > 0)
        {
          return "Produto atualizado com sucesso.";
        }

        return "Nenhum produto foi localizado com o identificador informado.";
      }
      catch (SqlException ex)
      {
        return ex.Message;
      }
    }





  }
}