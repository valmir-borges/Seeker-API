using Api.Models;

namespace Api.Repositorios.Interfaces
{
    public interface IObservacoesRepositorio
    {
        Task<List<ObservacoesModel>> GetAll();

        Task<ObservacoesModel> GetById(int id);

        Task<List<ObservacoesModel>> GetByIdPessoa(int id);


        Task<ObservacoesModel> InsertObservacao(ObservacoesModel observacao);

        Task<ObservacoesModel> UpdateObservacao(ObservacoesModel observacao, int id);

        Task<bool> DeleteObservacao(int id);
    }
}
