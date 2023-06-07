import React from "react";
import "./App.css";
import { Routes, Route } from "react-router-dom";
import Home from "./Views/Home";
import Layout from "./Components/Layout";
import Settings from "./Views/Settings";
import { Login } from "./Components/Login";

export default function App() {
  return (
       <Routes> 
        <Route path="/login" element={<Login />}/>
        <Route path='/' element={<Layout />}/>
          <Route path="/home" index element={<Home />} /> 
          <Route path='/settings' element={<Settings />} />
      </Routes>
  );
}
