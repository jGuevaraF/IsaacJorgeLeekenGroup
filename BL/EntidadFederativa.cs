using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EntidadFederativa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from Entidad in context.CatEntidadFederativas
                                select new
                                {
                                    Id = Entidad.Id,
                                    Estado = Entidad.Estado,
                                });
                    if (query != null )
                    {
                        result.Objects = new List<object>();
                        foreach( var obj in query)
                        {
                            ML.Entidad entidad = new ML.Entidad();
                            entidad.Id = obj.Id;
                            entidad.Estado = obj.Estado;

                            result.Objects.Add(entidad);
                        }
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
