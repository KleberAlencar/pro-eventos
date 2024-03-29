using ProEventos.Application.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ProEventos.Application.Contratos
{
    public interface IAccountService
    {
        Task<bool> UserExists(string userName);

        Task<UserUpdateDto> GetUserByUserNameAsync(string userName);

        Task<SignInResult> CheckUserPasswordAsync(UserUpdateDto userUpdateDto, string password);

        Task<UserDto> CreateAccountAsync(UserDto userDto);

        Task<UserUpdateDto> UpdateAccount(UserUpdateDto userUpdateDto);
    }
}