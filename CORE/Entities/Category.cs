﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entities
{
	public class Category : BaseEntity
    {
        public string Name { get; set; }
        public List<BrandCategory> BrandCategories { get; set; }
    }
}
