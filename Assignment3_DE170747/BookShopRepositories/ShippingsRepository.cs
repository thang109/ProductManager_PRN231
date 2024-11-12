using BookShopBusiness;
using BookShopDataAccess;
using System.Collections.Generic;

namespace BookShopRepository
{
    public class ShippingsRepository : IShippingsRepository
    {
        public void DeleteShipping(Shippings shipping) => ShippingsDAO.DeleteShipping(shipping);
        public void SaveShipping(Shippings shipping) => ShippingsDAO.InsertShipping(shipping);
        public void UpdateShipping(Shippings shipping) => ShippingsDAO.UpdateShipping(shipping);
        public List<Shippings> GetAllShippings() => ShippingsDAO.GetShippings();
        public Shippings GetShippingById(int id) => ShippingsDAO.GetShippingById(id);
    }
}
