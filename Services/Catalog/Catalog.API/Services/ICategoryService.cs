using Catalog.API.Dtos;
using MicroServiceArchitecture.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
