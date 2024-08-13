import React from "react";
import { Link, redirect } from "react-router-dom";
import PresentationCard from "../components/userHomepage/PresentationCard";
import { NavBar } from "../components/navBar/NavBar";
import { PlusIcon } from "@primer/octicons-react";
import { useState, useEffect } from "react";
import Cookies from "js-cookie";
import { useNavigate } from "react-router-dom";

export default function UserHomePage() {
  const [currentUser, setCurrentUser] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const jwt = Cookies.get("jwt");
    console.log("jwt", jwt);
    async function verifyUser() {
      const url = "http://localhost:5152/user/verify";
      const basicAuth = `Bearer ${jwt}`;
      await fetch(url, {
        method: "GET",

        headers: {
          Authorization: basicAuth,
        },
      })
        .then((response) => {
          if (response.ok) {
            // let payload = jwt.split(".")[1];
            const payload = JSON.parse(atob(jwt.split(".")[1]));
            // alert("Welcome " + payload.sub.toString());
            setCurrentUser(payload.sub.toString());
          } else {
            Cookies.remove("jwt");
            alert("User not verified please login again");

            // let payload = jwt.split(".")[1];
            // console.log("payload", payload);
            // payload = atob(jwt.split(".")[1]);
            // console.log("payload", payload);
            // decode the payload from base64
            // navigate("/user/login", { replace: true });
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

  return (
    <div>
      <NavBar user={currentUser} />
      <section className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
        <div className="max-w-screen-lg text-gray-500 sm:text-lg dark:text-gray-400">
          <h2 className="mb-4 text-4xl tracking-tight font-bold text-gray-900 dark:text-white">
            Presentations
          </h2>

          <p className="mb-4 font-light">
            Add New a Presentation: Create your new Presentation Here or view
            old presentations. Share your ideas with the world with built in
            functionality to make custom forums and Faq sections{" "}
          </p>
          <div className="grid grid-cols-12 max-w-5xl gap-2">
            <Link
              className="grid col-span-4 flex justify-center items-center group bg-gray-50 border-4 rounded-lg hover:border-blue-500"
              to="../createPresentation"
            >
              <PlusIcon className="h-6 sm:h-10 md:h-14 lg:h-16 xl:h-20 group-hover:text-blue-500" />
            </Link>
            <PresentationCard title="sample title" desc="sample description" />
            <PresentationCard title="sample title" desc="sample description" />
          </div>
        </div>
      </section>
    </div>
  );
}
