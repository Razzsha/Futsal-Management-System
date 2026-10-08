using DAO.IDAO;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.IService;

namespace Futsal_Management.Service
{
    public class ArenaService : IArenaService
    {
        private readonly IArenaDao _arenaDao;

        public ArenaService(IArenaDao arenaDao)
        {
            _arenaDao = arenaDao;
        }

        public async Task<ResponseResult<ArenaDto>> CreateArena(
            ArenaDto arenaDto)
        {
            try
            {
                if (arenaDto == null)
                {
                    return ResponseResult<ArenaDto>.Failure(
                        null,
                        "Please fill all details");
                }

                var arena = new Arena
                {
                    Name = arenaDto.Name,
                    Area = arenaDto.Area,
                    Hour = arenaDto.Hour
                };

                await _arenaDao.AddAsync(arena);
                await _arenaDao.SaveChangesAsync();

                arenaDto.Id = arena.Id;

                return ResponseResult<ArenaDto>.Success(
                    arenaDto,
                    "Arena created successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<List<ArenaDto>>> GetAllArena()
        {
            try
            {
                var arenas = await _arenaDao.GetAllAsync();

                var result = arenas
                    .Select(x => new ArenaDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Area = x.Area,
                        Hour = x.Hour
                    })
                    .ToList();

                return ResponseResult<List<ArenaDto>>.Success(
                    result,
                    "Arena retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<ArenaDto>>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<ArenaDto>> GetArenaById(int id)
        {
            try
            {
                var arena = await _arenaDao.GetByIdAsync(id);

                if (arena == null)
                {
                    return ResponseResult<ArenaDto>.Failure(
                        null,
                        "Arena not found");
                }

                var result = new ArenaDto
                {
                    Id = arena.Id,
                    Name = arena.Name,
                    Area = arena.Area,
                    Hour = arena.Hour
                };

                return ResponseResult<ArenaDto>.Success(
                    result,
                    "Arena retrieved successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<ArenaDto>> UpdateArena(
            int id,
            ArenaDto arenaDto)
        {
            try
            {
                if (arenaDto == null)
                {
                    return ResponseResult<ArenaDto>.Failure(
                        null,
                        "Please fill all details");
                }

                var arena = await _arenaDao.GetByIdAsync(id);

                if (arena == null)
                {
                    return ResponseResult<ArenaDto>.Failure(
                        null,
                        "Arena not found");
                }

                arena.Name = arenaDto.Name;
                arena.Area = arenaDto.Area;
                arena.Hour = arenaDto.Hour;
                arena.ModifiedDate = DateTime.UtcNow;

                await _arenaDao.SaveChangesAsync();

                var result = new ArenaDto
                {
                    Id = arena.Id,
                    Name = arena.Name,
                    Area = arena.Area,
                    Hour = arena.Hour
                };

                return ResponseResult<ArenaDto>.Success(
                    result,
                    "Arena updated successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(
                    null,
                    ex.Message);
            }
        }

        public async Task<ResponseResult<bool>> DeleteArena(int id)
        {
            try
            {
                var arena = await _arenaDao.GetByIdAsync(id);

                if (arena == null)
                {
                    return ResponseResult<bool>.Failure(
                        false,
                        "Arena not found");
                }

                _arenaDao.Delete(arena);

                await _arenaDao.SaveChangesAsync();

                return ResponseResult<bool>.Success(
                    true,
                    "Arena deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<bool>.Failure(
                    false,
                    ex.Message);
            }
        }
    }
}