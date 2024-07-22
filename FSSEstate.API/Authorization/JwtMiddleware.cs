using AutoMapper;
using FSSEstate.Business.Interfaces.Authorization;
using FSSEstate.Core.Models.UserModels;
using FSSEstate.Repository.Interfaces;

namespace FSSEstate.API.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IMapper mapper, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var email = jwtUtils.ValidateJwtToken(token);
            if (email != null)
            {
                // attach user to context on successful jwt validation
                var user = await unitOfWork.UserRepository.GetAsync(item => item.Email.ToLower() == email.ToLower());
                // need to change logic here for attaching user to context.
                context.Items.Add("user", user is not null ? mapper.Map<User>(user) : new User());
            }

            await _next(context);
        }
    }
}
