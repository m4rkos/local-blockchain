using System.Text.Json;
using LocalBlockchain.Dto;
using LocalBlockchain.Models;
using LocalBlockchain.service;

Console.WriteLine("Starting Chain");

var blockchain = new Blockchain();

var (pub, priv) = CryptoService.GenerateKeys();

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

foreach (var item in msgs)
{
    var obj = JsonSerializer.Serialize(item);
    var signature = CryptoService.Sign(obj, priv);
    var tx = item;

    tx = tx with
    {
        Signature = signature,
        PublicKey = pub
    };

    blockchain.AddTransaction(tx);   
}

blockchain.MinePendingTransactions();

Console.WriteLine("Blockchain válida? " + blockchain.IsValid());

foreach (var block in blockchain.Chain)
{
    Console.WriteLine("---------------------");
    Console.WriteLine(new BlockResponse(
        block.Index, 
        block.Hash, 
        block.PreviousHash,
        block.Nonce,
        block.Transactions,
        block.Timestamp
    ));
    if (block.Transactions.Count != 0)
    {
        foreach (var tx in block.Transactions)
        {
            Console.WriteLine(tx);
        }
    }
}