using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.GenericResponse;

namespace Futsal_Management.IService
{
    public interface IUserGroupService
    {
        Task<ResponseResult<UserGroupDto>> CreateUserGroup(UserGroupDto dto);
        Task<ResponseResult<UserGroupDto>> GetUserGroupById(int id);
        Task<ResponseResult<List<UserGroupDto>>> GetUserGroups();
        Task<ResponseResult<UserGroupDto>> UpdateUserGroup(   int id, UserGroupDto dto);
        Task<ResponseResult<string>> DeleteUserGroup(int id);
    }
}