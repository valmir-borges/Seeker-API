using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repositorios
{
    public class ObservacoesRepositorio : IObservacoesRepositorio
    {
        private readonly Contexto _dbContext;

        public ObservacoesRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ObservacoesModel>> GetAll()
        {
            return await _dbContext.Observacoes.ToListAsync();
        }

        public async Task<ObservacoesModel> GetById(int id)
        {
            return await _dbContext.Observacoes.FirstOrDefaultAsync(x => x.ObservacoesId == id);
        }

        public async Task<List<ObservacoesModel>> GetByIdPessoa(int pessoaId)
        {
            return await _dbContext.Observacoes
                                  .Where(x => x.PessoaId == pessoaId)
                                  .ToListAsync();
        }

        public async Task<ObservacoesModel> InsertObservacao(ObservacoesModel observacao)
        {
            await _dbContext.Observacoes.AddAsync(observacao);
            await _dbContext.SaveChangesAsync();
            return observacao;
        }

        public async Task<ObservacoesModel> UpdateObservacao(ObservacoesModel observacao, int id)
        {
            ObservacoesModel observacoes = await GetById(id);
            if (observacoes == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                observacoes.ObservacoesDescricao = observacao.ObservacoesDescricao;
                observacoes.ObservacoesLocal = observacao.ObservacoesLocal;
                observacoes.ObservacoesData = observacao.ObservacoesData;
                observacoes.PessoaId = observacao.PessoaId;
                observacoes.UsuarioId = observacao.UsuarioId;

                _dbContext.Observacoes.Update(observacoes);
                await _dbContext.SaveChangesAsync();
            }
            return observacoes;
        }

        public async Task<bool> DeleteObservacao(int id)
        {
            ObservacoesModel observacoes = await GetById(id);
            if (observacoes == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Observacoes.Remove(observacoes);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
