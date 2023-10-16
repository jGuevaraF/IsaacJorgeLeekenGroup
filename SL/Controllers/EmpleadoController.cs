using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/empleado")]
    public class EmpleadoController : ApiController
    {

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idEmpleado}")]
        [HttpDelete]
        public IHttpActionResult Delete(int idEmpleado)
        {
            ML.Result result = BL.Empleado.Delete(idEmpleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{idEmpleado}")]
        [HttpGet]
        public IHttpActionResult GetById(int idEmpleado)
        {
            ML.Result result = BL.Empleado.GetById(idEmpleado );
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [Route("{IdEmpleado}")]
        [HttpPut]
        public IHttpActionResult Update(int IdEmpleado, [FromBody] ML.Empleado empleado)
        {
            empleado.id = IdEmpleado;
            ML.Result result = BL.Empleado.Update(empleado);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }





    }
}
