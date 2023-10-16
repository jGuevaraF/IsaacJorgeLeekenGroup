using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    [RoutePrefix("api/entidad")]
    public class EntidadController : ApiController
    {
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll(ML.Entidad entidad)
        {

            ML.Result result = BL.EntidadFederativa.GetAll();
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
