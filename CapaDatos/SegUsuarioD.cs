using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class SegUsuarioD
    {

        public List<SegUsuarioE> Obtener(SegUsuarioE Usuario, string Esquema)
        {

            try
            {
                using (SqlConnection conexion = new SqlConnection(new Conexion().conexion()))
                {
                    string query = Esquema + ".PA_CON_TBL_BBLTC_SEG_USUARIO";
                    using (SqlCommand cmd = new SqlCommand(query, conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@P_OPCION",Usuario.Opcion);
                        cmd.Parameters.AddWithValue("@P_PK_TBL_BBLTC_IDENTIFICACION",Usuario.Identificacion);
                        cmd.Parameters.AddWithValue("@P_PERFIL", Usuario.Perfil);
                        conexion.Open();
                        using (SqlDataReader reader=cmd.ExecuteReader())
                        {
                            List<SegUsuarioE> Lista = new List<SegUsuarioE>();
                            while (reader.Read())
                            {
                                Lista.Add(new SegUsuarioE()
                                {
                                    Identificacion = UtilitarioSQL.ObtieneString(reader,"IDENTIFICACION"),
                                    Nombre = UtilitarioSQL.ObtieneString(reader,"NOMBRE"),
                                    Correo = UtilitarioSQL.ObtieneString(reader,"CORREO"),
                                    Telefono = UtilitarioSQL.ObtieneString(reader,"TELEFONO"),
                                    Contrasenna = UtilitarioSQL.ObtieneString(reader,"CONTRASENNA"),
                                    Perfil = UtilitarioSQL.ObtieneInt(reader,"PERFIL")
                                }) ;

                            }
                            return Lista;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }



    }
}
