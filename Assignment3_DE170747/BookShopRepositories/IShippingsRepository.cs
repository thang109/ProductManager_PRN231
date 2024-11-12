using BookShopBusiness;
using System.Collections.Generic;

namespace BookShopRepository
{
    public interface IShippingsRepository
    {
        List<Shippings> GetAllShippings();
        Shippings GetShippingById(int id);
        void SaveShipping(Shippings shipping);
        void UpdateShipping(Shippings shipping);
        void DeleteShipping(Shippings shipping);
    }
}
