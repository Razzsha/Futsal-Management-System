using DAO.Data;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.IService;
using Futsal_Management.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Futsal_Management.Service 
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<string> _passwordHasher;

        public UserService(AppDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<string>();
        }

        public async Task<ResponseResult<UserDto>> CreateUser(UserDto dto)
        {
            try
            {
                if (dto.UserGroupId < 0)
                {
                    return ResponseResult<UserDto>.Failure(null, "User group is Required");
                }

                var existingUser = await _context.Users.AnyAsync(x => x.Email == dto.Email);

                if (existingUser)
                {
                    return ResponseResult<UserDto>.Failure(null, "Email already exist");
                }
                var userGroupExists = await _context.UserGroups.AnyAsync(x => x.Id == dto.UserGroupId);

                if (!userGroupExists)
                {
                    return ResponseResult<UserDto>.Failure(null, "User group not found");
                }
                var hashedPassword = _passwordHasher.HashPassword(dto.Email!, dto.Password!);

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Password = hashedPassword,
                    UserGroupId = dto.UserGroupId
                };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var result = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId
                };
                return ResponseResult<UserDto>.Success(result, "User created successfully");
            }

            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(null, ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        UserGroupId = x.UserGroupId
                    }).FirstOrDefaultAsync();

                if (user == null)
                {
                    return ResponseResult<UserDto>.Failure(null, "User not Found");
                }
                return ResponseResult<UserDto>.Success(user, "User retrieved sucessfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(null, ex.Message);
            }
        }
         public async Task<ResponseResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _context.Users
                    .AsNoTracking()
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        UserGroupId = x.UserGroupId
                    })
                    .ToListAsync();

                return ResponseResult<List<UserDto>>.Success(
                    users,
                    "Users retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<UserDto>>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> UpdateUser(int id, UserDto dto)
        {
            try
            {
                if(dto.UserGroupId <= 0)
                {
                    return ResponseResult<UserDto>.Failure(null, "User group is required");
                }
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == id);
                
                if(user == null)
                {
                    return ResponseResult<UserDto>.Failure(null, "User not found");
                }
                var existingUser = await _context.Users
                    .AnyAsync(x => x.Email == dto.Email && x.Id != id);

                if (existingUser)
                {
                    return ResponseResult<UserDto>.Failure(null, "Email already exist");
                }
                var userGroupExists = await _context.UserGroups
   .AnyAsync(x => x.Id == dto.UserGroupId);

                if (!userGroupExists)
                {
                    return ResponseResult<UserDto>.Failure(null, "User group not found");
                }

                user.Name = dto.Name;
                user.Email = dto.Email;
                user.UserGroupId = dto.UserGroupId;
                user.ModifiedDate = DateTime.UtcNow;

                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    user.Password = _passwordHasher.HashPassword(
                        dto.Email, dto.Password);
                }
                await _context.SaveChangesAsync();

                var result = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    UserGroupId = user.UserGroupId
                };
                return ResponseResult<UserDto>.Success(result, "User Upddated sucessfully");

            }catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(null, ex.Message);
            }
        }

        public async Task<ResponseResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync (x => x.Id == id);

                if(user == null)
                {
                    return ResponseResult<UserDto>.Failure(null, "User Not Found");
                }
                _context.Users .Remove(user);
                await _context.SaveChangesAsync();
                return ResponseResult<UserDto>.Success(null, "user Deleted sucessFully");
            }catch (Exception ex)
            {
                return ResponseResult<UserDto>.Failure(null , ex.Message);
            }
        }

    }

}


