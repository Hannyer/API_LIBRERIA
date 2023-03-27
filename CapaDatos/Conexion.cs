using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CapaDatos
{

    public class Conexion
    {
        protected static ArrayList CargarPreferencias(string Key_Compannia)
        {
            ArrayList datos = new ArrayList();
            string direccion = ConfigurationManager.AppSettings[Key_Compannia];
            try
            {
                if (File.Exists(direccion))
                {
                    XmlDocument Conexiones = new XmlDocument();
                    Conexiones.Load(direccion);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/SERVER").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/DATABASE").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/USER").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/PASSWORD").InnerText);
                }
                else
                {
                }
            }
            catch
            {
            }
            return datos;
        }
        protected static ArrayList CargarPreferencias()
        {
            ArrayList datos = new ArrayList();
            string direccion = ConfigurationManager.AppSettings["Ruta_Conexion_Default"];
            try
            {
                if (File.Exists(direccion))
                {
                    XmlDocument Conexiones = new XmlDocument();
                    Conexiones.Load(direccion);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/SERVER").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/DATABASE").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/USER").InnerText);
                    datos.Add(Conexiones.SelectSingleNode("/CONEXION/PASSWORD").InnerText);
                }
                else
                {
                }
            }
            catch
            {
            }
            return datos;
        }
        public string conexion(string Key_Compannia)
        {
            ArrayList Conexion = new ArrayList();
            Conexion = CargarPreferencias(Key_Compannia);
            return "Data Source=" + Conexion[0].ToString() +
            ";Initial Catalog=" + Conexion[1].ToString() +
            ";User ID=" + Conexion[2].ToString() +
            ";Password=" + Conexion[3].ToString();
        }
        public string conexion()
        {
            ArrayList Conexion = new ArrayList();
            Conexion = CargarPreferencias();
            return "Data Source=" + Conexion[0].ToString() +
            ";Initial Catalog=" + Conexion[1].ToString() +
            ";User ID=" + Conexion[2].ToString() +
            ";Password=" + Conexion[3].ToString();
        }
    }
}
