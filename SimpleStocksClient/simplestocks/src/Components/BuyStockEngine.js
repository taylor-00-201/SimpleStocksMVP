import { useEffect, useState } from "react";

const BuyStockEngine = (props) => {
  const [userInputQuantity, setuserInputQuantity] = useState("");

  const [newStockPrice, setNewStockPrice] = useState();


  const buyUserStocks = (props) => {
    const stockFetchPostBuy = fetch(
      `https://localhost:7043/api/Transactions/ProcessTransaction`,
      {
        body: JSON.stringify({
          id: 0,
          userId: localStorage.getItem("StockUser").id,
          transactionType: "Buy",
          quantity: userInputQuantity,
          assetId: props.stock.assetId,
          dateTime: dateTime,
          orderId: Math.random(),
          AssetPrice: props.stock.currentPrice,
          amount: "",
        }),
        credentials: "include",
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );

    return (
      <div>
        <form onSubmit={submissionHandler}>
          <label>
            Asset:
            <select
              type='text'
              value={props.stock.name}
              onChange={(event) => setUserEmail(event.target.value)}
            ></select>
          </label>
          <label>
            Asset Price:
            <h1>{() => {}}</h1>
          </label>
          Quantity :
          <input
            type='text'
            value={PasswordHash}
            onChange={(event) => setPasswordHash(event.target.value)}
          ></input>
          <label>
            <input type='submit' value='submit'></input>
          </label>
        </form>
      </div>
    );
  };

  const sellUserStocks = (props) => {
    const stockFetchPostSell = fetch(
      `https://localhost:7043/api/Transactions/ProcessTransaction`,
      {
        body: JSON.stringify({
          id: 0,
          userId: localStorage.getItem("StockUser").id,
          transactionType: "Sell",
          quantity: userInputQuantity,
          assetId: props.stock.assetId,
          dateTime: dateTime,
          orderId: Math.random(),
          AssetPrice: props.stock.currentPrice,
          amount: "",
        }),
        credentials: "include",
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
      }
    );
    return (
      <div>
        <form onSubmit={submissionHandler}>
          <label>
            Email:
            <input
              type='text'
              value={Email}
              onChange={(event) => setUserEmail(event.target.value)}
            ></input>
          </label>
          Password:
          <input
            type='text'
            value={PasswordHash}
            onChange={(event) => setPasswordHash(event.target.value)}
          ></input>
          <label>
            <input type='submit' value='submit'></input>
          </label>
        </form>
      </div>
    );
  };

  const submissionHandler = () => {
    useEffect(() => {
      StockCardClone();
      buyUserStocks();
      sellUserStocks();
    }, []);
  };

  return <div>This is the stock engine component!</div>;
};

export default BuyStockEngine;
