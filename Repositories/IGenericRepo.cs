namespace TodoApp.Repositories
{
    public interface IGenericRepo<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Insert(T item);
        public void Update(T item);
        public bool Delete(int id);
        public void Save();
    }
}
