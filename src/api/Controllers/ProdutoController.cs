using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepositorio _repositorio;

        public ProdutoController(ProdutoRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet("{identificador}")]
        public Produto Obter([FromRoute] int identificador)
        {
            return _repositorio.Obter(identificador);
        }
    }
}