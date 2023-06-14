import React from "react";
import { useState, useEffect } from "react";

const RegisterStockUser = () => {
  const [userName, setUserName] = useState("");
  const [Email, setEmail] = useState("");
  const [FirstName, setFirstName] = useState("");
  const [LastName, setLastName] = useState("");
  const [passwordHash, setPasswordHash] = useState("");
  const [addressLineOne, setAddressLineOne] = useState("");
  const [addressLineTwo, setAddressLineTwo] = useState("");
  const [City, setCity] = useState("");
  const [State, setState] = useState("");
  const [Zip, setZip] = useState("");

  // const stockUser = JSON.parse(localStorage.getItem("StockUser"));

  // const stockUserId = stockUser.id;

  const fetchDataPost = async () => {
    try {
      const response = await fetch(
        `https://localhost:7043/api/StockUser/registeruser`,
        {
          body: JSON.stringify({
            Id: 0,
            UserName: userName,
            Email: Email,
            FirstName: FirstName,
            LastName: LastName,
            IsAdmin: false,
            AddressLineOne: addressLineOne,
            AddressLineTwo: addressLineTwo,
            City: City,
            State: State,
            Zip: Zip,
            PasswordHash: passwordHash,
            Balance: 0,
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

  const submissionHandler = (event) => {
    event.preventDefault();
    fetchDataPost();
  };

  return (
    <div>
      <h1>Not a member yet? Register Your Account Here</h1>

      <form onSubmit={submissionHandler}>
        <label>
          UserName:
          <input
            type='text'
            value={userName}
            onChange={(event) => setUserName(event.target.value)}
          ></input>
        </label>
        Email:
        <input
          type='text'
          value={Email}
          onChange={(event) => setEmail(event.target.value)}
        ></input>
        <label></label>
        First Name:
        <input
          type='text'
          value={FirstName}
          onChange={(event) => setFirstName(event.target.value)}
        ></input>
        <label></label>
        Last Name:
        <input
          type='text'
          value={LastName}
          onChange={(event) => setLastName(event.target.value)}
        ></input>
        <label></label>
        <label></label>
        Password:
        <input
          type='text'
          value={passwordHash}
          onChange={(event) => setPasswordHash(event.target.value)}
        ></input>
        <label></label>
        Address Line One:
        <input
          type='text'
          value={addressLineOne}
          onChange={(event) => setAddressLineOne(event.target.value)}
        ></input>
        <label></label>
        Address Line Two:
        <input
          type='text'
          value={addressLineTwo}
          onChange={(event) => setAddressLineTwo(event.target.value)}
        ></input>
        <label></label>
        City:
        <input
          type='text'
          value={City}
          onChange={(event) => setCity(event.target.value)}
        ></input>
        <label></label>
        State:
        <input
          type='text'
          value={State}
          onChange={(event) => setState(event.target.value)}
        ></input>
        <label></label>
        Zip:
        <input
          type='text'
          value={Zip}
          onChange={(event) => setZip(event.target.value)}
        ></input>
        <label>
          <input type='submit' value='submit'></input>
        </label>
      </form>
    </div>
  );
};

export default RegisterStockUser;
