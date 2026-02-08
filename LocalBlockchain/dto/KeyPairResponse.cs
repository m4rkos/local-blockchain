namespace LocalBlockchain.dto
{
    public record KeyPairResponse(
        string PublicKey,
        string PrivateKey
    )
    { }
}