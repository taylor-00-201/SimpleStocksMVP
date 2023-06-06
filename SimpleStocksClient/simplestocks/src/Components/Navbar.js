import { Link, NavLink } from "react-router-dom";
import React from "react";

export const Navbar = () => {
  return(
  <nav>
    <ul>
      <li>
        <Link to="/logout">Logout</Link>
      </li>
      <li>
        <Link to="/settings">Settings</Link>
      </li>
      <li>
        <Link to='/home'>Home</Link>
      </li>
    </ul>
  </nav>)
};
