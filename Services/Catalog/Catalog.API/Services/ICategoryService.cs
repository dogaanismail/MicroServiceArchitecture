using Catalog.Api.Dtos;
using MicroServiceArchitecture.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
