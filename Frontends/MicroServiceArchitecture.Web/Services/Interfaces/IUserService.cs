using MicroServiceArchitecture.Web.Models;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserAsync();
    }
}
