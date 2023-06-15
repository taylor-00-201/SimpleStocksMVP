import React from "react";
import { useEffect, useState } from "react";
import { useFetcher } from "react-router-dom";
import StockCard from "./StockCard";
import BuyStockEngine from "./BuyStockEngine";
import BuyStockModal from "./Styling/BuyStockModal";

export const BuyStock = (props) => {
  const [userTransactions, setUserTransactions] = useState([]);
  const [matchingStockTransaction, setMatchingStockTransaction] = useState([]);
  const [stocks, setStocks] = useState([]);
  const [userStocks, setUserStocks] = useState([]);

  const parsedTransactions = userTransactions;
  const StockUser = JSON.parse(localStorage.getItem("StockUser"));
  const stockUserId = StockUser ? StockUser.id : null;
  const[IsOpen, setIsOpen]=useState(false);

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
      .then((data) => {
        let updatedData = [];
        for (let i = 0; i < data.length; i++) {
          let anotherStockPrice =
            Math.random(data[i].currentPrice) + data[i].currentPrice;
          let newRandomPrice = Math.max(
            Math.min(Math.round(anotherStockPrice * 1.5), 600),
            100
          );
          let singleInstance = data[i];
          singleInstance.currentPrice = newRandomPrice;
          updatedData.push(singleInstance);
        }
        setStocks(updatedData);
      });
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
      {stocks.length !== 0 &&
        stocks.map((stock, index) => <StockCard index={index} stock={stock} />)}
      <BuyStockEngine stock={stocks} />
      <div>
        {matchingStockTransaction.length !== 0 &&
          matchingStockTransaction.map((match, index) => (
            <div key={index}>
              <div>
                <h1>transaction History</h1>
                <h1>transaction Type: {match.transaction.transactionType}</h1>
                <h1>Quantity: {match.transaction.quantity}</h1>
                <h1>Date: {match.transaction.datetime}</h1>
              </div>
              <div>
                <h1>Stock</h1>
                <h1>Stock Symbol: {match.stock.symbol}</h1>
                <h1>Stock Name: {match.stock.name}</h1>
                <h1>Stock Price: {match.stock.currentPrice}</h1>
                <button onClick={() => setIsOpen(true)}>Sell Stock</button>
                <BuyStockModal
                  stock={match.stock}
                  NewPrice={match.stock.currentPrice}
                  IsOpen={IsOpen}
                  SetIsOpen={setIsOpen}
                  ActionType={"Sell"}
                />
              </div>
              {console.log(match.stock, match.transaction)}
            </div>
          ))}
      </div>
    </div>
  );
};
