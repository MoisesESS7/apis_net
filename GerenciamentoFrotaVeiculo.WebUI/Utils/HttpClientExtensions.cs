using System.Text.Json;

namespace GerenciamentoFrotaVeiculo.WebUI.Utils
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao tentar se comunicar com a API. {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            var json = JsonSerializer.Deserialize<T>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return json!;
        }
    }
}
