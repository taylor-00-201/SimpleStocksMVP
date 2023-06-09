import React from "react";
import { useEffect, useState } from "react";
import { useFetcher } from "react-router-dom";
import StockCard from "./StockCard";
import BuyStockEngine from "./BuyStockEngine";

export const BuyStock = () => {
  let [userTransactions, setUserTransactions] = useState([]);
  let [matchingStockTransaction, setMatchingStockTransaction] = useState([]);
  let [stocks, setStocks] = useState([]);
  let [userStocks, setUserStocks] = useState([]);

  const parsedTransactions = userTransactions;
  const StockUser = JSON.parse(localStorage.getItem("StockUser"));
  const stockUserId = StockUser ? StockUser.id : null;

  // const returnUserTransactions = () => {
  //   return (
  //     <div>
  //       {parsedTransactions.map((transaction) => {
  //         return (
  //           <div key={transaction.datetime}>
  //             <h1>Transaction Type: {transaction.transactiontype}</h1>
  //             <h1>Quantity: {transaction.quantity}</h1>
  //             <h1>Asset Id: {transaction.assetId}</h1>
  //             <h1>Date: {transaction.datetime}</h1>
  //           </div>
  //         );
  //       })}
  //     </div>
  //   );
  // };

  const fetchUserTransactions = async (stockUserId) => {
    const apiResponse = await fetch(
      `https://localhost:7043/api/Transactions/TransactionByIdAll?Id=${stockUserId}`
    )
      .then((response) => response.json())
      .then((data) => setUserTransactions(data));
  };

  const fetchStocks = async () => {
    const apiResponse = await fetch(
      `https://localhost:7043/api/Assets/AllAssets`
    )
      .then((response) => response.json())
      .then((data) => setStocks(data));
  };

  const identifyUserStocks = () => {
    let matches = [];

    userTransactions.forEach((transaction) => {
      stocks.forEach((stock) => {
        if (transaction.assetId === stock.id) {
          matches.push({ transaction, stock });
        }
      });
    });
    setMatchingStockTransaction(matches);
  };

  useEffect(() => {
    if (stockUserId) {
      fetchUserTransactions(stockUserId);
      fetchStocks();
      identifyUserStocks();
    }
  }, [stockUserId]);

  useEffect(() => {
    console.log(userTransactions);
    console.log(stocks);
    console.log(matchingStockTransaction);
  }, [userTransactions, stocks]);

  useEffect(() => {
    identifyUserStocks();
  }, [userTransactions, stocks]);

  useEffect(() => {
    console.log(matchingStockTransaction);
  }, [matchingStockTransaction]);

  return (
    <div>
      {/* {stocks.length !== 0 &&
        stocks.map((stock, index) => <StockCard index={index} stock={stock} />)} */}
      <div>{<BuyStockEngine />}</div>
      <div>
        {matchingStockTransaction.length !== 0 &&
          matchingStockTransaction.map((match, index) => (
            <div key={index}>
              <div>
                <h1>transaction</h1>
                <h1>transaction Type: {match.transaction.transactionType}</h1>
                <h1>Quantity: {match.transaction.quantity}</h1>
                <h1>Date: {match.transaction.datetime}</h1>
              </div>
              <div>
                <div>
                  <h1>Stock</h1>
                  <h1>Stock Symbol: {match.stock.symbol}</h1>
                  <h1>Stock Name: {match.stock.name}</h1>
                  <h1>Stock Price: {match.stock.currentPrice}</h1>
                </div>
              </div>
              {console.log(match.stock, match.transaction)}
            </div>
          ))}
      </div>
    </div>
  );
};
