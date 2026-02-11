namespace LocalBlockchain.src.dto
{
    public record KeyPairResponse(
        string PublicKey,
        string PrivateKey
    )
    { }
}