using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using SimpleStocks.Interfaces;
using SimpleStocks.Models;
using SimpleStocks.Repositories;

namespace SimpleStocks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsRepository _transactioRepo;

        public TransactionsController(ITransactionsRepository transactionsRepo) 
        {
            _transactioRepo = transactionsRepo;
        }
        
        [HttpGet("TransactionByIdBuy")]
        public IActionResult ReturnTransationByUserIdBuy(int Id) 
        {
            var ReturnedTransactionsBuy = _transactioRepo.ReturnTransactionsByUserIdBuy(Id);
            return Ok(ReturnedTransactionsBuy);
        }

        [HttpGet("TransactionByIdSell")]
        public IActionResult ReturnTransationByUserIdSell(int Id)
        {
            var ReturnedTransactionsSell = _transactioRepo.ReturnTransactionsByUserIdSell(Id);
            return Ok(ReturnedTransactionsSell);
        }

        [HttpGet("TransactionByIdAll")]
        public IActionResult ReturnTransationByUserId(int Id)
        {
            var ReturnedTransactionsAll = _transactioRepo.ReturnTransactionsByUserId(Id);
            return Ok(ReturnedTransactionsAll);
        }


        [HttpPost("ProcessTransaction")]
        public IActionResult ProcessTransaction(Transactions transaction)
        {
            _transactioRepo.ProcessTransaction(transaction);
            return NoContent();
        }

        [HttpPut("UpdateTransaction")]
        public IActionResult UpdateTransaction(Transactions transaction, int Id) 
        {
            _transactioRepo.UpdateTransaction(transaction, Id);
            return NoContent();
        }

        [HttpDelete("DeleteTransaction")]
        public IActionResult DeleteTransaction(int Id)
        {
            _transactioRepo.DeleteTransaction(Id);
            return NoContent();
        }
    }
}
