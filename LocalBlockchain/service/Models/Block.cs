using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using LocalBlockchain.Dto;

namespace LocalBlockchain.service.Models
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<TransactionData> Transactions { get; set; }
        public string PreviousHash { get; set; }
        public long Nonce { get; set; }
        public string Hash { get; set; }

        public Block(int index, List<TransactionData> txs, string previousHash)
        {
            Index = index;
            Transactions = txs;
            PreviousHash = previousHash;
            Timestamp = DateTime.UtcNow;
            Hash = CalculateHash();
        }

        public string CalculateHash()
        {
            using var sha = SHA256.Create();

            var jsonTx = JsonSerializer.Serialize(Transactions);
            
            var raw = $"{Index}{Timestamp}{jsonTx}{PreviousHash}{Nonce}";
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(raw));

            return Convert.ToHexString(bytes);
        }

        public void Mine(int difficulty)
        {
            var prefix = new string('0', difficulty);

            while (!Hash.StartsWith(prefix))
            {
                Nonce++;
                Hash = CalculateHash();
            }
        }
    }
}
