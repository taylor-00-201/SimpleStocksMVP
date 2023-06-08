import React from "react";
import { useEffect, useState } from "react";
import { useFetcher } from "react-router-dom";
import StockCard from "./StockCard";

export const BuyStock = () => {
  let [userTransactions, setUserTransactions] = useState([]);
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

  // const calculateStocksAndRender = (stockUserId) => {
    
  // };

  //const buySellUserStocks = () => {};

  useEffect(() => {
    if (stockUserId) {
      fetchUserTransactions(stockUserId);
      fetchStocks();
    }
  }, [stockUserId]);

  useEffect(() => {
    console.log(userTransactions);
    console.log(stocks);
  }, [userTransactions, stocks]);

  return (
    <div>
      {stocks.length !== 0 &&
        stocks.map((stock, index) => 
          <StockCard index={index} stock={stock} />
        )}
    </div>
  );
};
