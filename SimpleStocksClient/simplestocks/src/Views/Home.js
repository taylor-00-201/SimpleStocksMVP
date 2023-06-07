import React from "react";
import { useNavigate, redirect } from "react-router-dom";
import { useEffect, useState } from "react";

const Home = (props) => {
  console.log("homeprops", props);
  const [stockData, setStockData] = useState([]);

  const navigate = useNavigate();

  useEffect(() => {
    if (props.user === null || props.user === undefined) {
      if (
        localStorage.getItem("StockUser") === null ||
        localStorage.getItem("StockUser") === undefined
      ) {
        alert("You are not logged in!");
        navigate("/login");
      } else {
        setUser(JSON.parse(localStorage.StockUser));
      }
    }
  }, [props.user]);

  const fetchData = async () => {
    try {
      await fetch(`https://localhost:7043/api/Assets/AllAssets
            `).then((Response) =>
        Response.json().then((data) => setStockData(data))
      );

      console.log(stockData);
    } catch (error) {}
  };

  useEffect(() => {
    fetchData();
  }, []);

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
