import { useState } from "react";

const StockCard = (props) => {
  console.log("stockCardProps", props);

  const [newStockPrice, setNewStockPrice] = useState();

  let anotherStockPrice =
    Math.random(props.stock.currentprice) + props.stock.currentPrice;

  const newRandomPrice = Math.round(anotherStockPrice * 100) / 100;

  return (
    <div>
      <h1>Symbol: {props.stock.symbol}</h1>
      <h1>Stock Name: {props.stock.name}</h1>
      <h1>Stock Price: {props.stock.currentPrice}</h1>
      <h1>Stock Price: {newRandomPrice}</h1>
    </div>
  );
};

export default StockCard;