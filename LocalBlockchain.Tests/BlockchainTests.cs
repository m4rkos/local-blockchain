using LocalBlockchain.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace LocalBlockchain.LocalBlockchain.Tests
{
    public class BlockchainTests(
        WebApplicationFactory<Program> webApplicationFactory,
        WebApplicationFactory<Program> factory
    ) : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _webApplicationFactory = webApplicationFactory;

        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task ShouldAddTransactionsAsync()
        {  
            // headers
            _client.DefaultRequestHeaders.Add("pub", "testPub");
            _client.DefaultRequestHeaders.Add("priv", "testPriv");

            var msgs = new TransactionData[]
            {
                new("Send Transaction", 1.0, "Marcos", "John", "ETH"), 
                new("Send Transaction", 0.2, "John", "Anna", "ETH"),
                new("Send Transaction", 0.45, "John", "Filipa", "ETH"),
                new("Send Transaction", 0.15, "Filipa", "Camila", "ETH"),
                new("Send Transaction", 0.15, "Filipa", "Amanda", "ETH"),
                new("Send Transaction", 0.15, "Filipa", "Pedro", "ETH"),
                new("Send Transaction", 0.05, "Pedro", "Marcos", "ETH")
            };

            var response = await _client.PostAsJsonAsync(
                "/api/blockchain/add-transaction", msgs
            );

            response.EnsureSuccessStatusCode();
        }
    }
}
