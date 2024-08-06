import React from "react";

export default function Login() {
  return (
    <div>
      <h1>Log In</h1>
      <form>
        <div>
          <label>Username:</label>
          <input type="text" />
        </div>
        <div>
          <label>Password:</label>
          <input type="password" />
        </div>
        <button type="submit">Log In</button>
      </form>
    </div>
  );
}   