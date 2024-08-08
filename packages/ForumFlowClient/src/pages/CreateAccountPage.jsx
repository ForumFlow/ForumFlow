import React, { useState } from "react";

export default function CreateAccountPage() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirsName] = useState("");
  const [lastName, setLastName] = useState("");
  const handleCreateAccount = async () => {
    const url = "http://localhost:5152/user/createUser";
    if (
      username === "" ||
      password === "" ||
      firstName === "" ||
      lastName === ""
    ) {
      alert("Please fill in all fields");
      return;
    }
    const data = {
      username: username,
      password: password,
      firstName: firstName,
      lastName: lastName,
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
        document.cookie = `Authorization=${response.headers.get("Authorization")}`;

          response.body().then((text) => {
            console.log(text);
          
          });
          

          // setTimeout(() => {
          //   window.location.href = window.location.origin + "/user/home";
          // }, 1000);
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
    <>
      <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <img
            alt="Your Company"
            src="https://tailwindui.com/img/logos/mark.svg?color=indigo&shade=600"
            className="mx-auto h-10 w-auto"
          />
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Create your your account
          </h2>
        </div>

        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <div className="space-y-6">
            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                Username
              </label>
              <div className="mt-2">
                <input
                  value={username}
                  onChange={(e) => setUsername(e.target.value)}
                  id="email"
                  name="email"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                password
              </label>
              <div className="mt-2">
                <input
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  id="email"
                  name="email"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                first name
              </label>
              <div className="mt-2">
                <input
                  value={firstName}
                  onChange={(e) => setFirsName(e.target.value)}
                  id="email"
                  name="email"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <label
                htmlFor="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                last name
              </label>
              <div className="mt-2">
                <input
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                  id="email"
                  name="email"
                  className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div>
              <button
                onClick={handleCreateAccount}
                className="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Create your account
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
