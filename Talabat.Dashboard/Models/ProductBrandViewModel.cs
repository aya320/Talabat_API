using System.ComponentModel.DataAnnotations;

namespace Talabat.Dashboard.Models
{
    public class ProductBrandViewModel
    {
        [Required(ErrorMessage ="Please Enter The Name Of Brand")]
        public string Name { get; set; }
    }
}
