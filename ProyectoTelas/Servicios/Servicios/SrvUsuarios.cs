using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Model;

namespace Servicios.Servicios
{
    public class SrvUsuarios
    {
        private Entities db;

        #region Método que consulta la tabla Usuarios

        public List<Usuario> GetUsuarios()
        {
            List<Usuario> oListUsuario = new List<Usuario>();
            try
            {
                using (db = new Entities())
                {
                    oListUsuario = db.Usuario.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
            return oListUsuario;
        }

        #endregion

        #region Método que permite dar de alta a un usuario

        public void AddUser(Usuario item)
        {
            try
            {
                using (db = new Entities())
                {
                    db.Usuario.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion

        #region Método que permite actualizar la información de un usuario

        public void UpdateUser(Usuario item)
        {
            try
            {
                using (db = new Entities())
                {
                    Usuario oUsuario = db.Usuario.Where(x => x.UsuarioID == item.UsuarioID).FirstOrDefault();
                    if (oUsuario != null)
                    {
                        oUsuario.NombreUsuario = item.NombreUsuario;
                        oUsuario.Contrasenia = item.Contrasenia;
                        oUsuario.ApellidoPaterno = item.ApellidoPaterno;
                        oUsuario.ApellidoMaterno = item.ApellidoMaterno;
                        oUsuario.CorreoElectronico = item.CorreoElectronico;
                        oUsuario.Direccion = item.Direccion;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion
    }
}
