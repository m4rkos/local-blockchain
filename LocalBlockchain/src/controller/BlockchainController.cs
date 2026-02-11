using LocalBlockchain.src.database.models;
using LocalBlockchain.src.dto;
using LocalBlockchain.src.service;
using Microsoft.AspNetCore.Mvc;

namespace LocalBlockchain.src.controller
{
    [Route("api/blockchain")]
    [ApiController]
    public class BlockchainController(BlockchainService blockchainService) : Controller
    {
        private readonly BlockchainService _blockchainService = blockchainService;

        [HttpGet("")]
        public async Task<IActionResult> Health()
        {
            try
            {
                return Ok("Is Blockchain valid? " + _blockchainService.BlockchainValid());    
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("get-blocks")]
        public async Task<ActionResult<List<BlockResponse>>> GetBlocks()
        {
            return _blockchainService.GetBlocks();
        }

        [HttpGet("db-blocks")]
        public async Task<IActionResult> GetBlocksFromDB()
        {
            var blocks = _blockchainService.GetBlocksFromDB();
            return Ok(blocks);
        }

        [HttpPost("add-transctionn")]
        public async Task<IActionResult> AddTransaction(
            [FromHeader] string pub, 
            [FromHeader] string priv,
            [FromBody] TransactionData[] transactionDatas)
        {
            if (transactionDatas is null) 
                return BadRequest("Transactions not found");

            try
            {
                _blockchainService.ExcuteTransactions(pub, priv, transactionDatas);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
            return Ok();
        }

        [HttpGet("generate-key-pair")]
        public async Task<ActionResult<KeyPairResponse>> GenerateKeyPair()
        {
            return Ok(_blockchainService.GenerateKeyPair());
        }
    }
}