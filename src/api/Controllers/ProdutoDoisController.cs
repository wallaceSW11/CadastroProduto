using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Controllers
{
  [Route("api/v1/produtosDois")]
  public class ProdutoDoisController : ControllerBase
  {
    private readonly ProdutoDoisRepositorio _repositorio;

    public ProdutoDoisController(ProdutoDoisRepositorio repositorio)
    {
      _repositorio = repositorio;
    }

    [HttpGet()]
    public IEnumerable<ProdutoDois> Listar()
    {
      return _repositorio.Listar();
    }
    [HttpGet("{identificador}")]
    public ProdutoDois Obter([FromRoute] int identificador)
    {
      return _repositorio.Obter(identificador);
    }


    [HttpPost()]
    public IActionResult Criar([FromBody] ProdutoDois produto)
    {
      if (ModelState.IsValid)
      {
        var idProdutoCadastrado = _repositorio.Criar(produto);

        int resultado;

        if (!int.TryParse(idProdutoCadastrado, out resultado))
        {
          return BadRequest("Falha no cadastro do produto. Mensagem original: " + idProdutoCadastrado);
        }

        return Ok("Identificador: " + idProdutoCadastrado);
      }

      return BadRequest(ModelState);
    }

    [HttpPut()]
    public IActionResult Atualizar([FromBody] ProdutoDois produto)
    {
      if (ModelState.IsValid)
      {
        var produtosAtualizados = _repositorio.Atualizar(produto);

        return Ok(produtosAtualizados);
      }

      return BadRequest("Falha ao atualizar o produto." + ModelState);
    }

    [HttpDelete("{identificador}")]
    public IActionResult Deletar([FromRoute] int identificador)
    {
      return Ok("Desculpe, método ainda não implementado.");
    }


    // Calculos \\

    [HttpPost("/Calcular/CustoReposicao")]
    public IActionResult ObterCustoReposicao([FromBody] CustoReposicao custoReposicao)
    {
      if (ModelState.IsValid)
      {
        CustoReposicao custoCalculado = new CustoReposicao(custoReposicao);

        return Ok(custoCalculado);

      }
      else
      {
        return BadRequest(ModelState);
      }
    }

    [HttpPost("/Calcular/Montagem")]
    public IActionResult ObterValorTotalMontagem([FromBody] Montagem montagem)
    {
      if (ModelState.IsValid)
      {
        Montagem valorMontagem = new Montagem(montagem);

        return Ok(valorMontagem);

      }
      else
      {
        return BadRequest(ModelState);
      }
    }



  }
}