using Futsal_Management.Domain.GenericResponse;
using Futsal_Management.Domain.ViewModel;
namespace Futsal_Management.IService
{
    public interface IArenaService
    {
        Task<ResponseResult<ArenaDto>> CreateArena(ArenaDto arenaDto);
        Task<ResponseResult<List<ArenaDto>>> GetAllArena();
        Task<ResponseResult<ArenaDto>> GetArenaById(int id);
        Task<ResponseResult<ArenaDto>> UpdateArena(int id, ArenaDto arenaDto);
        Task<ResponseResult<bool>> DeleteArena(int id);
    }
}
