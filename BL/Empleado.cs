using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    
                }

            } catch (Exception ex) {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static ML.Result GetById(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from Empleados in context.Empleadoes
                                 join Entidades in context.CatEntidadFederativas on Empleados.IdEstado equals Entidades.Id
                                 where Empleados.Id == empleado.id
                                 select new
                                 {
                                     Id = Empleados.Id,
                                     NumeroNomina = Empleados.NumeroNomina,
                                     Nombre = Empleados.Nombre,
                                     ApellidoPaterno = Empleados.ApellidoPaterno,
                                     ApellidoMaterno = Empleados.ApellidoMaterno,
                                     IdEstado = Empleados.IdEstado,
                                     Estado = Empleados.CatEntidadFederativa.Estado

                                     
                                 }
                                 );
                    result.Object = new List<object>();
                    if( query != null )
                    {
                        foreach( var item in query)
                        {
                            ML.Empleado empleadoF = new ML.Empleado();
                            empleadoF.Entidad = new ML.Entidad();
                            empleadoF.id = item.Id;
                            empleadoF.NumeroNomina = item.NumeroNomina;
                            empleadoF.Nombre = item.Nombre;
                            empleadoF.ApellidoPaterno= item.ApellidoPaterno;
                            empleadoF.ApellidoMaterno= item.ApellidoMaterno;
                            empleadoF.Entidad.Id = (int)item.IdEstado;
                            empleadoF.Entidad.Estado = item.Estado;
                            
                            result.Object = empleadoF;
                        }
                        result.Correct = true;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.InnerException.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from a in context.Empleadoes where a.Id == empleado.id select a).SingleOrDefault();
                    if (query != null)
                    {
                        query.NumeroNomina = empleado.NumeroNomina;
                        query.Nombre = empleado.Nombre;
                        query.ApellidoPaterno = empleado.ApellidoPaterno;
                        query.ApellidoMaterno = empleado.ApellidoMaterno;
                        query.IdEstado = empleado.Entidad.Id;
                        context.SaveChanges();

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false; 
                    }
                }

            }catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.InnerException.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
