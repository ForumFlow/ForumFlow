import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import Cookies from "js-cookie";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  let navigate = useNavigate();

  const handleLoginAccount = async () => {
    const url = "http://localhost:5152/user/login";
    if (username === "" || password === "") {
      alert("Please fill in all fields");
      return;
    }
    const data = {
      username: username,
      password: password,
    };

    await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data), // Correctly stringify the JSON object
    })
      .then((response) => {
        console.log(response);
        if (response.ok) {
          console.log("Success");
          alert("Account created successfully");
          // Cookies.set("jwt", response.headers.get("Authorization"));
          console.log(response.headers.get("Authorization"));
          response.text().then((text) => {
            Cookies.set("jwt", text);
            console.log(text);
          });

          setTimeout(() => {
            // window.location.href = window.location.origin + "/user/home";
            navigate("/user/home", { replace: true });
          }, 1000);
        }

        if (!response.ok) {
          // When the server responds with a status outside the range 200-299,
          // we get the response text and handle it in another .then()
          return response.text().then((text) => {
            if (text === "User already exists") {
              alert("Username already exists");
            } else {
              console.log("Failed:", text);
            }
            throw new Error(text); // Optional: re-throw to handle it in a .catch() if needed
          });
        }
        // If the response is OK, we handle the token or other success response
      })
      .catch((error) => {
        console.error("Error:", error);
      });

    // if (!response.ok) {
    //   if (response.text() === "User already exists") {
    //     alert("Username already exists");
    //   }
    // }

    // const result = await response.json();
    // console.log("Success:", result);
  };

  return (
    /*
  This example requires some changes to your config:
  
  ```
  // tailwind.config.js
  module.exports = {
    // ...
    plugins: [
      // ...
      require('@tailwindcss/forms'),
    ],
  }
  ```
*/
    <>
      {/*
        This example requires updating your template:

        ```
        <html class="h-full bg-white">
        <body class="h-full">
        ```
      */}
      <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <img
            alt="Your Company"
            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
            className="mx-auto h-10 w-auto"
          />
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Sign in to your account
          </h2>
        </div>

        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <div className="space-y-6">
            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                username
              </label>
              <div className="mt-2">
                <input
                  id="email"
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  name="email"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <div className="flex items-center justify-between">
                <label
                  htmlFor="password"
                  className="block text-sm font-medium leading-6 text-gray-900"
                >
                  Password
                </label>
                <div className="text-sm">
                  <a
                    href="#"
                    className="font-semibold text-indigo-600 hover:text-indigo-500"
                  >
                    Forgot password?
                  </a>
                </div>
              </div>
              <div className="mt-2">
                <input
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  id="password"
                  name="password"
                  type="password"
                  required
                  autoComplete="current-password"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <button
                type="submit"
                onClick={handleLoginAccount}
                className=" w-full
                flex
                justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Sign in
              </button>
            </div>
          </div>

          <p className="mt-10 text-center text-sm text-gray-500">
            Not a member?{" "}
            <a
              // href="user/create"
              onClick={(e) => {
                e.preventDefault();
                navigate("/user/create", { replace: true });
              }}
              className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500 hover:cursor-pointer"
            >
              Register now
            </a>
          </p>
        </div>
      </div>
    </>
  );
}
