/**
 * Dropdown navigation item component.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {string} props.menuLabel - The label for the menu.
 * @param {Array} props.options - The options for the dropdown menu.
 * @param {string} [props.theme="primary"] - The theme of the dropdown menu. Possible values are "primary" and "banner".
 * @param {string} [props.position="right"] - The position of the dropdown menu. Possible values are "left" and "right".
 * @returns {JSX.Element} The rendered dropdown navigation item.
 */
import React, { FC } from "react";
import classNames from "classnames";

// export interface NavItemOptions {
//   itemLabel: string;
//   onClick?: () => void;
//   href?: string;
// }

// export interface NavItemProps {
//   menuLabel: string;
//   options: NavItemOptions[];
//   theme?: "primary" | "banner";
//   position?: "left" | "right"; // to prvent overflow the screen
// }

 const DropdownHover = ({
  menuLabel,
  options,
  theme = "primary",
  position = "right",
}) => {
  const menuLabelClasses = classNames(
    "inline-flex h-10 w-max items-center justify-center text-sm font-medium md:text-lg",
    {
      "text-white": theme === "banner",
    }
  );
  const svgClasses = classNames(
    "fill-current h-4 w-4 transform group-hover:-rotate-180 transition duration-500 ease-in-out",
    {
      "text-white": theme === "banner",
    }
  );

  const dropdownPositionClass = position === "right" ? "right-0" : "left-0";

  return (
    <div className="relative group rounded-md cursor-pointer hover:text-primary-text hover:bg-primary">
      <div className="flex justify-center items-center px-4">
        <button className={menuLabelClasses}>{menuLabel}</button>
        <span className="ml-2">
          <svg
            className={svgClasses}
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 20 20"
          >
            <path d="M9.293 12.95l.707.707L15.657 8l-1.414-1.414L10 10.828 5.757 6.586 4.343 8z" />
          </svg>
        </span>
      </div>
      <div
        className={`absolute top-0 ${dropdownPositionClass} transition translate-y-0
                 z-50 min-w-[200px] transform duration-500 ease-in-out
                 invisible group-hover:visible group-hover:translate-y-5`}
      >
        <div className="relative top-6 bg-light-secondary dark:bg-dark-secondary rounded-md shadow-xl w-full">
          <div className="relative z-10">
            <ul className="p-1 overflow-hidden">
              {options.map((option, i) => (
                <div key={i}>
                  {option.href ? (
                    <a
                      key={option.itemLabel}
                      href={option.href}
                      className="block"
                    >
                      <li className="inline-flex h-10 w-full items-center justify-start rounded-md px-4 py-2 text-sm font-medium bg-light-secondary dark:bg-dark-secondary text-light-primary-text dark:text-dark-primary-text hover:bg-light-secondary-hover dark:hover:bg-dark-secondary-hover md:text-lg">
                        {option.itemLabel}
                      </li>
                    </a>
                  ) : (
                    <li
                      key={option.itemLabel}
                      className="inline-flex h-10 w-full items-center justify-start rounded-md px-4 py-2 text-sm font-medium bg-light-secondary dark:bg-dark-secondary text-light-primary-text dark:text-dark-primary-text hover:bg-light-secondary-hover dark:hover:bg-dark-secondary-hover md:text-lg"
                      onClick={option.onClick}
                    >
                      {option.itemLabel}
                    </li>
                  )}
                </div>
              ))}
            </ul>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DropdownHover;