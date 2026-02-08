namespace LocalBlockchain.Dto
{
    public record BlockResponse(
        int Index, 
        string Hash,
        string Prev,
        long Nonce,
        List<TransactionData> Data,
        DateTime Timestamp
    )
    { }

    public record TransactionData(
        string? Msg,
        double Amount,
        string? From = "UNKNOWN",
        string? To = "UNKNOWN",
        string? Currency = "ALL",
        string? Signature = null,
        string? PublicKey = null
    )
    { }
}