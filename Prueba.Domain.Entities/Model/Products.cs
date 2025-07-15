namespace Prueba.Domain.Entities.Model
{
    public class Products
    {
        public int Pro_Id { get; set; }
        public string Pro_Name { get; set; } = string.Empty;
        public string Pro_Description { get; set; } = string.Empty;
        public decimal Pro_Price { get; set; }
        public int Pro_CategoryId { get; set; }
        public int Pro_StatusId { get; set; }

        public Category? Category { get; set; }
        public Status? Status { get; set; }


    }
}
