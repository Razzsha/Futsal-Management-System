using DAO.IDAO;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;

namespace Futsal_Management.Service
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUserGroupDao _userGroupDao;

        public UserGroupService(IUserGroupDao userGroupDao)
        {
            _userGroupDao = userGroupDao;
        }

        public async Task<ResponseResult<UserGroupDto>> CreateUserGroup(
            UserGroupDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "Please fill all details");
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group name is required");
                }

                var existingGroup =
                    await _userGroupDao.ExistsByNameAsync(dto.Name);

                if (existingGroup)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group already exists");
                }

                var userGroup = new UserGroup
                {
                    Name = dto.Name,
                    Description = dto.Description
                };

                await _userGroupDao.AddAsync(userGroup);
                await _userGroupDao.SaveChangesAsync();

                var result = new UserGroupDto
                {
                    Id = userGroup.Id,
                    Name = userGroup.Name,
                    Description = userGroup.Description
                };

                return ResponseResult<UserGroupDto>.Success(
                    result,
                    "User group created successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserGroupDto>.Failure(
                    null,
                    ex.Message);
            }
        }
        public async Task<ResponseResult<UserGroupDto>> GetUserGroupById(
            int id)
        {
            try
            {
                var userGroup =
                    await _userGroupDao.GetByIdAsync(id);

                if (userGroup == null)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group not found");
                }

                var result = new UserGroupDto
                {
                    Id = userGroup.Id,
                    Name = userGroup.Name,
                    Description = userGroup.Description
                };

                return ResponseResult<UserGroupDto>.Success(
                    result,
                    "User group retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserGroupDto>.Failure(
                    null,
                    ex.Message);
            }
        }
        public async Task<ResponseResult<List<UserGroupDto>>> GetUserGroups()
        {
            try
            {
                var userGroups =
                    await _userGroupDao.GetAllAsync();

                var result = userGroups
                    .Select(x => new UserGroupDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description
                    })
                    .ToList();

                return ResponseResult<List<UserGroupDto>>.Success(
                    result,
                    "User groups retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<UserGroupDto>>.Failure(
                    null,
                    ex.Message);
            }
        }
        public async Task<ResponseResult<UserGroupDto>> UpdateUserGroup(
            int id,
            UserGroupDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "Please fill all details");
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group name is required");
                }

                var userGroup =
                    await _userGroupDao.GetByIdAsync(id);

                if (userGroup == null)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group not found");
                }

                var existingGroup =
                    await _userGroupDao.ExistsByNameAsync(
                        dto.Name,
                        id);

                if (existingGroup)
                {
                    return ResponseResult<UserGroupDto>.Failure(
                        null,
                        "User group name already exists");
                }

                userGroup.Name = dto.Name;
                userGroup.Description = dto.Description;
                userGroup.ModifiedDate = DateTime.UtcNow;

                await _userGroupDao.SaveChangesAsync();

                var result = new UserGroupDto
                {
                    Id = userGroup.Id,
                    Name = userGroup.Name,
                    Description = userGroup.Description
                };

                return ResponseResult<UserGroupDto>.Success(
                    result,
                    "User group updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserGroupDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<string>> DeleteUserGroup(int id)
        {
            try
            {
                var userGroup =
                    await _userGroupDao.GetByIdAsync(id);

                if (userGroup == null)
                {
                    return ResponseResult<string>.Failure(
                        null,
                        "User group not found");
                }

                var hasUsers =
                    await _userGroupDao.HasUsersAsync(id);

                if (hasUsers)
                {
                    return ResponseResult<string>.Failure(
                        null,
                        "Cannot delete user group because users are assigned to it");
                }

                _userGroupDao.Delete(userGroup);

                await _userGroupDao.SaveChangesAsync();

                return ResponseResult<string>.Success(
                    null,
                    "User group deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<string>.Failure(
                    null,
                    ex.Message);
            }
        }
    }
}