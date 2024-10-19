using System.ComponentModel.DataAnnotations;

namespace Talabat.Dashboard.Models
{
    public class RoleFormViewModel
    {
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
    }
}
