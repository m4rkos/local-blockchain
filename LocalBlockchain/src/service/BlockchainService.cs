
using System.Text.Json;
using LocalBlockchain.src.database.models;
using LocalBlockchain.src.dto;
using LocalBlockchain.src.repository;
using LocalBlockchain.src.service.Models;

namespace LocalBlockchain.src.service
{       
    public class BlockchainService(Blockchain blockchain, BlockchainRepository blockchainRepository)
    {
        private readonly Blockchain _blockchain = blockchain;
        private readonly BlockchainRepository _blockchainRepo = blockchainRepository;

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

        public async Task<List<BlockDB>> GetBlocksFromDB()
        {
            var result = await _blockchainRepo.GetBlocks();
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