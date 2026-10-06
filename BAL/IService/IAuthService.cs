using Futsal_Management.Domain.ViewModel;
using Futsal_Management.Domain.GenericResponse;

namespace Futsal_Management.IService
{
    public interface IAuthService
    {
        Task<ResponseResult<LoginResponseDto>> Login(LoginDto loginDto);
    }
}
    