using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace api.Models
{
  public class Salario
  {
    [Key]
    public string Identificador { get; set; }
    public float ValorSalario { get; set; }
    public int TotalHorasPorDia { get; set; }
    public int TotalDiasPorSemana { get; set; }
    public int TotalHorasPorSemana { get; set; }
    public int TotalHorasMes { get; set; }
    public float ValorCustoPorHora { get; set; }
    public float ValorCustoPorMinuto { get; set; }
    public float ValorTotalDespesa { get; set; }

  }
}
