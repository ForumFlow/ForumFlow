import React from "react";
import { useState } from "react";
import "./navBar.css";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";
import { SignOutIcon } from "@primer/octicons-react";

export const NavBar = ({ user }) => {
  const [showLogout, setShowLogout] = useState(false);
  const navigate = useNavigate();
  const toogleLogoutUI = () => {
    setShowLogout(!showLogout);
  };
  const logout = () => {
    Cookies.remove("jwt");
    navigate("/", { replace: true });
  };

  return (
    <header>
      <nav className="navUiComponent">
        <div className="logo">ForumFlow</div>
        <ul>
          <li>
            <a href="/">Home</a>
          </li>
          <li>
            <a href="/#about">About</a>
          </li>
          <li>
            <a href="/user/create">Register</a>
          </li>
          <li onClick={toogleLogoutUI} className="relative cursor-pointer">
            {showLogout && (
              <>
                {/* creates a div that will cover the whole screen so when clicked it will close the logout menu */}
                <div className="fixed top-0 left-0 w-full h-full z-20"></div>

                <div
                  onClick={logout}
                  className="absolute left-0 z-30 mt-8 text-black rounded-md bg-white shadow-lg ring-1 ring-black 
                  ring-opacity-5 py-3.5 text-nowrap flex justify-start items-center w-fit pr-1"
                >
                  <SignOutIcon className="w-fit mr-3" size={24} />
                  Logout
                </div>
              </>
            )}
            {user ? <span>{user}</span> : <a href="/user/login">Login</a>}
          </li>
        </ul>
      </nav>
    </header>
  );
};
