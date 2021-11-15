using Catalog.API.Dtos;
using Catalog.API.Models;
using MicroServiceArchitecture.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    internal interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(Category category);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
