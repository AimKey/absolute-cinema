using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects.DAOs;

public class GenericDao<TEntity> where TEntity : class
{
    internal AbsoluteCinemaContext context;
    internal DbSet<TEntity> dbSet;

    public GenericDao(AbsoluteCinemaContext context)
    {
        this.context = context;
        this.dbSet = context.Set<TEntity>();
    }
 
}