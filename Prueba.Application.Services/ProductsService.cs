using Prueba.Domain.Interfaces;
using Prueba.Domain.Entities.Model;
using Microsoft.EntityFrameworkCore;

namespace Prueba.Application.Services
{
    public class ProductsService : GenericService<Products>, IProductsService
    {
        public ProductsService(IRepository<Products> repository, IMediatorService mediator) : base(repository, mediator) { }

        public List<Products> GetAllData()
        {
            return Get()
                .Include(s => s.Status)
                .Include(c => c.Category)
                .ToList();
        }

        public Products Create(Products products)
        {
            var status = _mediator.GetStatus().Any(s => s.Sta_Id == products.Pro_StatusId);
            var category = _mediator.GetCategory().Any(s => s.Cat_Id == products.Pro_StatusId);

            if (!status) throw new NullReferenceException("Estado de producto no valido.");
            if (!category) throw new NullReferenceException("Categoría  de producto no valido.");

            return Add(products);
        }

        public Products UpdateProduct(int id, Products products)
        {
            var product = GetById(id) ?? throw new NullReferenceException("No existe producto.");
            var status = _mediator.GetStatus().Any(s => s.Sta_Id == products.Pro_StatusId);
            var category = _mediator.GetCategory().Any(s => s.Cat_Id == products.Pro_StatusId);

            if (!status) throw new NullReferenceException("Estado de producto no valido.");
            if (!category) throw new NullReferenceException("Categoría  de producto no valido.");

            return Update(id, products);
        }
    }
}
