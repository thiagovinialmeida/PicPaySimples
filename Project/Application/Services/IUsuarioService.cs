namespace Project.Services
{
    public interface IUsuarioService
    {
        void CriarConta(string nome, string email, string senha, double saldo, string identidade);
        void DeletarConta(Guid id);
        void EditarConta(Guid id);
    }
}
