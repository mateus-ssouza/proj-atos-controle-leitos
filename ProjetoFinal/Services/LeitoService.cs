using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Enums;
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

        public async Task<List<Leito>> FindLeitosClinicosDisponiveis()
        {
            // Realiza a consulta para encontrar os leitos sem solicitação
            var list = await _contexto.Leito
                .Where(p => p.Solicitacao == null) // Verifica se o leito não possui nenhuma solicitação
                .Where(p => p.TipoLeito == TipoLeito.CLINICO) // Verifica se o leito é tipo clinico
                .ToListAsync();

            return list;
        }

        public async Task<List<Leito>> FindLeitosCirurgicosDisponiveis()
        {
            // Realiza a consulta para encontrar os leitos sem solicitação
            var list = await _contexto.Leito
                .Where(p => p.Solicitacao == null) // Verifica se o leito não possui nenhuma solicitação
                .Where(p => p.TipoLeito == TipoLeito.CIRURGICO) // Verifica se o leito é tipo cirurgico
                .ToListAsync();

            return list;
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _contexto.Leito.FindAsync(id);
                _contexto.Leito.Remove(obj);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
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

        public void MudarStatusLeito(Leito obj)
        {
            obj.Status = StatusLeito.OCUPADO;
        }

        public void UpdateData(Leito _old, Leito _new)
        {
            _old.Codigo = _new.Codigo;
            if (_old.Status == StatusLeito.LIVRE)
            {
                _old.TipoLeito = _new.TipoLeito;
            }
        }
    }
}
