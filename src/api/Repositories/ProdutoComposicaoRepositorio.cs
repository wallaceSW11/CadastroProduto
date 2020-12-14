using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using api.Factories;
using api.Models;
using Dapper;

namespace api.Repositories
{
    internal class ProdutoComposicaoRepositorio : IDisposable
    {
        private const string SELECT = @"
            SELECT
              ProdutoComposicaoId AS Identificador,
              Quantidade,
              ProdutoId,
              ProdutoId AS Identificador,
              Descricao,
              ValorVenda
            FROM
              ProdutoComposicao (NoLock) INNER JOIN Produto (NoLock) ON (ProdutoComposicao.ProdutoComposicaoId = Produto.Identificador)
            WHERE
              ProdutoComposicao.ProdutoId = @identificadorProduto
        ";

        private readonly IDbConnection _connection;

        internal ProdutoComposicaoRepositorio(DatabaseConnectionFactory factory)
        {
            _connection = factory.Create();   
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public IEnumerable<ProdutoComposicao> Obter(Produto produto)
        {
            return _connection
                .Query<ProdutoComposicao, Produto, ProdutoComposicao>(
                    SELECT, 
                    (produtoComposicao, produto) => 
                    {
                        produtoComposicao.Produto = produto;
                        return produtoComposicao;
                    },
                    splitOn: "ProdutoId",
                    param: new 
                    { 
                        identificadorProduto = produto.Identificador 
                    })
                    .Distinct()
                    .ToList();
        }
    }
}