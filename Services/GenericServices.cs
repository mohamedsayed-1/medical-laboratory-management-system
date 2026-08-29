using Medical_Laboratory_Management_System.Data;
using Medical_Laboratory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Medical_Laboratory_Management_System.Services
{
    public class GenericServices<T> : IServices<T> where T : class
    {
        private readonly MLMSDbContext context;
        public GenericServices(MLMSDbContext context)
        {
            this.context = context;
        }
        public void Add(T entity)
        {
            context.Add(entity);
        }
        public void Delete(T entity)
        {
            context.Remove(entity);
        }
        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }
        public IQueryable<T> GetAllWithIncludes()
        {
            IQueryable<T> queue = context.Set<T>();
            var navigationProperties = context.Model
               .FindEntityType(typeof(T))?
               .GetNavigations()
               .Select(nav => nav.Name);
            if (navigationProperties is not null)
            {
                foreach (string? navigationProperty in navigationProperties)
                {
                    queue = queue.Include(navigationProperty);
                }
            }
            return queue;
        }
        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }
        public T? GetByIdWithIncludes(int id)
        {
            IQueryable<T> queue = context.Set<T>()
                .Where((x => EF.Property<int>(x, "Id") == id));
            if (queue is null)
                return null;
            var navigationProperties = context.Model
               .FindEntityType(typeof(T))?
               .GetNavigations()
               .Select(nav => nav.Name);
            if (navigationProperties is not null)
            {
                foreach (string? navigationProperty in navigationProperties)
                {
                    queue = queue.Include(navigationProperty);
                }
            }
            return queue.First();
        }
        public void Update(T entity)
        {
            context.Update(entity);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}