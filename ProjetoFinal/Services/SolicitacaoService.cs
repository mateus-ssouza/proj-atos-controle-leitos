using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Models.Enums;
using ProjetoFinal.Models.ViewModels;
using ProjetoFinal.Services.Exceptions;

namespace ProjetoFinal.Services
{
    public class SolicitacaoService
    {
        private readonly Contexto _contexto;

        public SolicitacaoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Solicitacao>> FindAllAsync()
        {
            return await _contexto.Solicitacao
                .Include(obj => obj.Paciente)
                .ToListAsync();
        }

        public async Task InsertAsync(Solicitacao obj)
        {
            obj.DataSolicitacao = DateTime.Now;
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task<Solicitacao> FindByIdAsync(int id)
        {
            return await _contexto.Solicitacao
                .Include(obj => obj.Paciente)
                .Include(obj => obj.Leito)
                .FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task<Solicitacao> FindByIdLeitoAsync(int id)
        {
            return await _contexto.Solicitacao
                .Include(obj => obj.Paciente)
                .Include(obj => obj.Leito)
                .FirstOrDefaultAsync(obj => obj.IdLeito == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Solicitacao.FindAsync(id);
            _contexto.Solicitacao.Remove(obj);
            await _contexto.SaveChangesAsync();
        }

        public async Task UpdateAsync(Solicitacao obj)
        {
            bool existeAlgum = await _contexto.Solicitacao.AnyAsync(x => x.Id == obj.Id);

            // Verificar se não existe o Solicitacao no banco
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

        public void UpdateData(Solicitacao _old, Solicitacao _new)
        {
            _old.Motivo = _new.Motivo;
            _old.Prioridade = _new.Prioridade;
            _old.TipoLeito = _new.TipoLeito;
            _old.NomeMedico = _new.NomeMedico;
            _old.NomeEnfermeiro = _new.NomeEnfermeiro;
            _old.Observacoes = _new.Observacoes;
        }

        public void RegularSolicitacao(Solicitacao _old, SolicitacaoViewModel _new)
        {
            _new.Solicitacao.TipoLeito = _old.TipoLeito;
            _new.Solicitacao.Prioridade = _old.Prioridade;

            UpdateData(_old, _new.Solicitacao);
            _old.IdLeito = _new.Solicitacao.IdLeito;
            _old.DataRegulacao = DateTime.Now;
            _old.Status = SolicitacaoStatus.REGULADO;
        }

        public void TransferirSolicitacao(Solicitacao _old, SolicitacaoViewModel _new)
        {
            _new.Solicitacao.TipoLeito = _old.TipoLeito;
            _new.Solicitacao.Prioridade = _old.Prioridade;

            UpdateData(_old, _new.Solicitacao);
            _old.IdLeito = _new.Solicitacao.IdLeito;
            _old.Status = SolicitacaoStatus.REGULADO;
        }
    }
}
