using AutoMapper;
using MongoDB.Driver;
using Services.CatalogAPI.Dtos;
using Services.CatalogAPI.Models;
using Services.CatalogAPI.Settings;
using Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.CatalogAPI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IDatabaseSetings databaseSetings, IMapper mapper)
        {
            var client = new MongoClient(databaseSetings.ConnectionString);
            var database = client.GetDatabase(databaseSetings.DatabaseName);

            _categoryCollection = database.GetCollection<Category>(databaseSetings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync() =>
            Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(await _categoryCollection.Find(category => true).ToListAsync()), 200);

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            await _categoryCollection.InsertOneAsync(_mapper.Map<Category>(categoryDto));
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(categoryDto), 200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();

            if (category == null)
                return Response<CategoryDto>.Fail("Category Not Found", 404);

            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
