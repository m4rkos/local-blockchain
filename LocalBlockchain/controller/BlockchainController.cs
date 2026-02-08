using LocalBlockchain.dto;
using LocalBlockchain.Dto;
using LocalBlockchain.service;
using Microsoft.AspNetCore.Mvc;

namespace LocalBlockchain.controller
{
    [Route("api/blockchain")]
    public class BlockchainController(BlockchainService blockchainService) : ControllerBase
    {
        private readonly BlockchainService _blockchainService = blockchainService;

        [HttpGet("/")]
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