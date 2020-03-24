using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Length must be from 1 to 35 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Range(0,1000000, ErrorMessage = "Cost must be from 0 to 1000000")]
        public double Cost { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
