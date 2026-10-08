using DAO.IDAO;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;
using Microsoft.AspNetCore.Identity;

namespace Futsal_Management.Service
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly PasswordHasher<string> _passwordHasher;

        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<ResponseResult<UserDto>> CreateUser(UserDto dto)
        {
            try
            {
                if (dto.UserGroupId < 0)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User group is Required");
                }

                var existingUser = await _userDao.ExistsByEmailAsync(dto.Email!);

                if (existingUser)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "Email already exist");
                }

                var userGroupExists =
                    await _userDao.UserGroupExistsAsync(dto.UserGroupId);

                if (!userGroupExists)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User group not found");
                }

                var hashedPassword = _passwordHasher.HashPassword(
                    dto.Email!,
                    dto.Password!);

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = hashedPassword,
                    UserGroupId = dto.UserGroupId
                };

                await _userDao.AddAsync(user);
                await _userDao.SaveChangesAsync();

                var result = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId
                };

                return ResponseResult<UserDto>.Success(
                    result,
                    "User created successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userDao.GetByIdAsync(id);

                if (user == null)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User not Found");
                }

                var result = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId
                };

                return ResponseResult<UserDto>.Success(
                    result,
                    "User retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userDao.GetAllAsync();

                var result = users.Select(x => new UserDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    UserGroupId = x.UserGroupId
                }).ToList();

                return ResponseResult<List<UserDto>>.Success(
                    result,
                    "Users retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<UserDto>>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> UpdateUser(
            int id,
            UserDto dto)
        {
            try
            {
                if (dto.UserGroupId <= 0)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User group is required");
                }

                var user = await _userDao.GetByIdAsync(id);

                if (user == null)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User not found");
                }

                var existingUser =
                    await _userDao.ExistsByEmailAsync(dto.Email!, id);

                if (existingUser)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "Email already exist");
                }

                var userGroupExists =
                    await _userDao.UserGroupExistsAsync(dto.UserGroupId);

                if (!userGroupExists)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User group not found");
                }

                user.Name = dto.Name;
                user.Email = dto.Email;
                user.UserGroupId = dto.UserGroupId;
                user.ModifiedDate = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    user.Password = _passwordHasher.HashPassword(
                        dto.Email!,
                        dto.Password);
                }

                await _userDao.SaveChangesAsync();

                var result = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId
                };

                return ResponseResult<UserDto>.Success(
                    result,
                    "User Updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var user = await _userDao.GetByIdAsync(id);

                if (user == null)
                {
                    return ResponseResult<UserDto>.Failure(
                        null,
                        "User Not Found");
                }

                _userDao.Delete(user);
                await _userDao.SaveChangesAsync();

                return ResponseResult<UserDto>.Success(
                    null,
                    "User Deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(
                    null,
                    ex.Message);
            }
        }
    }
}