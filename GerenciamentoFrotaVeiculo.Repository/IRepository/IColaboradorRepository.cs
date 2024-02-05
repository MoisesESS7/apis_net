﻿using GerenciamentoFrotaVeiculo.Models;

namespace GerenciamentoFrotaVeiculo.Repository.IRepository
{
    public interface IColaboradorRepository
    {
        Task<Colaborador> GetAsync(int id);
        Task<List<Colaborador>> GetAllAsync();
        Task CreateAsync(Colaborador colaborador);
        Task UpdateAsync(Colaborador colaboradorRequisicao, Colaborador colaboradorDb);
        Task DeleteAsync(Colaborador colaborador);
    }
}
