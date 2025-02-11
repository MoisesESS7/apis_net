using GerenciamentoFrotaVeiculo.WebUI.Models;
using GerenciamentoFrotaVeiculo.WebUI.Services.IServices;
using GerenciamentoFrotaVeiculo.WebUI.Utils;

namespace GerenciamentoFrotaVeiculo.WebUI.Services
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlBase = $"api/v{1}/colaboradores";

        public ColaboradorService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<ColaboradorViewModel>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_urlBase);
            var json = await response.ReadContentAs<IEnumerable<ColaboradorViewModel>>();

            return json!;
        }

        public async Task<ColaboradorViewModel> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_urlBase}/busca-completa/{id}");
            var json = await response.ReadContentAs<ColaboradorViewModel>();

            return json!;
        }

        public async Task<ColaboradorViewModel> CreateAsync(ColaboradorViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(_urlBase, model);
            var json = await response.ReadContentAs<ColaboradorViewModel>();

            return json!;
        }

        public async Task<ColaboradorViewModel> UpdatAsync(ColaboradorViewModel model)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_urlBase}/{model.Id}", model);
            var json = await response.ReadContentAs<ColaboradorViewModel>();

            return json!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_urlBase}/{id}");
            var json = await response.ReadContentAs<bool>();

            return json!;
        }
    }
}
