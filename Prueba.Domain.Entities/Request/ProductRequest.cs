using System.ComponentModel.DataAnnotations;

namespace Prueba.Domain.Entities.Request
{
    public class ProductRequest
    {

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción del producto es obligatoria.")]
        [StringLength(100, ErrorMessage = "La descripción no puede tener más de 100 caracteres.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, 1000000, ErrorMessage = "El precio debe estar entre 0 y $ 1.000.000.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado válido.")]
        public int StatusId { get; set; }
    }
}
