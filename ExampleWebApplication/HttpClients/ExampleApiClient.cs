using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ExampleForStudents.Contracts;
using ExampleWebApplication.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ExampleWebApplication.HttpClients
{
    public class ExampleApiClient : IExampleApiClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public ExampleApiClient(IOptions<AppConfiguration> config, HttpClient client, ILogger<ExampleApiClient> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _client.BaseAddress = new Uri(config?.Value?.ExampleApiUrl ?? throw new ArgumentNullException(nameof(config)));
        }

        public async Task<ResponseWrapperDto<IEnumerable<CarDto>>> GetCarsByFilter(CarsSearchFilterDto filter)
        {
            var content = new StringContent(JsonSerializer.Serialize(filter), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/cars/by-filter", content);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Status Code - {StatusCode}. Reason Phrase - {ReasonPhrase}. Content - {Content}",
                    response.StatusCode, response.ReasonPhrase, await response.Content.ReadAsStringAsync());
                return new ResponseWrapperDto<IEnumerable<CarDto>>("it was not possible to get cars");
            }

            var cars = await JsonSerializer.DeserializeAsync<List<CarDto>>(await response.Content.ReadAsStreamAsync());

            return new ResponseWrapperDto<IEnumerable<CarDto>>(cars);
        }
    }

    public interface IExampleApiClient
    {
        Task<ResponseWrapperDto<IEnumerable<CarDto>>> GetCarsByFilter(CarsSearchFilterDto filter);
    }
}