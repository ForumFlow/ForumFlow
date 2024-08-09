import React, { useState, useEffect } from "react";
import "../land.css";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";

function LandingPage() {
  const [showRegistration, setShowRegistration] = useState(false);
  const [showLogin, setShowLogin] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    // TODO - Uncomment this code when the login page is ready

    const jwt = Cookies.get("jwt");
    async function verifyUser() {
      const url = "http://localhost:5152/user/verify";
      await fetch(url, {
        method: "GET",
      })
        .then((response) => {
          if (response.ok) {
            navigate("/user/home", { replace: true });
          } else {
            Cookies.remove("jwt");
            console.log("User not verified");
          }
        })
        .catch((error) => {
          console.error("Error:", error);
        });
    }

    if (jwt) {
      verifyUser();
    }
  }, []);

  const handleRegisterClick = (e) => {
    e.preventDefault();
    setShowRegistration(true);
  };
  const handleLoginClick = (e) => {
    e.preventDefault();
    setShowLogin(true);
  };
  if (showLogin) {
    return <Login />;
  }

  if (showRegistration) {
    return <RegistrationPage />;
  }

  return (
    <>
      <header>
        <nav>
          <div className="logo">ForumFlow</div>
          <ul>
            <li>
              <a href="#home">Home</a>
            </li>
            <li>
              <a href="#about">About</a>
            </li>
            <li>
              <a href="/user/create">Register</a>
            </li>
            <li>
              <a href="/user/login">Login</a>
            </li>
          </ul>
        </nav>
      </header>

      <main>
        <section id="hero">
          <div className="hero-content">
            <h1 className="animate-text">Welcome to ForumFlow</h1>
            <p className="animate-text">Presentation feedback, done better</p>
            <a href="#signup" className="cta-button">
              Get Started
            </a>
          </div>
        </section>

        <section id="about">
          <h2>About Us</h2>
          <div className="about-content">
            <div className="about-list">
              <h3>Product</h3>
              <ul>
                <li>
                  <a href="#">Website Templates</a>
                </li>
                {/* Add more product list items here */}
              </ul>
            </div>
            {/* Add more about sections here */}
          </div>
        </section>

        <section id="signup">
          <div className="signup-container">
            <h2>Sign Up for Updates</h2>
            <p>Subscribe to our site for the latest news and updates.</p>
            <form id="signup-form">
              <input type="email" placeholder="Your Email Address" required />
              <button type="submit">Subscribe</button>
            </form>
          </div>
        </section>
      </main>

      <footer>
        <p>&copy; 2024 MyBrand. All rights reserved.</p>
      </footer>
    </>
  );
}

export default LandingPage;
