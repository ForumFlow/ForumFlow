import React from "react";
import PropTypes from "prop-types";

const PresentationCard = ({ title, desc }) => {
  return (
        <div className="grid col-span-4 relative min-h-40 cursor-pointer min-h-60">
          <div
            className="group shadow-lg hover:shadow-2xl duration-200 delay-75 w-full bg-white rounded-sm py-6 pr-6 pl-9"
            href=""
          >
            <p className="text-2xl font-bold text-gray-500 group-hover:text-gray-700">
              {" "}
              {title}{" "}
            </p>

            <p className="text-sm font-semibold text-gray-500 group-hover:text-gray-700 mt-2 leading-6">
              {" "}
              {desc}{" "}
            </p>

            <div className="bg-blue-400 group-hover:bg-blue-600 h-full w-4 absolute top-0 left-0">
              {" "}
            </div>
          </div>
        </div>
  );
};

export default PresentationCard;

PresentationCard.propTypes = {
  title: PropTypes.string.isRequired,
  desc: PropTypes.string.isRequired,
};
