using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUsername(string IdUsername)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DpascualAlfaSolucionesContext context = new DL.DpascualAlfaSolucionesContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByIdUser '{IdUsername}' ").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        
                        usuario.Clave = query.Clave;
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Username = query.Username;


                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = " Ocurrio un error ";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
