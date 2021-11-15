using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Models;
using Catalog.API.Settings;
using MicroServiceArchitecture.Shared.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    internal class CategoryService : ICategoryService
    {
        #region Fields
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public CategoryService(IMapper mapper,
            IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        #endregion

        #region Methods

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();

            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }

        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return Response<CategoryDto>.Fail("Category not found!", 404);
            }

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }

        #endregion
    }
}
