using Project.Models;

namespace Project.Services
{
    public interface IUsuarioService
    {
        Task CriarConta<T>(T user);
        Task DeletarConta(Guid id);
        Task EditarConta<T>(Guid id, T usuarioNovo);
        Task<bool> VerificarExistencia<T>(T usuario);
    }
}
