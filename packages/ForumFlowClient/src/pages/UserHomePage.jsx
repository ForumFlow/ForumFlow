import React from "react";
import { Link } from "react-router-dom";
import PresentationCard from "../components/userHomepage/PresentationCard";

export default function UserHomePage() {

  return (
    <div>
      <p>Here the Presentations assigned for each user will be listed</p>
      <PresentationCard title="sample title" desc="sample description" />
      <Link to="../Forum">
        <PresentationCard title="Add a new Presentation" />
      </Link>
    </div>
  );
}
