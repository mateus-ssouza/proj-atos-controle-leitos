using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Services.Exceptions;

namespace ProjetoFinal.Services
{
    public class LeitoService
    {
        private readonly Contexto _contexto;

        public LeitoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Leito>> FindAllAsync()
        {
            return await _contexto.Leito
                .Include(obj => obj.Solicitacao) 
                .ToListAsync();
        }

        public async Task InsertAsync(Leito obj)
        {
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Leito> FindByIdAsync(int id)
        {
            return await _contexto.Leito
                .Include(obj => obj.Solicitacao)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Leito.FindAsync(id);
            _contexto.Leito.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Leito obj)
        {
            bool existeAlgum = await _contexto.Leito.AnyAsync(x => x.Id == obj.Id);

            // Verificar se não existe o Leito no banco
            if (!existeAlgum)
            {
                throw new NotFoundException("Id não encontrado!");
            }

            try
            {
                _contexto.Update(obj);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
