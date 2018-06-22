using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Model;

namespace Servicios.Servicios
{
    public class SrvProveedor
    {
        private Entities db;

        #region Método que consulta la tabla Proveedores

        public List<Proveedor> GetProveedor()
        {
            List<Proveedor> oListProveedor = new List<Proveedor>();
            {
                try
                {
                    using (db = new Entities())
                    {
                        oListProveedor = db.Proveedor.ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(SrvMessages.getMessageSQL(ex));
                }
            }
            return oListProveedor;
        }

        public List<Proveedor> GetProveedorByID(int id)
        {
            List<Proveedor> oListProveedor = new List<Proveedor>();
            {
                try
                {
                    using (db = new Entities())
                    {
                        oListProveedor = db.Proveedor.Where(x => x.ProveedorID == id).ToList();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(SrvMessages.getMessageSQL(ex));
                }
            }
            return oListProveedor;
        }

        #endregion

        #region Método que permite dar de alta a un nuevo proveedor

        public void AddProveedor(Proveedor item)
        {
            try
            {
                using (db = new Entities())
                {
                    db.Proveedor.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion

        #region Método que permite actualizar la información de un proveedor

        public void UpdateProveedor(Proveedor item)
        {
            try
            {
                using (db = new Entities())
                {
                    Proveedor oProveedor = db.Proveedor.Where(x => x.ProveedorID == item.ProveedorID).FirstOrDefault();
                    if (oProveedor != null)
                    {
                        oProveedor.NombreProveedor = item.NombreProveedor;
                        oProveedor.RFC = item.RFC;
                        oProveedor.Folio = item.Folio;
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

        #region Método que permite eliminar la información de un proveedor

        public void DeleteProveedor(int id)
        {
            try
            {
                using (db = new Entities())
                {
                    Proveedor oProveedor = db.Proveedor.Where(x => x.ProveedorID == id).FirstOrDefault();
                    db.Proveedor.Remove(oProveedor);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion


        #region Método que consulta la tabla Contacto

        public List<Contacto> getContacto()
        {
            List<Contacto> oListContacto = new List<Contacto>();
            try
            {
                using (db = new Entities())
                {
                    oListContacto = db.Contacto.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
            return oListContacto;
        }

        #endregion

        #region Método que permite guardar la información de contacto en la BD

        public void MandarCorreo(Contacto item)
        {
            try
            {
                using (db = new Entities())
                {
                    db.Contacto.Add(item);
                    db.SaveChanges();
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
