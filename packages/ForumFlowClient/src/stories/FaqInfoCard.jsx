import React from "react";
import PropTypes from "prop-types";

export const FaqInfoCard = ({ header, setHeader, subtext, setSubtext, edit }) => {
  return (
    <a
      href="#"
      className="block max-w-3xl p-6 bg-white border border-gray-200 rounded-lg shadow hover:bg-gray-100 dark:bg-gray-800 dark:border-gray-700 dark:hover:bg-gray-700"
    >
      {edit ? (
        <input
          onChange={setHeader}
          value={header}
          className="mb-2 text-2xl font-bold tracking-tight border text-black"
          type="text"
        />
      ) : (
        <h5 className="mb-2 text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
          {header}
        </h5>
      )}

      {edit ? (
        <input
          onChange={setSubtext}
          value={subtext}
          className="font-normal text-gray-700 dark:text-gray-400"
          type="text"
        />
      ) : (
        <p className="font-normal text-gray-700 dark:text-gray-400">
          {subtext}
        </p>
      )}
    </a>
  );
};
