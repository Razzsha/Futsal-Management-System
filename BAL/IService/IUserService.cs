using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Service;

namespace Futsal_Management.IService
{
    public interface IUserService 
    {
        Task<ResponseResult<UserDto>> CreateUser(UserDto dto);
        Task<ResponseResult<UserDto>> GetUserById(int id);
        Task<ResponseResult<List<UserDto>>> GetUsers();
        Task<ResponseResult<UserDto>> UpdateUser(int id, UserDto dto);
        Task<ResponseResult<UserDto>> DeleteUser(int id);
    }
}
