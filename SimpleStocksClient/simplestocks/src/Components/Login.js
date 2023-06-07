import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export const Login = () => {
  const [errors, setErrors] = useState("");
  const [userData, setUserData] = useState([]);
  const [userEmail, setUserEmail] = useState("");
  const [passwordHash, setPasswordHash] = useState("");
  const [userType, setUserType] = useState({
    firstName: "",
    lastName: "",
    email: "",
    isStaff: false,
  });

  const navigate = useNavigate();

  const navigateDashboard = () => navigate("/home");

  const setLocalStorage = (data) => {
    console.log(data);
    const transitoryobject = {
      firstName: data.firstName,
      lastName: data.lastName,
      Email: data.email,
      IsAdmin: data.IsAdmin,
    };

    console.log(transitoryobject);

    try {
      if (transitoryobject.IsAdmin === true) {
        let StockUser = "StockUser";

        localStorage.setItem(
          StockUser,
          JSON.stringify({
            FirstName: transitoryobject.FirstName,
            LastName: transitoryobject.LastName,
            Email: transitoryobject.Email,
            UserType: transitoryobject.IsAdmin,
          })
        );
      } else {
        new Error(
          console.log(`There was an error loggin in error message: ${Error}`)
        );
      }
    } catch (error) {
      console.log(error);
    }
  };

  const fetchData = async () => {
    try {
      const response = await fetch(
        `https://localhost:7043/api/Login/LoginUser`,
        {
          body: JSON.stringify({
            Email: userEmail,
            Password: passwordHash,
          }),
          credentials: "include",
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      const data = await response.json();
      console.log(data);
      console.log(data["user"]);
      setLocalStorage(data["user"]);
      setUserType(data);

      if (
        response.ok &&
        response.headers.get("Content-Type").includes("application/json")
      ) {
        console.log(data);
        console.log(response);
      } else {
        console.log("not valid JSON");
      }

      if (
        response.status === 200 ||
        response.status === 201 ||
        response.status === 202
      ) {
        setUserData(data);
        navigateDashboard();
      } else {
        throw new Error(
          "Database call failed, data could not be retrieved and the user could not be logged in."
        );
      }
    } catch (error) {
      console.log(error);
      setErrors(error);
    }
  };

  const submissionHandler = (event) => {
    event.preventDefault();
    fetchData();
  };

  return (
    <>
      <form onSubmit={submissionHandler}>
        <label>
          Email:
          <input
            type='text'
            value={userEmail}
            onChange={(event) => setUserEmail(event.target.value)}
          ></input>
        </label>
        Password:
        <input
          type='text'
          value={passwordHash}
          onChange={(event) => setPasswordHash(event.target.value)}
        ></input>
        <label>
          <input type='submit' value='submit'></input>
        </label>
      </form>
    </>
  );
};
