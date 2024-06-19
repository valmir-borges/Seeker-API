using Api.Data;
using Api.Models;
using Api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly Contexto _dbContext;

        public UsuarioRepositorio(Contexto dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UsuarioModel>> GetAll()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        public async Task<UsuarioModel> GetById(int id)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.UsuarioId == id);
        }

        public async Task<UsuarioModel> GetByEmail(string email)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.UsuarioEmail == email);
        }

        public async Task<UsuarioModel> InsertUsuario(UsuarioModel user)
        {
            //Verificando se já existe o email
            UsuarioModel existe = await _dbContext.Usuario.FirstOrDefaultAsync(x => x.UsuarioEmail == user.UsuarioEmail);

            if(existe == null)
            {
                await _dbContext.Usuario.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            else
            {              
               throw new Exception("Email já existe!");
            }
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel user, int id)
        {
            UsuarioModel users = await GetById(id);
            if (users == null)
            {
                throw new Exception("Não encontrado.");
            }
            else
            {
                users.UsuarioNome = user.UsuarioNome;
                users.UsuarioTelefone = user.UsuarioTelefone;
                users.UsuarioEmail = user.UsuarioEmail;
                users.UsuarioSenha = user.UsuarioSenha;
                _dbContext.Usuario.Update(users);
                await _dbContext.SaveChangesAsync();
            }
            return users;

        }
        public async Task<bool> DeleteUsuario(int id)
        {
            UsuarioModel users = await GetById(id);
            if (users == null)
            {
                throw new Exception("Não encontrado.");
            }

            _dbContext.Usuario.Remove(users);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
