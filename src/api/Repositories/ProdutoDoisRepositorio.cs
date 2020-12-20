using System;
using System.Data;
using api.Factories;
using api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Dapper.Contrib;
using api.Repositories.Scripts;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace api.Repositories
{
  public class ProdutoDoisRepositorio : IDisposable
  {
    private readonly IDbConnection _connection;
    private readonly InsumoRepositorio _InsumoRepositorio;

    private readonly CustoReposicaoProdutoRepositorio _CustoReposicaoProdutoRepositorio;

    public ProdutoDoisRepositorio(DatabaseConnectionFactory factory)
    {
      _connection = factory.Create();
      _InsumoRepositorio = new InsumoRepositorio(factory);
      _CustoReposicaoProdutoRepositorio = new CustoReposicaoProdutoRepositorio(factory);
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

      produtoDois.CustoReposicao = _CustoReposicaoProdutoRepositorio.Obter(identificador);

      return produtoDois;
    }

    public string Criar(ProdutoDois produto)
    {
      using (var transaction = new TransactionScope())
      {
        try
        {

          var identificadorProdutoCadastrado = _connection.QuerySingle<string>(ProdutoScripts.INSERT_PRODUTO, produto);

          if (produto.PossuiComposicao)
          {
            foreach (Insumo insumo in produto.Insumos)
            {
              insumo.IdentificadorProdutoPrincipal = identificadorProdutoCadastrado;
            }

            _connection.Execute(ProdutoScripts.INSERT_INSUMO, produto.Insumos);
          }

          foreach (CustoReposicaoProduto custoReposicaoItem in produto.CustoReposicao)
          {
            custoReposicaoItem.IdentificadorProduto = identificadorProdutoCadastrado;
          }

          _connection.Execute(ProdutoScripts.INSERT_CUSTO_REPOSICAO, produto.CustoReposicao);

          transaction.Complete();

          return identificadorProdutoCadastrado;

        }
        catch (SqlException ex)
        {
          return ex.Message;
        }
      }
    }

    public string Atualizar(ProdutoDois produto)
    {
      using (TransactionScope transaction = new TransactionScope())
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
                tempoMontagem = produto.TempoMontagem,
                valorCustoMontagem = produto.ValorCustoMontagem,
                valorTotalCustoMontagem = produto.ValorTotalCustoMontagem,
                possuiComposicao = produto.PossuiComposicao
              });

          if (produto.PossuiComposicao)
          {
            _connection.Execute(ProdutoScripts.DELETE_INSUMO, new { identificador = produto.Identificador });
            _connection.Execute(ProdutoScripts.INSERT_INSUMO, produto.Insumos);
          }

          if (produtosAtualizados > 0)
          {
            transaction.Complete();
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
}