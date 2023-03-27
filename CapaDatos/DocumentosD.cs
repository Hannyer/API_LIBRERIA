using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DocumentosD
    {
        public string Key { get; set; } = "Ruta_Conexion_1";
        public string Esquema { get; set; } = "DELORO";
        public string Disminutivo_Moneda_Colones { get; set; } = "CRC";
        public string Disminutivo_Moneda_Euro { get; set; } = "EUR";
        public List<DocumentosE> Obtener(DocumentosE p_objeto)
        {
            Conexion con = new Conexion();
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(con.conexion(Key).ToString());
                string Sql = this.Esquema + ".PA_CON_TBL_MTRA_DOCUMENTOS";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_OPCION ", p_objeto.Opcion);
                cmd.Parameters.AddWithValue("@P_USUARIO", p_objeto.Usuario);
                cmd.Parameters.AddWithValue("@P_PK_TBL_MTRA_DOCUMENTOS", p_objeto.Clave);
                cmd.Parameters.AddWithValue("@P_CONSECUTIVO", p_objeto.Consecutivo);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", p_objeto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", p_objeto.Fecha_Final);
                cmd.Parameters.AddWithValue("@P_EMISOR", p_objeto.EmisorBusqueda);

                myConexion.Open();
                reader = cmd.ExecuteReader();
                List<DocumentosE> lista = new List<DocumentosE>();

                while (reader.Read())
                {
                    DocumentosE obObjeto = new DocumentosE();
                    obObjeto.Clave = UtilitarioSQL.ObtieneNumeroString(reader, "clave");
                    obObjeto.Consecutivo = UtilitarioSQL.ObtieneString(reader, "consecutivo");
                    obObjeto.pdf = ((byte[])reader["pdf"]);
                    obObjeto.ID_Emisor = UtilitarioSQL.ObtieneString(reader, "emisor");
                    obObjeto.Nombre_emisor = UtilitarioSQL.ObtieneString(reader, "nombreemisor");
                    obObjeto.Fecha_Registrado = UtilitarioSQL.ObtieneDateTime(reader, "registrado_el");
                    obObjeto.Cantidad_Aprobaciones = UtilitarioSQL.ObtieneInt(reader, "CANTIDAD_APROBACION");
                    obObjeto.Prioridad = UtilitarioSQL.ObtieneInt(reader, "PRIORIDAD");
                    if (UtilitarioSQL.ObtieneString(reader, "DEPARTAMENTO_DESCRIPCION").Equals(String.Empty))
                    {
                        obObjeto.Departamento_Descripcion = "Sin asignar";
                    }
                    else
                    {
                        obObjeto.Departamento_Descripcion = UtilitarioSQL.ObtieneString(reader, "DEPARTAMENTO_DESCRIPCION");
                    }
                    if (UtilitarioSQL.ObtieneString(reader, "REQUIERE_EMBARQUE").Equals("1"))
                    {
                        obObjeto.Requiere_Embarque_Descripcion = "Sí";
                        obObjeto.Requiere_Embarque = true;
                    }
                    else
                    {
                        obObjeto.Requiere_Embarque_Descripcion = "No";
                        obObjeto.Requiere_Embarque = false;
                    }
                    if (UtilitarioSQL.ObtieneString(reader, "CATEGORIA_DESCRIPCION").Equals(String.Empty))
                    {
                        obObjeto.Categoria_Descripcion = "Sin asignar";
                    }
                    else
                    {
                        obObjeto.Categoria_Descripcion = UtilitarioSQL.ObtieneString(reader, "CATEGORIA_DESCRIPCION");
                    }
                    if (UtilitarioSQL.ObtieneString(reader, "NIVEL_APROBADO").Equals("-1"))
                    {
                        obObjeto.Departamento_Descripcion = "Cancelado";
                    }

                    lista.Add(obObjeto);
                }
                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos al obtener los documentos " + ex.Message);
            }
        }
        public List<DocumentosE> ObtenerAsignados(DocumentosE p_objeto)
        {
            Conexion con = new Conexion();
            SqlConnection myConexion = null;
            SqlCommand cmd = null;
            SqlDataReader reader = null;
            try
            {
                myConexion = new SqlConnection(con.conexion(Key).ToString());
                string Sql = this.Esquema + ".PA_CON_TBL_MTRA_APROBACION_DOCUMENTO";
                cmd = new SqlCommand(Sql, myConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@P_OPCION ", p_objeto.Opcion);
                cmd.Parameters.AddWithValue("@P_USUARIO", p_objeto.Usuario);
                cmd.Parameters.AddWithValue("@P_PK_TBL_MTRA_APROBACION_DOCUMENTO", p_objeto.Clave);
                cmd.Parameters.AddWithValue("@P_PK_TBL_MTRA_DEPARTAMENTO", p_objeto.fkDepartamentoBusqueda);
                cmd.Parameters.AddWithValue("@P_CONSECUTIVO", p_objeto.Consecutivo);
                cmd.Parameters.AddWithValue("@P_FECHA_INICIO", p_objeto.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@P_FECHA_FINAL", p_objeto.Fecha_Final);
                cmd.Parameters.AddWithValue("@P_EMISOR", p_objeto.EmisorBusqueda);
                cmd.Parameters.AddWithValue("@P_ESTADO_ASIGNADO", p_objeto.EstadoAsignado);
                cmd.Parameters.AddWithValue("@P_FK_TBL_USUARIO_ASIGNADO", p_objeto.FK_TBL_USUARIO_ASIGNADO);
                cmd.Parameters.AddWithValue("@P_PRIORIDAD", p_objeto.Prioridad);
                cmd.Parameters.AddWithValue("@P_BUZON", p_objeto.Buzon);
                myConexion.Open();
                reader = cmd.ExecuteReader();
                List<DocumentosE> lista = new List<DocumentosE>();
                while (reader.Read())
                {
                    DocumentosE obObjeto = new DocumentosE();
                    obObjeto.Clave = UtilitarioSQL.ObtieneNumeroString(reader, "clave");
                    //se utiliza el usuario de creación para capturar al que asigna
                    obObjeto.ID_Usuario_Asignador = UtilitarioSQL.ObtieneString(reader, "USUARIO_CREACION");
                    obObjeto.Consecutivo = UtilitarioSQL.ObtieneString(reader, "consecutivo");
                    obObjeto.pdf = ((byte[])reader["pdf"]);
                    obObjeto.ID_Emisor = UtilitarioSQL.ObtieneString(reader, "emisor");
                    obObjeto.Nombre_emisor = UtilitarioSQL.ObtieneString(reader, "nombreemisor");
                    obObjeto.Fecha_Registrado = UtilitarioSQL.ObtieneDateTime(reader, "registrado_el");
                    obObjeto.FK_Departamento = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_MTRA_DEPARTAMENTO");
                    obObjeto.Cantidad_Aprobaciones = UtilitarioSQL.ObtieneInt(reader, "NIVEL_APROBADO");
                    obObjeto.FK_Categoria = UtilitarioSQL.ObtieneInt(reader, "FK_TBL_MTRA_CATEGORIA");
                    obObjeto.Posicion_Firma = UtilitarioSQL.ObtieneString(reader, "POSICION_FIRMA");
                    obObjeto.Usuario_Primer_Firma = UtilitarioSQL.ObtieneString(reader, "USUARIO_PRIMER_FIRMA");
                    obObjeto.Nivel_Primer_Firma = UtilitarioSQL.ObtieneInt(reader, "NIVEL_PRIMER_FIRMA");
                    obObjeto.Prioridad = UtilitarioSQL.ObtieneInt(reader, "PRIORIDAD");
                    obObjeto.Monto = UtilitarioSQL.ObtieneDecimal(reader, "monto");
                    obObjeto.Tipo_Moneda = UtilitarioSQL.ObtieneString(reader, "moneda").ToUpper();
                    NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
                    numberFormatInfo.NumberDecimalSeparator = ".";
                    numberFormatInfo.CurrencyDecimalSeparator = ".";
                    numberFormatInfo.NumberGroupSeparator = ",";
                    numberFormatInfo.CurrencyGroupSeparator = ",";
                    if (obObjeto.Tipo_Moneda.ToLower().Equals(Disminutivo_Moneda_Colones.ToLower()))
                    {
                        obObjeto.Monto_string = Math.Round(obObjeto.Monto, 2).ToString("₡ #,###,###.00", numberFormatInfo);
                    }
                    else
                    {
                        if (obObjeto.Tipo_Moneda.ToLower().Equals(Disminutivo_Moneda_Euro.ToLower()))
                        {
                            obObjeto.Monto_string = Math.Round(obObjeto.Monto, 2).ToString("€ #,###,###.00", numberFormatInfo);
                        }
                        else
                        {
                            obObjeto.Monto_string = Math.Round(obObjeto.Monto, 2).ToString("$ #,###,###.00", numberFormatInfo);
                        }
                    }
                    if (UtilitarioSQL.ObtieneString(reader, "DEPARTAMENTO_DESCRIPCION").Equals(String.Empty))
                    {
                        obObjeto.Departamento_Descripcion = "Sin asignar";
                    }
                    else
                    {
                        obObjeto.Departamento_Descripcion = UtilitarioSQL.ObtieneString(reader, "DEPARTAMENTO_DESCRIPCION");
                    }
                    if (UtilitarioSQL.ObtieneString(reader, "REQUIERE_EMBARQUE").Equals("1"))
                    {
                        obObjeto.Requiere_Embarque_Descripcion = "Sí";
                        obObjeto.Requiere_Embarque = true;
                    }
                    else
                    {
                        obObjeto.Requiere_Embarque_Descripcion = "No";
                        obObjeto.Requiere_Embarque = false;
                    }
                    int va1 = UtilitarioSQL.ObtieneInt(reader, "NIVEL_APROBADO");
                    int va2 = (UtilitarioSQL.ObtieneInt(reader, "CANT_APROBACIONES"));
                    obObjeto.Nivel_Aprobado = va1;
                    if ((UtilitarioSQL.ObtieneInt(reader, "NIVEL_APROBADO")) != (UtilitarioSQL.ObtieneInt(reader, "CANT_APROBACIONES")))
                    {
                        obObjeto.Nivel_Usuario_Aprobador = (UtilitarioSQL.ObtieneInt(reader, "NIVEL_APROBADO") + 1).ToString();
                    }
                    else
                    {
                        obObjeto.Nivel_Usuario_Aprobador = "Presidente";
                    }
                    lista.Add(obObjeto);
                }
                reader.Dispose();
                cmd.Dispose();
                myConexion.Close();
                myConexion.Dispose();
                return lista;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error en Base de Datos al obtener los documentos " + ex.Message);
            }
        }
    }
}
