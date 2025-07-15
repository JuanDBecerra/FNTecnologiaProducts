using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Prueba.Adapters.API.Controllers;
using Prueba.Domain.Entities.Model;
using Prueba.Domain.Interfaces;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Request;
using Microsoft.AspNetCore.Mvc;


namespace Prueba.UnitTest.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsService> _productServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductsService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<ProductsController>>();
            _controller = new ProductsController(
                _productServiceMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public void GetById_ReturnsOk_WithValidId()
        {
            // Arrange
            var product = new Products()
            {
                Pro_Id = 1,
                Pro_Name = "Producto de prueba",
                Pro_Description = "Este es un producto de prueba para test unitario",
                Pro_Price = 999.99m,
                Pro_CategoryId = 2,
                Pro_StatusId = 1
            };
            var dto = new ProductsDto()
            {
                Id = 1,
                Name = "Producto de prueba",
                Description = "Este es un producto de prueba para test unitario",
                Price = 999.99m,
                CategoryId = 2,
                StatusId = 1
            };

            _productServiceMock.Setup(s => s.GetById(1)).Returns(product);
            _mapperMock.Setup(m => m.Map<ProductsDto>(product)).Returns(dto);

            // Act
            var result = _controller.Get(1) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(dto, result.Value);
        }

        [Fact]
        public void GetAll_ReturnsListOfProducts()
        {
            var list = new List<Products>()
            {
                new Products
                {
                    Pro_Id = 1,
                    Pro_Name = "Laptop Lenovo",
                    Pro_Description = "Laptop de alto rendimiento para desarrollo",
                    Pro_Price = 3500000m,
                    Pro_CategoryId = 1,
                    Pro_StatusId = 1
                },
            new Products
                {
                    Pro_Id = 2,
                    Pro_Name = "Mouse Inalámbrico",
                    Pro_Description = "Mouse ergonómico con conexión Bluetooth",
                    Pro_Price = 85000m,
                    Pro_CategoryId = 2,
                    Pro_StatusId = 2
                }
            };
            var dtos = new List<ProductsDto> { new ProductsDto { Id = 1 }, new ProductsDto { Id = 2 } };

            _productServiceMock.Setup(s => s.GetAllData()).Returns(list);
            _mapperMock.Setup(m => m.Map<List<ProductsDto>>(list)).Returns(dtos);

            var result = _controller.GetAll() as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(dtos, result.Value);
        }

        [Fact]
        public void Create_ReturnsOk_WhenValid()
        {
            var request = new ProductRequest { Name = "Nuevo" };
            var entity = new Products () { Pro_Id = 1, Pro_Name = "Producto de prueba", Pro_Description = "Este es un producto de prueba para test unitario", Pro_Price = 999.99m, Pro_CategoryId = 2, Pro_StatusId = 1 };
            var dto = new ProductsDto { Id = 5, Name = "Nuevo" };

            _mapperMock.Setup(m => m.Map<Products>(request)).Returns(entity);
            _productServiceMock.Setup(s => s.Create(entity)).Returns(entity);
            _mapperMock.Setup(m => m.Map<ProductsDto>(entity)).Returns(dto);

            var result = _controller.Create(request) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(dto, result.Value);
        }

        [Fact]
        public void Update_ReturnsOk_WhenValid()
        {
            var id = 1;
            var request = new ProductRequest { Name = "Editado" };
            var entity = new Products () { Pro_Id = 1, Pro_Name = "Producto de prueba", Pro_Description = "Este es un producto de prueba para test unitario", Pro_Price = 999.99m, Pro_CategoryId = 2, Pro_StatusId = 1 };
            var dto = new ProductsDto { Id = id, Name = "Editado" };

            _mapperMock.Setup(m => m.Map<Products>(request)).Returns(entity);
            _productServiceMock.Setup(s => s.UpdateProduct(id, entity)).Returns(entity);
            _mapperMock.Setup(m => m.Map<ProductsDto>(entity)).Returns(dto);

            var result = _controller.Update(id, request) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(dto, result.Value);
        }

        [Fact]
        public void Delete_ReturnsOk()
        {
            var id = 3;
            _productServiceMock.Setup(s => s.Delete(id)).Returns(true);

            var result = _controller.Delete(id) as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(true, result.Value);
        }
    }
}
