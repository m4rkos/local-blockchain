using System.Security.Cryptography;
using System.Text;

namespace LocalBlockchain.src.service
{
    public class CryptoService
    {
        public static (string publicKey, string privateKey) GenerateKeys()
        {
            using var ecdsa = ECDsa.Create(ECCurve.NamedCurves.nistP256);

            var privateKey = Convert.ToBase64String(ecdsa.ExportECPrivateKey());
            var publicKey = Convert.ToBase64String(ecdsa.ExportSubjectPublicKeyInfo());

            return (publicKey, privateKey);
        }

        public static string Sign(string data, string privateKeyBase64)
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportECPrivateKey(Convert.FromBase64String(privateKeyBase64), out _);

            var bytes = Encoding.UTF8.GetBytes(data);
            var signature = ecdsa.SignData(bytes, HashAlgorithmName.SHA256);

            return Convert.ToBase64String(signature);
        }

        public static bool Verify(string data, string signatureBase64, string publicKeyBase64)
        {
            using var ecdsa = ECDsa.Create();
            ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(publicKeyBase64), out _);

            var dataBytes = Encoding.UTF8.GetBytes(data);
            var sigBytes = Convert.FromBase64String(signatureBase64);

            return ecdsa.VerifyData(dataBytes, sigBytes, HashAlgorithmName.SHA256);
        }
    }
}