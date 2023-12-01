﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldFinder.DAL.Entities
{
    public  class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<Field> Fields { get; set; } = new HashSet<Field>();
    }
}