using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Entities
{
	public class BrandCategory
	{
        public int CategoryId { get; set; }
        public Category Category { get; set; }

		public int BrandId { get; set; }
		public Brand Brand { get; set; }
	}
}
