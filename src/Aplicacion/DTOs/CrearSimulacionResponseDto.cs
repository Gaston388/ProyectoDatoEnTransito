using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class SimulacionResponseDto
    {
        public int Id { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int TtlFinal { get; set; }
        public double LatenciaTotalMs { get; set; }
    }
}