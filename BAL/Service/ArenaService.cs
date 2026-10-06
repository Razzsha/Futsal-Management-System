using DAO.Data;
using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.IService;
using Microsoft.EntityFrameworkCore;

namespace Futsal_Management.Service
{
    public class ArenaService : IArenaService
    {
        private readonly AppDbContext _context;

        public ArenaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseResult<ArenaDto>> CreateArena(ArenaDto arenaDto)
        {
            try
            {
                var arena = new Arena
                {
                    Name = arenaDto.Name,
                    Area = arenaDto.Area,
                    Hour = arenaDto.Hour
                };
                await _context.Arenas.AddAsync(arena);
                await _context.SaveChangesAsync();

                arenaDto.Id = arenaDto.Id;

                return ResponseResult<ArenaDto>.Success(arenaDto, "Arena Created Successfully");

            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(null, ex.Message);
            }
        }
        public async Task<ResponseResult<List<ArenaDto>>> GetAllArena()
        {
            try
            {
                var arenas = await _context.Arenas
                    .AsNoTracking()
                    .Select(x => new ArenaDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Area = x.Area,
                        Hour = x.Hour
                    }).ToListAsync();

                return ResponseResult<List<ArenaDto>>.Success(arenas, "Arena retrived successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<List<ArenaDto>>.Failure(null, ex.Message);
            }
        }

        public async Task<ResponseResult<ArenaDto>> GetArenaById(int id)
        {
            try
            {
                var arena = await _context.Arenas
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(x => new ArenaDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Area = x.Area,
                        Hour = x.Hour
                    }).FirstAsync();

                if (arena == null)
                {
                    return ResponseResult<ArenaDto>.Failure(null, "Arena not found");
                }
                return ResponseResult<ArenaDto>.Success(arena, "Arena retrived sucessfully");

            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(null, ex.Message);
            }
        }

        public async Task<ResponseResult<ArenaDto>> UpdateArena(int id, ArenaDto arenaDto)
        {
            try
            {
                var arena = await _context.Arenas
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (arena == null)
                {
                    return ResponseResult<ArenaDto>.Failure(null, "Arena not found");
                }
                arena.Name = arenaDto.Name;
                arena.Area = arenaDto.Area;
                arena.Hour = arenaDto.Hour;

                await _context.SaveChangesAsync();

                arenaDto.Id = arena.Id;

                return ResponseResult<ArenaDto>.Success(arenaDto, "Arena Updated successfully");

            }
            catch (Exception ex)
            {
                return ResponseResult<ArenaDto>.Failure(null, ex.Message);
            }
        }

        public async Task<ResponseResult<bool>> DeleteArena(int id)
        {
            try
            {
                var arena = await _context.Arenas
                .FirstOrDefaultAsync(x => x.Id == id);

                if (arena == null)
                {
                    return ResponseResult<bool>.Failure(
                        false,
                        "Arena not found");
                }

                _context.Arenas.Remove(arena);

                await _context.SaveChangesAsync();

                return ResponseResult<bool>.Success(
                    true,
                    "Arena deleted successfully");
            }
            catch (Exception ex)
            {
                return ResponseResult<bool>.Failure(false, ex.Message);
            }

        }
    }
}
