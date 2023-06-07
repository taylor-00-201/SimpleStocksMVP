import React, { useState, useEffect } from "react";
import "./App.css";
import { Routes, Route } from "react-router-dom";
import Home from "./Views/Home";
import Layout from "./Components/Layout";
import Settings from "./Views/Settings";
import { Login } from "./Components/Login";

export default function App() {
  const [user, setUser] = useState();

  return (
    <Routes>
      <Route path='/login' element={<Login SetUser={setUser} />} />
      <Route path='/' element={<Layout />} />
      <Route path='/home' index element={<Home user={user} />} />
      <Route path='/settings' element={<Settings user={user} />} />
    </Routes>
  );
}
