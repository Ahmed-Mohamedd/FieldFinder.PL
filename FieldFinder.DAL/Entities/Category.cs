using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.DAL.Entities
{
    public  class Category
    {
        public Category()
        {
                Fields =new HashSet<Field>();
        }
        
        
        public int Id { get; set; }


        [Required(ErrorMessage ="Code is required")]
        public string Code { get; set; }


        [Required(ErrorMessage ="Name is required")]
        [MaxLength(60)]
        public string Name { get; set; }


        public DateTime DateOfCreation { get; set; }


        public IEnumerable<Field> Fields { get; set; }
    }
}
