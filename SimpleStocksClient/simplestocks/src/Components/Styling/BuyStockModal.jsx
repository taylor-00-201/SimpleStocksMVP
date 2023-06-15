import { StrictMode, useState } from "react";
import ReactDOM from "react-dom";
import Modal from "react-modal";
import { json } from "react-router-dom";

const BuyStockModal = (props) => {
  console.log("BuyStockModal", props);

  const stockUser = JSON.parse(localStorage.getItem("StockUser"));

  const stockUserId = stockUser.id;

  const buyOrSellUserStocks = async () => {
    console.log("here");
    let quantity = document.querySelector("#Quantity").value;
    try {
      const response = await fetch(
        `https://localhost:7043/api/Transactions/ProcessTransaction`,
        {
          body: JSON.stringify({
            id: 0,
            userId: stockUserId,
            transactionType: props.ActionType,
            quantity: quantity,
            assetId: props.stock.id,
            dateTime: new Date(),
            orderId: Math.max(Math.min(Math.random(), 600), 100),
            amount:
              props.NewPrice == null || props.NewPrice == undefined
                ? props.stock.currentPrice * quantity
                : props.NewPrice * quantity,
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
        <input type='Number' Name='Quantity' id='Quantity' />
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
