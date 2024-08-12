using Dto.Request;
using Dto.Response;

namespace BlazorClient.Proxy
{
    public interface IUserProxy
    {
        Task<LoginDtoResponse> Login(LoginDtoRequest request);

        Task Register(RegistrarUsuarioDto request);
    }
}
