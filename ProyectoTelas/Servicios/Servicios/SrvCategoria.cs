using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Model;

namespace Servicios.Servicios
{
    public class SrvCategoria
    {
        private Entities db;

        #region Método que consulta la tabla Categoria

        public List<Categoria> GetCategoria()
        {
            List<Categoria> oListCategory = new List<Categoria>();
            try
            {
                using (db = new Entities())
                {
                    oListCategory = db.Categoria.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
            return oListCategory;
        }

        #endregion

        #region Método que permite dar de alta a una nueva categoria

        public void AddCategory(Categoria item)
        {
            try
            {
                using (db = new Entities())
                {

                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion

        #region Método que permite actualizar la información de una categoría

        public void UpdateCategory(Categoria item)
        {
            try
            {
                using (db = new Entities())
                {
                    Categoria oCategory = db.Categoria.Where(x => x.CategoriaID == item.CategoriaID).FirstOrDefault();
                    if (oCategory != null)
                    {
                        oCategory.Nombre = item.Nombre;
                        oCategory.Tipo = item.Tipo;
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

        #region Método que permite eliminar la información de una categoría

        public void DeleteCategory(int id)
        {
            try
            {
                using (db = new Entities())
                {
                    Categoria oCategory = db.Categoria.Where(x => x.CategoriaID == id).FirstOrDefault();
                    db.Categoria.Remove(oCategory);
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
