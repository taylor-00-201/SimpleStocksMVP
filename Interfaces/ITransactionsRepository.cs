﻿using SimpleStocks.Models;

namespace SimpleStocks.Interfaces
{
    public interface ITransactionsRepository
    {
        void ProcessTransaction(Transactions transaction);
        List<Transactions> ReturnTransactionsByUserId(int UserId);
        List<Transactions> ReturnTransactionsByUserIdBuy(int UserId);
        List<Transactions> ReturnTransactionsByUserIdSell(int UserId);
    }
}