using Microsoft.AspNetCore.Mvc;
using Proyecto.login.Esfe.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace Proyecto.login.Esfe.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Data Source=LENOVO5;Initial Catalog = DB_ACCESO; Integrated Security = True";
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if(oUsuario.Clave == oUsuario.ConfirmarClave)
            {
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
                return View();
            }
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarrUsuario", cn);
                cmd.Parameters.AddWithValue();
            };
             return View();
        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

    }
}
