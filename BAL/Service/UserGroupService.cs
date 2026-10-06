using DAO.Data;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.IService;
using Microsoft.EntityFrameworkCore;

namespace Futsal_Management.Service
{
    public class UserGroupService : IUserGroupService
    {
        private readonly AppDbContext _context;

        public UserGroupService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<UserGroupDto>> CreateUserGroup(UserGroupDto dto)
        {
            if (dto == null)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "Please Fill All Details");
            }

            var existingGroup = await _context.UserGroups.AnyAsync(x => x.Name == dto.Name);

            if (existingGroup)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "User Group Already Exist");
            }
            var userGroup = new UserGroup
            {
                Name = dto.Name,
                Description = dto.Description
            };

            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();

            var result = new UserGroupDto
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Description = userGroup.Description
            };
            return ResponseResult<UserGroupDto>.Success(result, "User Group Created Successfully");
        }

        public async Task<ResponseResult<UserGroupDto>> GetUserGroupById(int id)
        {
            var userGroup = await _context.UserGroups
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new UserGroupDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).FirstOrDefaultAsync();

            if (userGroup == null)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "User Group Not Found");
            }
            return ResponseResult<UserGroupDto>.Success(userGroup, "User Group Retrived Successfully");

        }
        public async Task<ResponseResult<List<UserGroupDto>>> GetUserGroups()
        {
            var userGroup = await _context.UserGroups
                .AsNoTracking()
                .Select(x => new UserGroupDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToListAsync();

            return ResponseResult<List<UserGroupDto>>.Success(userGroup, "User groups retrieved successfully");
        }

        public async Task<ResponseResult<UserGroupDto>> UpdateUserGroup(int id, UserGroupDto dto)
        {
            if (dto == null)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "Please fill all details");
            }
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return ResponseResult<UserGroupDto>.Failure(null, "User group name is required");
            }
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(x => x.Id == id);

            if (userGroup == null)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "User Group not found");
            }

            var existingGroup = await _context.UserGroups
                .AnyAsync(x =>
                x.Name == dto.Name && x.Id != id);

            if (existingGroup)
            {
                return ResponseResult<UserGroupDto>.Failure(null, "User group name already exist");
            }
            userGroup.Name = dto.Name;
            userGroup.Description = dto.Description;
            userGroup.ModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            var result = new UserGroupDto
            {
                Id = userGroup.Id,
                Name = userGroup.Name,
                Description = userGroup.Description
            };
            return ResponseResult<UserGroupDto>.Success(result, "User group Updated Sucessfully");
        }

        public async Task<ResponseResult<string>> DeleteUserGroup(int id)
        {
            var userGroup = await _context.UserGroups.FirstOrDefaultAsync(x => x.Id == id);

            if(userGroup == null)
            {
                return ResponseResult<string>.Failure(null, "User group not found");
            }
            var hasUser = await _context.Users.AnyAsync(x => x.UserGroupId == id);

            if (hasUser)
            {
                return ResponseResult<string>.Failure(null, "Cannot delete user group bcz usergroup assign to it");
            }
            _context.UserGroups.Remove(userGroup);
             
            await _context.SaveChangesAsync();

            return ResponseResult<string>.Success(null, "User group deleted sucessfully");
        }
    }
}
