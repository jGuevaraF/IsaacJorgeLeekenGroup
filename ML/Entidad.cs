using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Entidad
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public List<object> Entidades { get; set; }
    }
}
