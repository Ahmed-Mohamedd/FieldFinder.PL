using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FieldFinder.PL.Models
{
    public class FieldViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string? Description { get; set; }
  
       
        

        [Required]
        [Range(1, 10000, ErrorMessage = "Maximum Price is 10000")]
        public double HourRate { get; set; }

  
        [DisplayName("Category")]
        [Required(ErrorMessage ="Specifing Field Category Is a Mandatory")]
        public int CategoryId { get; set; }


        [DisplayName("Location")]
        [Required(ErrorMessage = "Specifing Field Location Is a Mandatory")]
        public int LocationId { get; set; }


        public IFormFile Image { get; set; }

        [ValidateNever]
        public string ImageName { get; set; }

    }
}
