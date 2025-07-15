﻿using Microsoft.EntityFrameworkCore;
using Prueba.Domain.Interfaces;
using Prueba.Infraestructure.Contexts.Context;

namespace Prueba.Infraestructure.Contexts
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IQueryable<T> Get()
        {
            return _dbSet.AsQueryable();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(int id, T entity)
        {
            var existingEntity = _dbSet.Find(id);
            if (existingEntity != null)
            {
                var entry = _dbContext.Entry(existingEntity);
                var keyName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                                 .Select(x => x.Name).First();
                var entityValues = _dbContext.Entry(entity).CurrentValues;
                foreach (var property in entityValues.Properties)
                {
                    if (property.Name != keyName)
                    {
                        entry.Property(property.Name).CurrentValue = entityValues[property];
                    }
                }
                _dbContext.SaveChanges();
                return existingEntity;
            }
            return null;
        }



        public bool Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return _dbContext.SaveChanges() > 0;
            }
            return false;
        }
    }
}
