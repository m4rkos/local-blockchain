using System.Text.Json;
using LocalBlockchain.Dto;

namespace LocalBlockchain.Models
{
    public class Blockchain
    {
        public List<Block> Chain { get; set; } = [];
        public int Difficulty { get; set; } = 3;
        public List<TransactionData> PendingTransactions { get; set; } = [];


        public Blockchain()
        {
            Chain.Add(CreateGenesisBlock());
        }

        private Block CreateGenesisBlock()
        {   
            var genesis = new Block(
                0, [new("Genesis Block", 0.0)], "0"
            );
            genesis.Mine(Difficulty);
            return genesis;
        }

        public Block GetLatestBlock() => Chain[^1];

        public void AddTransaction(TransactionData tx)
        {
            PendingTransactions.Add(tx);
        }

        public void MinePendingTransactions()
        {
            if (PendingTransactions.Count == 0)
                return;
            
            var block = new Block(
                Chain.Count,
                [.. PendingTransactions],
                GetLatestBlock().Hash
            );

            AddBlock(block);

            PendingTransactions.Clear();
        }


        public void AddBlock(Block block)
        {
            block.PreviousHash = GetLatestBlock().Hash;
            block.Mine(Difficulty);
            Chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var current = Chain[i];
                var previous = Chain[i - 1];

                if (current.Hash != current.CalculateHash())
                    return false;

                if (current.PreviousHash != previous.Hash)
                    return false;
            }
            return true;
        }
    }
}
