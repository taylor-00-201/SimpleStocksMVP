import React from "react";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

const Home = () => {
  const [stockData, setStockData] = useState([]);

  const fetchData = async () => {
    try {
      await fetch(`https://localhost:7043/api/Assets/AllAssets
            `).then((Response) =>
        Response.json().then((data) => setStockData(data))
      );

      console.log(stockData);
    } catch (error) {}
  };

  // function GetAllData() {
  // fetch(`http//Localhost:7043/api/Assets/AllAssets`).then((Response) =>
  //   Response.json().then((data) => {
  //     console.log(data);
  //     data.map((stock) => ({
  //       Symbol: stock.Symbol,
  //       Name: stock.Name,
  //       CurrentPrice: stock.CurrentPrice,
  //     }));
  //     setAllStocksFetch(stock);
  //   })
  // );

  return <button onClick={fetchData}>See Stocks</button>;
};

export default Home;
