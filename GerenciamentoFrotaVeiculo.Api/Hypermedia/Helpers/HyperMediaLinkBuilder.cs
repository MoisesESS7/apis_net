using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace GerenciamentoFrotaVeiculo.Api.Hypermedia.Helpers
{
    public class HyperMediaLinkBuilder
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiExplorer;

        public HyperMediaLinkBuilder(IApiDescriptionGroupCollectionProvider apiExplorer)
        {
            _apiExplorer = apiExplorer;
        }

        public Dictionary<string, string> BuildLinks(ResultExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            var apiVersion = "v" + context.RouteData.Values["version"]?.ToString();

            var urlHelper = new UrlHelperFactory().GetUrlHelper(context);

            var result = new Dictionary<string, string>();

            var endpoints = _apiExplorer.ApiDescriptionGroups.Items
                .SelectMany(g => g.Items)
                .Where(d =>
                    d.ActionDescriptor.RouteValues["controller"]?.Equals(controllerName, StringComparison.OrdinalIgnoreCase) == true &&
                    d.GroupName == apiVersion // considera a versão da API se houver
                );

            foreach (var endpoint in endpoints)
            {
                var routeValues = new RouteValueDictionary(endpoint.ActionDescriptor.RouteValues);
                string id = "";
                // adiciona parâmetro id se ele estiver presente na rota
                if (endpoint.ParameterDescriptions.Any(p => p.Name == "id"))
                {
                    id = context?.RouteData?.Values["id"]?.ToString() ?? "1";
                    routeValues["id"] = id;
                }

                // inclui versionamento se necessário
                if (!string.IsNullOrEmpty(apiVersion))
                {
                    routeValues["version"] = apiVersion.Replace("v", "");
                }

                var link = urlHelper.Link("DefaultApi", new { id });

                if (!string.IsNullOrEmpty(link))
                {
                    var httpMethod = endpoint.HttpMethod ?? "GET";
                    var relativePath = endpoint.RelativePath ?? string.Empty;

                    // Garante que tanto o path quanto o que será removido tenham o mesmo formato (sem maiúsculas, nem barras extras)
                    var prefixToRemove = $"api/v1/{controllerName}/".ToLowerInvariant();
                    var cleanedPath = relativePath.ToLowerInvariant().StartsWith(prefixToRemove)
                        ? relativePath.Substring(prefixToRemove.Length)
                        : "";

                    var key = $"{httpMethod.ToUpper()} {cleanedPath}";
                    result[key] = $"{link}/{cleanedPath}";
                }
            }

            return result;
        }
    }
}
