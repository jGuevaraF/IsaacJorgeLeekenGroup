using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int id { get; set; }
        public string NumeroNomina { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public ML.Entidad Entidad { get; set; }

        public List<object> Empleados { get; set; }
    }
}
