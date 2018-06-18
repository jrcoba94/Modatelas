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
        #region Método que consulta la tabla Proveedores

        public List<Proveedor> GetProveedor()
        {
            List<Proveedor> oListProveedor = new List<Proveedor>();
            {
                try
                {
                    using (TelasEntities db = new TelasEntities())
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

        #endregion

        #region Método que permite dar de alta a un nuevo proveedor

        public void AddProveedor(Proveedor item)
        {
            try
            {
                using (TelasEntities db = new TelasEntities())
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
                using (TelasEntities db = new TelasEntities())
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
                using (TelasEntities db = new TelasEntities())
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
    }
}
