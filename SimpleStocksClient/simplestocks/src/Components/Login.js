import React from "react";
import { useState } from "react";
import { useEffect } from "react";
// import { useNavigate } from "react-router-dom";

export const Login = (props) => {
  // const [errors, setErrors] = useState("");
  //const [userData, setUserData] = useState([]);
  const [Email, setUserEmail] = useState("");
  const [PasswordHash, setPasswordHash] = useState("");
  // const [userType, setUserType] = useState({
  //   firstName: "",
  //   lastName: "",
  //   email: "",
  //   isStaff: false,
  //   id: "",
  // });

  // const navigate = useNavigate();

  // const navigateDashboard = () => navigate("/home");

  // const setLocalStorage = (data) => {
  //   console.log(data);
  //   const transitoryobject = {
  //     firstName: data.firstName,
  //     lastName: data.lastName,
  //     email: data.email,
  //     isAdmin: data.isAdmin,
  //     id: data.id,
  //   };

  //   console.log(transitoryobject);

  //   try {
  //     if (transitoryobject.isAdmin === false) {
  //       let StockUser = "StockUser";

  //       localStorage.setItem(
  //         StockUser,
  //         JSON.stringify({
  //           firstName: transitoryobject.firstName,
  //           lastName: transitoryobject.lastName,
  //           email: transitoryobject.email,
  //           userType: transitoryobject.isAdmin,
  //           id: transitoryobject.id,
  //         })
  //       );
  //     } else {
  //       new Error(
  //         console.log(`There was an error loggin in error message: ${Error}`)
  //       );
  //     }
  //   } catch (error) {
  //     console.log(error);
  //   }
  // };

  async function fetchData() {
    try {
      const response = await fetch(
        `https://localhost:7043/api/Login/LoginUser`,
        {
          body: JSON.stringify({
            Email: Email,
            PasswordHash: PasswordHash,
          }),
          credentials: "include",
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      //------------------
      // const data = await response.json();
      const data = await response.json();
      //----------------

      console.log(data);
      console.log(data.user);

      // setLocalStorage(data.user);
      // setUserType(data.user);
      // props.SetUser(data.user);

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
        ///-------------------------------------------
        if (data.user.isAdmin === false) {
          localStorage.setItem(
            "StockUser",
            JSON.stringify({
              firstName: data.user.firstName,
              lastName: data.user.lastName,
              email: data.user.email,
              userType: data.user.isAdmin,
              id: data.user.id,
            })
          );
        } else {
          new Error(
            console.log(`There was an error loggin in error message: ${Error}`)
          );
        }

        //------------------------------------------------------
        props.setIsLoggedIn(true);
        props.setUser(data.user);
        props.setDisplay("BuyStock");
        //navigateDashboard();
      } else {
        throw new Error(
          "Database call failed, data could not be retrieved and the user could not be logged in."
        );
      }
    } catch (error) {
      console.log(error);
    }
  }

  // const fetchData = async () => {
  //   try {
  //     const response = await fetch(
  //       `https://localhost:7043/api/Login/LoginUser`,
  //       {
  //         body: JSON.stringify({
  //           Email: Email,
  //           PasswordHash: PasswordHash,
  //         }),
  //         credentials: "include",
  //         method: "POST",
  //         headers: {
  //           "Content-Type": "application/json",
  //         },
  //       }
  //     );

  //     const data = await response.json();
  //     console.log(data);
  //     console.log(data.user);
  //     setLocalStorage(data.user);
  //     setUserType(data.user);
  //     props.SetUser(data.user);

  //     if (
  //       response.ok &&
  //       response.headers.get("Content-Type").includes("application/json")
  //     ) {
  //       console.log(data);
  //       console.log(response);
  //     } else {
  //       console.log("not valid JSON");
  //     }

  //     if (
  //       response.status === 200 ||
  //       response.status === 201 ||
  //       response.status === 202
  //     ) {
  //       props.setIsLoggedIn(true);
  //       props.setUser(data);
  //       props.setDisplay("BuyStock")
  //       //navigateDashboard();
  //     } else {
  //       throw new Error(
  //         "Database call failed, data could not be retrieved and the user could not be logged in."
  //       );
  //     }
  //   } catch (error) {
  //     console.log(error);
  //     setErrors(error);
  //   }
  // };

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
            value={Email}
            onChange={(event) => setUserEmail(event.target.value)}
          ></input>
        </label>
        Password:
        <input
          type='text'
          value={PasswordHash}
          onChange={(event) => setPasswordHash(event.target.value)}
        ></input>
        <label>
          <input type='submit' value='submit'></input>
        </label>
      </form>
    </>
  );
};
