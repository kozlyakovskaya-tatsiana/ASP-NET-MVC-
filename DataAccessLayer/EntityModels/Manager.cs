using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DataAccessLayer.EntityModels
{
    public class Manager
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(35, MinimumLength = 1, ErrorMessage = "Length should be from 1 to 35 symbols")]
        [Remote("CheckExisting", "Manager", ErrorMessage = "Manager with such name already exists. Choose another name")]
        public string Name { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

    }
}
