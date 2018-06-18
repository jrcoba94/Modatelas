using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Model;

namespace Servicios.Servicios
{
    public class SrvPromociones
    {
        #region Método que consulta la tabla promociones

        public List<Promocion> GetPromocion()
        {
            List<Promocion> oListPromocion = new List<Promocion>();
            try
            {
                using (TelasEntities db = new TelasEntities())
                {
                    oListPromocion = db.Promocion.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
            return oListPromocion;
        }

        #endregion

        #region Método que permite dar de alta a una nueva promoción

        public void AddPromocion(Promocion item)
        {
            try
            {
                using (TelasEntities db = new TelasEntities())
                {
                    db.Promocion.Add(item);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(SrvMessages.getMessageSQL(ex));
            }
        }

        #endregion

        #region Método que permite actualizar la información de una promoción

        public void UpdatePromocion(Promocion item)
        {
            try
            {
                using (TelasEntities db = new TelasEntities())
                {
                    Promocion oPromocion = db.Promocion.Where(x => x.PromocionID == item.PromocionID).FirstOrDefault();
                    if (oPromocion != null)
                    {
                        oPromocion.Nombre = item.Nombre;
                        oPromocion.Descripcion = item.Descripcion;
                        oPromocion.FechaInicio = item.FechaInicio;
                        oPromocion.FechaFin = item.FechaFin;
                        oPromocion.ProductoID = item.ProductoID;
                        oPromocion.PrecioPromocion = item.PrecioPromocion;
                        oPromocion.Imagen = item.Imagen;
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

        #region Método que permite eliminar información de una promoción

        public void DeletePromocion(int id)
        {
            try
            {
                using (TelasEntities db = new TelasEntities())
                {
                    Promocion oPromocion = db.Promocion.Where(x => x.PromocionID == id).FirstOrDefault();
                    db.Promocion.Remove(oPromocion);
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
