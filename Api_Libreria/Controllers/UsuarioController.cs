using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace Api_Libreria.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpPost]
        [Route("Obtener")]
        public IHttpActionResult Obtener(SegUsuarioE usuario, string Esquema)
        {
            try
            {
                List<SegUsuarioE> Lista = new SegUsuarioD().Obtener(usuario, Esquema);
                if (Lista != null)
                {
    
                        return Ok(Lista);
                  
                }
                else
                {
                    return  NotFound();
                }
            }
            catch (Exception ex)
            {

               return NotFound();
            }

        }


    }
}
