﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Interfaces
{
    //List<User>
    //IRepository<User>
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(object id);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Delete(object id);
        void Add(TEntity entity);
        int SaveChanges();
    }
}
