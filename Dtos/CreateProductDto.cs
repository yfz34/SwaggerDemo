using System.ComponentModel.DataAnnotations;

namespace SwaggerDemo.Dtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage ="Name不能為空!")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(0, 50000)]
        public decimal Price { get; set; }
    }
}