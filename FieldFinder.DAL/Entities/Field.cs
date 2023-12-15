using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.DAL.Entities
{
    public  class Field
    {
        
        public int Id { get; set; }
        public string Name { get; set; }

        public string  Description { get; set; }

        public double HourRate { get; set; }

        public string ImageName { get; set; }

        public int CategoryId { get; set; }
    
        public Category Category { get; set; }

        public int LocationId {  get; set; }
        public Location Location { get; set; }
    }
}
