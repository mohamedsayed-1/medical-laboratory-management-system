namespace Medical_Laboratory_Management_System.Services
{
    public interface IServices<T>
    {
        public void Add(T entity);
        public List<T> GetAll();
        public IQueryable<T> GetAllWithIncludes();
        public T? GetById(int id);
        public T? GetByIdWithIncludes(int id);
        public void Update(T entity);
        public void Delete(T entity);
        public void Save();
    }
}
