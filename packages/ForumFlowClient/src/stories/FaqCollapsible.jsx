import React, { useEffect } from "react";
import PropTypes from "prop-types";
import { ChevronDownIcon } from "@primer/octicons-react";

export const FaqCollapsible = ({
  setQuestion,
  question,
  setAnswer,
  answer,
  edit,
}) => {
  // TODO logic for collapsible
  return (
    <div className="pl-3 py-5 border-3 bg-white border border-gray-200 rounded-lg shadow dark:bg-gray-800 dark:border-gray-700 max-w-3xl">
      <details className="group" open={true}>
        <summary className="flex justify-between items-center font-medium cursor-pointer list-none ">
          {edit ? (
            <input
              onChange={setQuestion}
              value={question}
              className="border text-black"
              type="text"
            />
          ) : (
            <span className="text-black">{question}</span>
          )}

          <span className="transition group-open:rotate-180 flex justify-end w-6">
            <ChevronDownIcon size={24} />
          </span>
        </summary>
        {edit ? (
          <input
            onChange={setAnswer}
            value={answer}
            className="border mr-5 mt-3"
            type="text"
          />
        ) : (
          <p className="text-neutral-600 mt-3 group-open:animate-fadeIn">
            {answer}
          </p>
        )}
      </details>
      <hr className="mt-3" />
    </div>
  );
};

FaqCollapsible.propTypes = {
  question: PropTypes.string.isRequired,
  answer: PropTypes.string.isRequired,
};

// FaqCollapsible.defaultProps = {
//   question: "What is the meaning of life?",
//   answer: "42",
// };
