namespace Prueba.Domain.Entities.Model
{
    public class Category
    {
        public int Cat_Id { get; set; }
        public string Cat_Description { get; set; } = string.Empty;

        public ICollection<Products>? Products { get; set; }

    }
}
