using GerenciamentoFrotaVeiculo.WebUI.Models;

namespace GerenciamentoFrotaVeiculo.WebUI.Services.IServices
{
    public interface IColaboradorService
    {
        Task<IEnumerable<ColaboradorViewModel>> GetAllAsync();
        Task<ColaboradorViewModel> GetByIdAsync(int id);
        Task<ColaboradorViewModel> CreateAsync(ColaboradorViewModel vo);
        Task<ColaboradorViewModel> UpdatAsync(ColaboradorViewModel vo);
        Task<bool> DeleteAsync(int id);
    }
}
