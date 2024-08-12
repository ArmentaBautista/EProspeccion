using Dto.Request;
using Dto.Response;
using System.Net.Http.Json;

namespace BlazorClient.Proxy
{
    public class UserProxy : IUserProxy
    {
        private readonly HttpClient _httpClient;

        public UserProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuarios/Login", request);
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginDtoResponse>();

            return loginResponse!;
        }

        public async Task Register(RegistrarUsuarioDto request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Usuarios/Register", request);

            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();

            if (resultado is { Exito: false })
            {
                throw new InvalidOperationException(resultado.MensajeError);
            }

        }
    }
}
