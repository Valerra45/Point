using MongoDB.Bson;
using MongoDB.Driver;
using Point.External.Core.Domain;
using Point.External.Infrastructure.Environment;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Point.External.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IExternalDatabaseSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        private string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(new BsonDocument())
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstWhere(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            return await _collection.Find(x => ids.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate)
               .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }
    }
}
