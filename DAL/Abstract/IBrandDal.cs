﻿using CORE.Entities;
using CORE.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IBrandDal:IRepository<Brand>
    {
        Task<Brand> GetProductsByBrandAsync(int id);
    }
}
