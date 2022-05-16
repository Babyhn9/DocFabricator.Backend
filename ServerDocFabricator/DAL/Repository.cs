using ColoredLive.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace ServerDocFabricator.DAL
{
    public class Repository<T> : IRepository<T>
        where T : class, new()
    {
        private static Dictionary<string, PropertyInfo> _storesInfo;

        private AppDbContext _context;
        private DbSet<T> _store;

        public Repository(AppDbContext context)
        {
            _context = context;
            _store = GetCurrentStore(context);
        }

        static Repository()
        {
            _storesInfo = new Dictionary<string, PropertyInfo>();

            var type = typeof(AppDbContext);

            var storeTypes = type.GetProperties();

            foreach (var storeProperty in storeTypes)
            {
                var storePropertyType = storeProperty.PropertyType;
                var closedStoreType = storePropertyType.GetGenericArguments().FirstOrDefault();
                if (closedStoreType == null) continue;
                _storesInfo.Add(closedStoreType.Name, storeProperty);
            }

        }

        public T Add(T entity)
        {
            var newEntity = _store.Add(entity);
            _context.SaveChanges();
            return newEntity.Entity;
        }

        public T Find(Guid id)
        {
            var result = _store.FirstOrDefault(el => (el as Entity).Id == id) ?? new T();
            return result;
        }

        public T Find(Expression<Func<T, bool>> predic) => _store.FirstOrDefault(predic) ?? new T();


        public List<T> FindAll(Expression<Func<T, bool>> predic) => _store.Where(predic).ToList();


        public bool Remove(T entity)
        {
            var removed = _store.Remove(entity)?.Entity;

            if (removed == null) return false;

            _context.SaveChanges();

            return true;
        }

        public bool Remove(Guid entity)
        {
            var finded = Find(entity);
            if (finded == null && _store.Remove(finded)?.Entity == null) return false;
            _context.SaveChanges();
            return true;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int Count() => _store.Count();

        private static DbSet<T> GetCurrentStore(AppDbContext context)
        {
            return (DbSet<T>)_storesInfo[typeof(T).Name].GetValue(context);
        }
    }
}
