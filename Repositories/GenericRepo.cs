using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly DatabaseContext context;

        public GenericRepo(DatabaseContext context)
        {
            this.context = context;
        }
        public T GetById(int id)
        {
            T item = context.Set<T>().Find(id);
            if (item != null)
            {
                context.Entry(item).State = EntityState.Detached;
                return item;
            }
            return null;
        }



        public List<T> GetAll()
        {
            return context.Set<T>().AsNoTracking().ToList();
        }
        public void Insert(T item)
        {
            context.Add(item);
        }
        public void Update(T item)
        {
            context.Update<T>(item);
        }
        public bool Delete(int id)
        {
            T item = GetById(id);
            if (item != null)
            {
                context.Remove(item);
                return true;
            }
            return false;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
