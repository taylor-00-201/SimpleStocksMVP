import { useState } from "react";
import { useEffect } from "react";
import { Link } from "react-router-dom";
import { useNavigate } from "react-router-dom";

export const AllStocksView = () => {
  const [allStocksFetch, setAllStocksFetch] = useState([]);

  const AllStocksView = () => {
    const [allStocksFetch, setAllStocksFetch] = useState([]);


    useEffect(() => {
        fetch(`http//Localhost:7043/api/Assets/AllAssets`).then((Response) =>
          Response.json().then((data) => {
            data.map((stock) => ({
              Symbol: stock.Symbol,
              Name: stock.Name,
              CurrentPrice: stock.CurrentPrice,
            }));
            setAllStocksFetch(stock);
          })
        );
      }, []);
      
      const 

      return (
        <div>
          <div>
            <button onClick={(allStocksFetch.map((data) => {<div key={data.Symbol}>
                <h1>Symbol: {data.Symbol}</h1>
                <h1>Name: {data.Name}</h1>
                <h1>CurrentPrice: {data.CurrentPrice}</h1>
              </div>}))}>See All Stocks</button>
          </div>
          <div className=''>

          </div>
          <div className=''></div>
        </div>
      );
  };
};
