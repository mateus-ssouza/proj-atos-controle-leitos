﻿using Microsoft.EntityFrameworkCore;
using ProjetoFinal.Data;
using ProjetoFinal.Models;
using ProjetoFinal.Services.Exceptions;

namespace ProjetoFinal.Services
{
    public class EnderecoService
    {
        private readonly Contexto _contexto;

        public EnderecoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        // Inserir um endereço
        public async Task InsertAsync(Endereco obj)
        {
            _contexto.Add(obj);
            await _contexto.SaveChangesAsync();
        }


        // Buscar por ID um endereço
        public async Task<Endereco> FindByIdAsync(int id)
        {
            return await _contexto.Endereco.FirstOrDefaultAsync(obj => obj.Id == id);
        }


        // Remover um endereço
        public async Task RemoveAsync(int id)
        {
            var obj = await _contexto.Endereco.FindAsync(id);
            _contexto.Endereco.Remove(obj);
            await _contexto.SaveChangesAsync();
        }
    }
}
