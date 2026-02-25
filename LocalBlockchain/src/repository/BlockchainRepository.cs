using LocalBlockchain.src.database;
using LocalBlockchain.src.database.models;

namespace LocalBlockchain.src.repository
{
    public class BlockchainRepository(BlockchainDbContext blockchainDbContext)
    {
        private readonly BlockchainDbContext _context = blockchainDbContext;

        public async Task<List<BlockDB>> GetBlocks()
        {
            return [.. _context.Blocks];
        }
    }
}
