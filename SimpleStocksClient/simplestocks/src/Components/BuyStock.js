import React from "react";
import { useEffect, useState } from "react";
import { useFetcher } from "react-router-dom";

export const BuyStock = () => {
  let [userStock, setUserStock] = useState([]);

  const FetchUserStock = async (Id) => {
    const apiResponse = await fetch(
      $`https://localhost:7043/api/Transactions/TransactionByIdAll?Id=${Id}`
    )
      .then((response) => response.json())
      .then((data) => setUserStock(data));
  };

  const Id = localStorage.getItem('Id')

  useEffect(() => 
  {
    if(Id === !null)
    {
        FetchUserStock(Id)
    }    
  }, [])


  const BuyStock = () => 
  {
    
    
    return
    (
        
    )
  }

};
