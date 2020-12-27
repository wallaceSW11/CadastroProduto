using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
  [Route("api/v1/salario")]
  public class SalarioController : ControllerBase
  {
    private readonly SalarioRepositorio _repositorio;

    public SalarioController(SalarioRepositorio repositorio)
    {
      _repositorio = repositorio;
    }

    [HttpGet()]
    public Salario Obter()
    {
      return _repositorio.Obter();
    }
  }
}