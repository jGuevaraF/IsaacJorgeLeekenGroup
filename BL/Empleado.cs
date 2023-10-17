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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from empleado in context.Empleadoes
                                 join estado in context.CatEntidadFederativas on empleado.IdEstado equals estado.Id
                                 select new
                                 {
                                     id = empleado.Id,
                                     NumeroNomina = empleado.NumeroNomina,
                                     Nombre = empleado.Nombre,
                                     ApellidoPaterno = empleado.ApellidoPaterno,
                                     ApellidoMaterno = empleado.ApellidoMaterno,
                                     Id = estado.Id,
                                     Estado = estado.Estado
                                 }).ToList();

                    result.Objects = new List<object>();

                    if (query.Count > 0)
                    {


                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.id = item.id;
                            empleado.NumeroNomina = item.NumeroNomina;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;

                            empleado.Entidad = new ML.Entidad();
                            empleado.Entidad.Id = item.Id;
                            empleado.Entidad.Estado = item.Estado;

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al obtener los empleados";
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

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    //creamos un objeto de tipo DL, de mi modelo de EF 
                    DL.Empleado add = new DL.Empleado();
                    //add.Id = empleado.id;
                    add.NumeroNomina = empleado.NumeroNomina;
                    add.Nombre = empleado.Nombre;
                    add.ApellidoPaterno = empleado.ApellidoPaterno;
                    add.ApellidoMaterno = empleado.ApellidoMaterno;
                    add.IdEstado = empleado.Entidad.Id;

                    context.Empleadoes.Add(add);
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0) result.Correct = true;
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar el registro";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(int idEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from empleado in context.Empleadoes
                                 where empleado.Id == idEmpleado
                                 select empleado).SingleOrDefault(); //se hace un getByID para encontrar el registro, si lo encuentra, se guarda en query

                    if (query != null)
                    {
                        context.Empleadoes.Remove(query); // se elimina el elemento encontrado de la base
                        context.SaveChanges();
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al eliminar";
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

        public static ML.Result GetById(int idEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.IsaacJorgeLeekenGroupEntities context = new DL.IsaacJorgeLeekenGroupEntities())
                {
                    var query = (from Empleados in context.Empleadoes
                                 join Entidades in context.CatEntidadFederativas on Empleados.IdEstado equals Entidades.Id
                                 where Empleados.Id == idEmpleado
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
