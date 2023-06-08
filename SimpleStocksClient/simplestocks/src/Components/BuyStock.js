import React from "react";
import { useEffect, useState } from "react";

export const BuyStock = () => {
  let [userTransactions, setUserTransactions] = useState([]);

  const parsedTransactions = userTransactions;
  const StockUser = JSON.parse(localStorage.getItem("StockUser"));
  const stockUserId = StockUser ? StockUser.id : null;

  const returnUserTransactions = () => {
    return (
      <div>
        {parsedTransactions.map((transaction) => {
          return (
            <div key={transaction.datetime}>
              <h1>Transaction Type: {transaction.transactiontype}</h1>
              <h1>Quantity: {transaction.quantity}</h1>
              <h1>Asset Id: {transaction.assetId}</h1>
              <h1>Date: {transaction.datetime}</h1>
            </div>
          );
        })}
      </div>
    );
  };

  const fetchUserTransactions = async (stockUserId) => {
    const apiResponse = await fetch(
      `https://localhost:7043/api/Transactions/TransactionByIdAll?Id=${stockUserId}`
    )
      .then((response) => response.json())
      .then((data) => setUserTransactions(data));

    returnUserTransactions();
  };

  //const buySellUserStocks = () => {};

  useEffect(() => {
    if (stockUserId) {
      fetchUserTransactions(stockUserId);
    }
  }, []);

  return returnUserTransactions();
};
