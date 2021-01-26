using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using G3.Core.Enums;
using G3.Core.Interfaces;
using G3.Core.Models;

namespace G3.Core.Tests.Mocks
{
    public class MockContext : IContext
    {
        private readonly List<IEntity> _itemList;

        public MockContext()
        {
            _itemList = new List<IEntity>();
        }
        
        public async Task<TEntity> AddEntity<TEntity>(TEntity newEntity) 
            where TEntity : class, IEntity
        {
            _itemList.Add(newEntity);
            
            return await Task.FromResult(newEntity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.FromResult(0);
        }

        public async Task<TEntity> GetEntity<TEntity>(Guid id) 
            where TEntity : class, IEntity
        {
            var q = from item in _itemList
                where item is TEntity
                where item.Id == id
                where item.EntityStatus == EntityStatusEnum.Active
                select item as TEntity;

            return await Task.FromResult(q.FirstOrDefault());
        }

        public async Task<List<TEntity>> GetEntities<TEntity>(List<SearchFilterModel> searchFilters) 
            where TEntity : class, IEntity
        {
            // Mock only supports search by name
            var nameSearch = searchFilters?
                .FirstOrDefault(o => o.FieldName == "Name")?
                .FieldValue;
            
            var q = from item in _itemList
                where item is TEntity
                where item.EntityStatus == EntityStatusEnum.Active
                select item as TEntity;

            if (nameSearch != null)
            {
                q = q.Where(o => o.Name.StartsWith(nameSearch));
            }
            
            return await Task.FromResult(q.ToList());
        }
    }
}