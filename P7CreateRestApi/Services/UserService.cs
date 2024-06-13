using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Models.InputModel;
using P7CreateRestApi.Models.OutputModel;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserOutputModel?> Create(UserInputModel inputModel)
        {
            var user = new User
            {
                UserName = inputModel.UserName,
                FullName = inputModel.FullName,
                Role = inputModel.Role,
            };
            var result = await _userManager.CreateAsync(user, inputModel.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, user.Role);
                return ToOutputModel(user);
            }
            return null;
        }

        public async Task<UserOutputModel?> Delete(int id)
        {
            var user = _userRepository.FindById(id);
            if (user is not null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return ToOutputModel(user);
                }
            }
            return null;
        }

        public UserOutputModel? Get(int id)
        {
            var user = _userRepository.FindById(id);
            if (user is not null)
            {
                return ToOutputModel(user);
            }
            return null;
        }

        public async Task<List<UserOutputModel>> List()
        {
            var list = new List<UserOutputModel>();
            var users = await _userRepository.FindAll();
            foreach (var user in users)
            {
                list.Add(ToOutputModel(user));
            }
            return list;
        }

        public async Task<UserOutputModel?> Update(int id, UserInputModel inputModel)
        {
            var user = _userRepository.FindById(id);
            if (user is null)
            {
                return null;
            }
            if (!await _userManager.CheckPasswordAsync(user, inputModel.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _userManager.ResetPasswordAsync(user, token, inputModel.Password);
            }
            else
            {
                user.UserName = inputModel.UserName;
                user.FullName = inputModel.FullName;
                user.Role = inputModel.Role;
                await _userManager.UpdateAsync(user);
            }
            return ToOutputModel(user);
        }

        private UserOutputModel ToOutputModel(User user) => new UserOutputModel
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.PasswordHash,
            FullName = user.FullName,
            Role = user.Role
        };
    }
}
