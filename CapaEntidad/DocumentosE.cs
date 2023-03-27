using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DocumentosE : BaseE
    {
        public string Clave { get; set; }
        public string Consecutivo { get; set; }
        public byte[] pdf { get; set; }
        public string ID_Emisor { get; set; }
        public string Nombre_emisor { get; set; }
        public decimal Monto { get; set; }
        public string Monto_string { get; set; }
        public string Tipo_Moneda { get; set; }
        public DateTime Fecha_Registrado { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Final { get; set; }
        public string EmisorBusqueda { get; set; }
        //uso de funcionalidad
        public int FK_Departamento { get; set; }
        //solo para busqueda
        public int fkDepartamentoBusqueda { get; set; }
        //propiedad de los departamentos para saber cuantas aprobaciones realizar
        public int Cantidad_Aprobaciones { get; set; }
        //para traer la categoria a la que fue asignada en la tabla ASIGNACION_DOCUMENTO
        public int FK_Categoria { get; set; }
        public string Categoria_Descripcion { get; set; }
        public string Departamento_Descripcion { get; set; }
        public bool Requiere_Embarque { get; set; }
        public string Requiere_Embarque_Descripcion { get; set; }
        // Se utiliza para definir el usuario que asigno los documentos para que entre al proceso de aprobación.
        public string ID_Usuario_Asignador { get; set; }
        //Nivel del usuario aprobador
        public string Nivel_Usuario_Aprobador { get; set; }
        //vartiable se utiliza para obtener los documentos con el estado de asignado 
        public string EstadoAsignado { get; set; }
        public string Posicion_Firma { get; set; }
        //usuario que firmó por primera vez el documento
        public string Usuario_Primer_Firma { get; set; }


        //usuario al que se le asigno la aprobacion del documento
        public string FK_TBL_USUARIO_ASIGNADO { get; set; }
        public int Prioridad { get; set; }


        //nivel primer firma del departamento
        public int Nivel_Primer_Firma { get; set; }

        // buzones
        public string Buzon { get; set; }

        // ultimo nivel en aprobar el documento
        public int Nivel_Aprobado { get; set; }
    }
}
