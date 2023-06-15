import { useState } from "react";
import BuyStockModal from "./Styling/BuyStockModal";

const StockCard = (props) => {
  console.log("stockCardProps", props);
  const[IsOpen, setIsOpen]=useState(false);
  const [newStockPrice, setNewStockPrice] = useState();

  let anotherStockPrice =
    Math.random(props.stock.currentprice) + props.stock.currentPrice;
  const newRandomPrice = Math.max(Math.min(Math.round(anotherStockPrice * 1.5), 600), 100);



  return (
    <div>
      <h1>Symbol: {props.stock.symbol}</h1>
      <h1>Stock Name: {props.stock.name}</h1>
      <h1>Last Price: {props.stock.currentPrice}</h1>
      <h1>New Price: {newRandomPrice}</h1>
      <button onClick={()=>setIsOpen(true)}>Buy Stock</button>
      <BuyStockModal stock={props.stock} NewPrice={newRandomPrice} IsOpen={IsOpen} SetIsOpen={setIsOpen} ActionType={"Buy"} />
    </div>
  );
};

export default StockCard;
