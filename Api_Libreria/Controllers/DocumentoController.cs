using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api_Libreria.Controllers
{
    public class DocumentoController : ApiController
    {

        [HttpPost]
        [Route("ObtenerDocumentos")]
        public IHttpActionResult Obtener(DocumentosE Documento)
        {
            try
            {
                List<DocumentosE> Lista = new DocumentosD().Obtener(Documento);
                if (Lista != null)
                {

                    return Ok(Lista);

                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {

                return NotFound();
            }

        }

    }
}