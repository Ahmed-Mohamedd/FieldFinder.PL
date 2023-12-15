using FieldFinder.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace FieldFinder.PL.Models
{
    public class HomeIndexViewModel
    {
        [Display(Name ="Location")]
        public int LocationId { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
