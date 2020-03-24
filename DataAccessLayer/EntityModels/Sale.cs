using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels
{
    public class Sale
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The field is compulsory")]
        [Range(0, 1000000, ErrorMessage = "Sum must be from 0 to 1000000")]
        public double Sum { get; set; }

        [Required]
       // [RegularExpression(@"(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/(19|20)\d\d", ErrorMessage = @"Data must be in dd/MM/yyyy format")]
        public DateTime Date { get; set; }

        public int? ManagerId { get; set; }

        public int? CustomerId { get; set; }

        public int? ProductId { get; set; }


        public virtual Manager Manager { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Product Product { get; set; }

       
    }
}
