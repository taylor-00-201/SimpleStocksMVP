import { StrictMode, useState } from "react";
import ReactDOM from "react-dom";
import Modal from "react-modal";
import { json } from "react-router-dom";

const BuyStockModal = (props) => {
  console.log("BuyStockModal", props);

  const [Quantity, setQuantity] = useState(0);

  const stockUser = JSON.parse(localStorage.getItem("StockUser"));

  const stockUserId = stockUser.id;

  const buyOrSellUserStocks = async () => {
    console.log("here");

    try {
      const response = await fetch(
        `https://localhost:7043/api/Transactions/ProcessTransaction`,
        {
          body: JSON.stringify({
            id: 0,
            userId: stockUserId,
            transactionType: props.ActionType,
            quantity: Quantity,
            assetId: props.stock.id,
            dateTime: new Date(),
            amount:
              props.NewPrice == null || props.NewPrice == undefined
                ? props.stock.currentPrice * Quantity
                : props.NewPrice * Quantity,
          }),
          credentials: "include",
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      console.log(response);
    } catch (error) {
      console.log(error);
    }

    if (props.ActionType === "Sell") {
      const fetchSell = await fetch(
        `https://localhost:7043/api/StockUser/AddToBankAccount`,
        {
          body: JSON.stringify({
            userId: stockUserId,
            balance:
              props.NewPrice == null || props.NewPrice == undefined
                ? props.stock.currentPrice * Quantity
                : props.NewPrice * Quantity,
          }),
          credentials: "include",
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      if (!fetchSell.ok) {
        const errorMessageSell = await fetchSell.text();
        console.log(`there was a network error ${errorMessageSell}`);
      }
    } else if (props.ActionType === "Buy") {
      const fetchBuy = await fetch(
        `https://localhost:7043/api/StockUser/SubtractFromBankAccount`,
        {
          body: JSON.stringify({
            userId: stockUserId,
            balance:
              props.NewPrice == null || props.NewPrice == undefined
                ? props.stock.currentPrice * Quantity
                : props.NewPrice * Quantity,
          }),
          credentials: "include",
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      if (!fetchBuy.ok) {
        const errorMessageBuy = await fetchBuy.text();
        console.log(`there was a network error ${errorMessageBuy}`);
      }
    }
  };

  const customStyles = {
    content: {
      top: "50%",
      left: "50%",
      right: "auto",
      bottom: "auto",
      marginRight: "-50%",
      transform: "translate(-50%, -50%)",
    },
  };

  return (
    <div className='BuyStockModal'>
      <Modal
        isOpen={props.IsOpen}
        style={customStyles}
        contentLabel='Buy Stock'
      >
        <h2>{props.ActionType} Stock</h2>
        <button onClick={() => props.SetIsOpen(false)}>close</button>

        {/* <form> */}
        <h3>{props.stock.name}</h3>
        <p>{props.stock.currentPrice}</p>
        <input
          onChange={(event) => setQuantity(event.target.value)}
          value={Quantity}
          type='Number'
          Name='Quantity'
          id='Quantity'
        />
        <button
          onClick={() => {
            buyOrSellUserStocks();
          }}
        >
          {props.ActionType}
        </button>
        {/* </form> */}
      </Modal>
    </div>
  );
};

export default BuyStockModal;
