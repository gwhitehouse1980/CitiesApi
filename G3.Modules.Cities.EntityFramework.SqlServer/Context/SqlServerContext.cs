using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3.Core.Enums;
using G3.Core.Interfaces;
using G3.Core.Models;
using G3.Modules.Cities.Entities;
using G3.Modules.Cities.EntityFramework.SqlServer.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace G3.Modules.Cities.EntityFramework.SqlServer.Context
{
    /// <summary>
    /// In Memory context for quick testing only, all data will be lost when app closes
    /// </summary>
    public class SqlServerContext : DbContext, IContext
    {
        public SqlServerContext(IOptions<DatabaseOptions> databaseOptions) : base(new DbContextOptionsBuilder()
            .UseSqlServer(databaseOptions.Value.ConnectionString)
            .Options)
        {
            
        }
        
        
        
        public DbSet<City> Cities { get; set; }
        
        public async Task<TEntity> AddEntity<TEntity>(TEntity newEntity) where TEntity : class, IEntity
        {
            var entry = await base.AddAsync(newEntity);

            return entry.Entity;
        }

        public async Task<TEntity> GetEntity<TEntity>(Guid id) where TEntity : class, IEntity
        {
            return await base.FindAsync<TEntity>(id);
        }

        /// <summary>
        /// Search entities by the values provided in search filters, needs to be updated to use reflection to ad query params, or to use dynamic linq
        /// </summary>
        /// <param name="searchFilters">The list of search filters</param>
        /// <typeparam name="TEntity">The type of entity we are searching for</typeparam>
        /// <returns>A list of matching entities</returns>
        public async Task<List<TEntity>> GetEntities<TEntity>(List<SearchFilterModel> searchFilters) where TEntity : class, IEntity
        {
            // Currently only supports search by name
            var nameSearch = searchFilters?
                .FirstOrDefault(o => o.FieldName == "Name")?
                .FieldValue;
            
            var q = from item in Set<TEntity>()
                where item.EntityStatus == EntityStatusEnum.Active
                select item;

            if (nameSearch != null)
            {
                q = q.Where(o => o.Name.StartsWith(nameSearch));
            }
            
            return await Task.FromResult(q.ToList());
        }
    }
}