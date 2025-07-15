namespace Prueba.Domain.Entities.Model
{
    public class Status
    {
        public int Sta_Id { get; set; }
        public string Sta_Description { get; set; } = string.Empty;

        public ICollection<Products>? Products { get; set; }
    }

}
