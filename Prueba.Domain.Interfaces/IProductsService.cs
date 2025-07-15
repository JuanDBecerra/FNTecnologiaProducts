using Prueba.Domain.Entities.Model;

namespace Prueba.Domain.Interfaces
{
    public interface IProductsService : IGenericService<Products>
    {
        List<Products> GetAllData();
        Products Create(Products products);
        Products UpdateProduct(int id, Products products);
    }
}
