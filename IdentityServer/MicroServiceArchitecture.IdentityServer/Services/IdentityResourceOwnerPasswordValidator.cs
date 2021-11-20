using IdentityModel;
using IdentityServer4.Validation;
using MicroServiceArchitecture.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceArchitecture.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        #region Fields
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Ctor

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #endregion

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            if (existUser == null)
            {
                var errors = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "Email veya sifreniz yanlis!" } }
                };

                context.Result.CustomResponse = errors;

                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);

            if (!passwordCheck)
            {
                var errors = new Dictionary<string, object>
                {
                    { "errors", new List<string> { "Email veya sifreniz yanlis!" } }
                };

                context.Result.CustomResponse = errors;

                return;
            }

            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
