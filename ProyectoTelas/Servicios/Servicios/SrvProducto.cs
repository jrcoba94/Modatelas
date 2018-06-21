using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Model;

namespace Servicios.Servicios
{
    public class SrvProducto
    {
        private Entities db;

        #region Método que consulta la tabla producto
        public List<Producto> GetProducto()
        {
            List<Producto> oListProduct = new List<Producto>();
            try
            {
                using (db = new Entities())
                {
                    oListProduct = db.Producto.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
            return oListProduct;
        }
        #endregion

        #region Método que permite dar de alta a un nuevo producto

        public void AddProduct(Producto item)
        {
            try
            {
                using (db = new Entities())
                {
                    db.Producto.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion

        #region Método que permite actualizar la información de un producto

        public void UpdateProduct(Producto item)
        {
            try
            {
                using (db = new Entities())
                {
                    Producto oProducto = db.Producto.Where(x => x.ProductoID == item.ProductoID).FirstOrDefault();
                    if (oProducto != null)
                    {
                        oProducto.Nombre = item.Nombre;
                        oProducto.Descripcion = item.Descripcion;
                        oProducto.Caracteristicas = item.Caracteristicas;
                        oProducto.ImagenPortada = item.ImagenPortada;
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

        #region Método que permite eliminar información de un producto

        public void DeleteProducto(int id)
        {
            try
            {
                using (db = new Entities())
                {
                    Producto oProducto = db.Producto.Where(x => x.ProductoID == id).FirstOrDefault();
                    db.Producto.Remove(oProducto);
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
