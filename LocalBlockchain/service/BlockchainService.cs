
using System.Text.Json;
using LocalBlockchain.dto;
using LocalBlockchain.Dto;
using LocalBlockchain.service.Models;

namespace LocalBlockchain.service
{       
    public class BlockchainService(Blockchain blockchain)
    {
        private readonly Blockchain _blockchain = blockchain;

        public bool BlockchainValid() => _blockchain.IsValid();

        public List<BlockResponse> GetBlocks()
        {
            var result = new List<BlockResponse>();
            foreach (var block in _blockchain.Chain)
            {
                result.Add(new BlockResponse(
                    block.Index, 
                    block.Hash, 
                    block.PreviousHash,
                    block.Nonce,
                    block.Transactions,
                    block.Timestamp
                ));
            }
            return result;
        }

        public void ExcuteTransactions(
            string pub, 
            string priv,
            TransactionData[] transactionDatas)
        {
            foreach (var item in transactionDatas)
            {
                var obj = JsonSerializer.Serialize(item);
                var signature = CryptoService.Sign(obj, priv);
                var tx = item;

                tx = tx with
                {
                    Signature = signature,
                    PublicKey = pub
                };

                _blockchain.AddTransaction(tx);   
            }
        }

        public KeyPairResponse GenerateKeyPair()
        {
            var (pub, priv) = CryptoService.GenerateKeys();
            return new KeyPairResponse(pub, priv);
        }
    }
}