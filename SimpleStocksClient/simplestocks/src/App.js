import React, { useState, useEffect } from "react";
import "./App.css";
import { Routes, Route } from "react-router-dom";
import Layout from "./Components/Layout";
import Settings from "./Views/Settings";
import { Login } from "./Components/Login";
import { BuyStock } from "./Components/BuyStock";
import { Logout } from "./Components/Logout";

export default function App() {
  const [user, setUser] = useState();
  const [Display, setDisplay] = useState("Login");
  const [IsLoggedIn, setIsLoggedIn] = useState(false);
  const [ReRender, setReRender] = useState(false);

  useEffect(() => {
    let stockUser = localStorage.getItem("StockUser");
    if (
      localStorage.getItem("StockUser") !== null &&
      localStorage.getItem("StockUser") !== undefined
    ) {
      setIsLoggedIn(true);
      setDisplay("BuyStock");
      setUser(JSON.parse(stockUser));
      setReRender(false);
    }
  }, []);

  useEffect(() => {
    if (user === null || user === undefined) {
      if (
        localStorage.getItem("StockUser") === null ||
        localStorage.getItem("StockUser") === undefined
      ) {
        alert("You are not logged in!");
      }
    }
  }, []);

  return (
    <div className='App'>
      <Layout
        IsLoggedIn={IsLoggedIn}
        SetDisplay={setDisplay}
        SetIsLoggedIn={setIsLoggedIn}
        SetReRender={setReRender}
      />
      {Display === "Login" && (
        <Login
          setDisplay={setDisplay}
          setIsLoggedIn={setIsLoggedIn}
          SetUserData={setUser}
        />
      )}
      {Display === "BuyStock" && <BuyStock User={user} />}
      {Display === "Settings" && <Settings />}
    </div>
  );
}
