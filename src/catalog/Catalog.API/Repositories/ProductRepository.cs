using Catalog.API.Data.Interfaces;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                           .Products
                           .Find(p => true)
                           .ToListAsync();
        }

        public async Task<Product> GetProduct(string Id)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);

            return await _context
                           .Products
                           .Find(filter)
                           .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task Create(Product product)
        {
            await _context
                    .Products
                    .InsertOneAsync(product);
        }

        public async Task<bool> Delete(string Id)
        {

            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, Id);

            DeleteResult delete = await _context
                                            .Products
                                            .DeleteOneAsync(filter);

            return delete.IsAcknowledged && delete.DeletedCount > 0;
        }

        public async Task<bool> Update(Product product)
        {
            var update = await _context
                                   .Products
                                   .ReplaceOneAsync(p => p.Id == product.Id, replacement: product);

            return update.IsAcknowledged && update.ModifiedCount > 0;
        }
    }
}
