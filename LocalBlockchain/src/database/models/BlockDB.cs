
namespace LocalBlockchain.src.database.models
{
    public class BlockDB
    {
        public int Id { get; set; }
        public required string Hash { get; set; }
        public string? PrevHash { get; set; }
        public long Nonce { get; set; }
        public DateTime Timestamp { get; set; }

        public List<TransactionDataDB>? Transactions { get; set; }
    }
}