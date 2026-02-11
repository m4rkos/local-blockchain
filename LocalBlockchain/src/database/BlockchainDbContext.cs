using Microsoft.EntityFrameworkCore;
using LocalBlockchain.src.database.models;

namespace LocalBlockchain.src.database
{
    public class BlockchainDbContext(DbContextOptions<BlockchainDbContext> options) : DbContext(options)
    {
        public DbSet<BlockDB> Blocks { get; set; }
        public DbSet<TransactionDataDB> Transactions { get; set; }
    }
}