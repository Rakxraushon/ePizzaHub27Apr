﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(object id);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Delete(object id);
        void Add(TEntity entity);
    }
}
