import React from "react";
import { useState, useEffect } from "react";
import { json } from "react-router-dom";
import { DeleteUser } from "../Components/Styling/DeleteUser";
import "../Components/Styling/Settings.css"

const Settings = () => {
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

  //const StockUser = JSON.parse(localStorage.getItem("StockUser"));
  //const stockUserId = StockUser ? StockUser.id : null;

  const stockUser = JSON.parse(localStorage.getItem("StockUser"));

  const stockUserId = stockUser.id;


  const fetchDataPost = async () => {
    try {
      const response = await fetch(
        `https://localhost:7043/api/StockUser/updateuser?Id=${stockUserId}
        `,
        {
          body: JSON.stringify({
            Id: stockUserId,
            UserName: userName,
            Email: Email,
            FirstName: FirstName,
            LastName: LastName,
            PasswordHash: passwordHash,
            IsAdmin: false,
            AddressLineOne: addressLineOne,
            AddressLineTwo: addressLineTwo,
            City: City,
            State: State,
            Zip: Zip,
          }),
          credentials: "include",
          method: "PUT",
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
    <div className="Main">
      <h1>Modify User Settings Here</h1>

      <form onSubmit={submissionHandler}>
          <input
            type='text'
            value={userName}
            onChange={(event) => setUserName(event.target.value)}
            placeholder="UserName"
          ></input>
        <input
          type='text'
          value={Email}
          onChange={(event) => setEmail(event.target.value)}
          placeholder="Email"
        ></input>
        <input
          type='text'
          value={FirstName}
          onChange={(event) => setFirstName(event.target.value)}
          placeholder="First Name"
        ></input>
        <input
          type='text'
          value={LastName}
          onChange={(event) => setLastName(event.target.value)}
          placeholder="Last Name"
        ></input>
        <input
          type='text'
          value={passwordHash}
          onChange={(event) => setPasswordHash(event.target.value)}
          placeholder="Password"
        ></input>
        <input
          type='text'
          value={addressLineOne}
          onChange={(event) => setAddressLineOne(event.target.value)}
          placeholder="Address Line One"
        ></input>
        <input
          type='text'
          value={addressLineTwo}
          onChange={(event) => setAddressLineTwo(event.target.value)}
          placeholder="Address Line Two"
        ></input>
        <input
          type='text'
          value={City}
          onChange={(event) => setCity(event.target.value)}
          placeholder="City"
        ></input>
        <input
          type='text'
          value={State}
          onChange={(event) => setState(event.target.value)}
          placeholder="State"
        ></input>
        <input
          type='text'
          value={Zip}
          onChange={(event) => setZip(event.target.value)}
          placeholder="Zip"
        ></input>
        <label>
          <input type='submit' value='submit'></input>
        </label>
        <label>
         <DeleteUser StockUserId={stockUserId}/>
        </label>
      </form>
    </div>
  );
};

export default Settings;
