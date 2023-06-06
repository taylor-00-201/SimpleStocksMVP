//import React from "react";
// import { useNavigate } from "react-router-dom";
// import { useEffect, useState } from "react";
// import { AllStocksView } from "./AllStocksView";

const Home = () => {
  function GetAllData() {
    fetch(`http//Localhost:7043/api/Assets/AllAssets`).then((Response) =>
      Response.json().then((data) => {
        console.log(data);
        // data.map((stock) => ({
        //   Symbol: stock.Symbol,
        //   Name: stock.Name,
        //   CurrentPrice: stock.CurrentPrice,
        // }));
        // setAllStocksFetch(stock);
      })
    );
  }
  return(<div>Home</div>)
};

export default Home;
