using LocalBlockchain.Dto;
using LocalBlockchain.service;

namespace LocalBlockchain.Tests;

public class UnitTest1(BlockchainService blockchainService)
{
    private readonly BlockchainService _blockchainService = blockchainService;

    [Fact]
    public async Task Test1Async()
    {
        var transactions = new TransactionData[]
        {
            new("Send Transaction", 1.0, "Marcos", "John", "ETH"),
            new("Send Transaction", 0.2, "John", "Anna", "ETH"),
            new("Send Transaction", 0.45, "John", "Filipa", "ETH"),
            new("Send Transaction", 0.15, "Filipa", "Camila", "ETH"),
            new("Send Transaction", 0.15, "Filipa", "Amanda", "ETH"),
            new("Send Transaction", 0.15, "Filipa", "Pedro", "ETH"),
            new("Send Transaction", 0.05, "Pedro", "Marcos", "ETH")
        };

        var pairs = _blockchainService.GenerateKeyPair();
        _blockchainService.ExcuteTransactions(pairs.PublicKey, pairs.PrivateKey, transactions);
    }
}
