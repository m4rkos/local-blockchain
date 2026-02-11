namespace LocalBlockchain.src.database.models
{
    public class TransactionDataDB
    {
        public int Id { get; set; }
        public string? Msg { get; set; }
        public double Amount { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Currency { get; set; }

        public int BlockId { get; set; }
        public BlockDB? Block { get; set; }
    }
}