namespace Project.Services
{
    public interface IUsuarioService
    {
        Task CriarConta(string nome, string email, string senha, double saldo, string identidade);
        Task DeletarConta(Guid id);
        Task EditarConta(Guid id);
        Task<bool> VerificarExistencia(string email, string identidade);
    }
}
