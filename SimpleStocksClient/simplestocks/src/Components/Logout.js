import React from "react";
import { useEffect, useState } from "react";
import { useNavigate, redirect } from "react-router-dom";

export const Logout = () => {
  const [errors, setErrors] = useState("");
  const [logedIn, setLoggedIn] = useState(true);

  const deleteLocalStorage = () => {
    try {
      localStorage.removeItem("StockUser");
    } catch (error) {
      console.log(error);
    }
  };

  const deletWithClick = () => {
    deleteLocalStorage();
  };
  return (
    <>
      <input type='button' value={"logout"} onClick={deletWithClick}></input>
    </>
  );
};
2;
