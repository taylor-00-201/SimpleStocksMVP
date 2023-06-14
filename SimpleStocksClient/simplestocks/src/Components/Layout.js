//import { Outlet, Link } from "react-router-dom";

const Layout = (props) => {
  const deleteLocalStorage = () => {
    try {
      localStorage.removeItem("StockUser");
    } catch (error) {
      console.log(error);
    }
  };

  const logout = () => {
    props.SetIsLoggedIn(false);
    deleteLocalStorage();
    props.SetReRender(true);
  };

  return (
    <nav>
      <ul>
        {props.IsLoggedIn === true && (
          <>
            <li>
              <button onClick={() => logout()}>Logout</button>
            </li>
            <li>
              <button onClick={() => props.SetDisplay("Settings")}>
                Settings
              </button>
            </li>
          </>
        )}
        <li>
          <button onClick={() => props.SetDisplay("BuyStock")}>Home</button>
        </li>
      </ul>
    </nav>
  );
};

export default Layout;
