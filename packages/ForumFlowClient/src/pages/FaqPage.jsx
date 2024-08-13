import { useState } from "react";
import { DiffAddedIcon } from "@primer/octicons-react";
import { FaqCollapsible } from "../stories/FaqCollapsible";
import { PlusCircleIcon } from "@primer/octicons-react";
import { Menu, MenuButton, MenuItem, MenuItems } from "@headlessui/react";
import DropdownHover from "../stories/DropdownHover";
import { Button } from "../stories/Button";
import { QuestionIcon } from "@primer/octicons-react";
import { PencilIcon } from "@primer/octicons-react";
import { TrashIcon } from "@primer/octicons-react";
import { FaqInfoCard } from "../stories/FaqInfoCard";
import { CheckIcon } from "@primer/octicons-react";
import { NavBar } from "../components/navBar/NavBar";
export default function FaqPage() {
  // TODO make fetch for faq for a userID
  // ID
  // presentationId
  // question
  // answer
  // presentationId
  // order

  // display options
  // 0 -> collapsible
  // 1 -> general info

  const [faq, setFaq] = useState([
    {
      question: "What is the meaning of life?",
      answer: "42",
      edit: false,
      displayOption: 0,
    },
  ]);
  const [menuOpen, setMenuOpen] = useState(false);
  function addNewItem(displayOption) {
    setFaq([
      ...faq,
      {
        question: "fill",
        answer: "fill",
        edit: true,
        displayOption: displayOption,
      },
    ]);
  }
  function handleQuestionChange(index, e) {
    setFaq(
      faq.map((item, i) =>
        i === index ? { ...item, question: e.target.value } : item
      )
    );
  }
  function handleAnswerChange(index, e) {
    setFaq(
      faq.map((item, i) =>
        i === index ? { ...item, answer: e.target.value } : item
      )
    );
  }
  function handleEdit(index) {
    console.log("edit");
    setFaq(
      faq.map((item, i) => (i === index ? { ...item, edit: !item.edit } : item))
    );
  }

  // Rest of the component code...
  return (
    <>
      <NavBar />
      <section className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
        <div className="max-w-screen-lg text-gray-500 sm:text-lg dark:text-gray-400">
          <h2 className="mb-4 text-4xl tracking-tight font-bold text-gray-900 dark:text-white">
            General Information{" "}
          </h2>
          <p className="mb-4 font-light">
            Add New FAQ: Input questions and their corresponding answers through
            a simple form, which includes fields for the question, answer, and
            an option to categorize the FAQ under specific topics.
          </p>
          <p className="mb-4 font-medium"></p>
          <div className="max-w-4xl">
            {faq.map((faqItem, index) => (
              <div key={index} className="flex items-start gap-1 group pb-3">
                {faqItem.displayOption === 0 && (
                  <FaqCollapsible
                    question={faqItem.question}
                    answer={faqItem.answer}
                    edit={faqItem.edit}
                    setAnswer={(e) => handleAnswerChange(index, e)}
                    setQuestion={(e) => handleQuestionChange(index, e)}
                  />
                )}

                {faqItem.displayOption === 1 && (
                  <FaqInfoCard
                    setHeader={(e) => handleQuestionChange(index, e)}
                    header={faqItem.question}
                    subtext={faqItem.answer}
                    edit={faqItem.edit}
                    setSubtext={(e) => handleAnswerChange(index, e)}
                  />
                )}
                <div className="w-fit flex flex-col gap-1 items-start">
                  <button
                    onClick={() => {
                      handleEdit(index);
                    }}
                    className="w-fit bg-blue-500 hover:bg-blue-700 text-white font-bold py-1 px-2 rounded"
                  >
                    {faqItem.edit ? (
                      <CheckIcon size={20} />
                    ) : (
                      <PencilIcon size={20} />
                    )}
                  </button>
                  <button
                    onClick={() => {
                      setFaq(faq.filter((_, i) => i !== index));
                    }}
                    className="w-fit bg-red-500 hover:bg-red-700 text-white font-bold py-1 px-2 rounded"
                  >
                    <TrashIcon size={20} />
                  </button>
                </div>
              </div>
            ))}
          </div>

          <div className="flex justify-end mt-2 max-w-3xl">
            <button
              className="w-fit text-nowrap inline-flex justify-center gap-x-1.5 rounded-md bg-blue-700 
            hover:bg-blue-800 
            focus:outline-none focus:ring-4 focus:ring-blue-30 px-6 
            py-2 text-sm font-semibold text-white shadow-sm ring-1 ring-inset ring-gray-300 
          mr-1  
            "
            >
              Save Change
            </button>
            <Menu
              as="div"
              className="relative inline-block text-left w-fit "
              data-open={menuOpen}
            >
              <MenuButton
                onClick={() => {
                  console.log("hello");
                }}
                className="text-nowrap inline-flex w-full justify-center gap-x-1.5 rounded-md bg-white px-6 py-2 text-sm font-semibold text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 hover:bg-gray-50"
              >
                Add New Item
                {/* <ChevronDownIcon aria-hidden="true" className="-mr-1 h-5 w-5 text-gray-400" /> */}
              </MenuButton>

              <MenuItems
                transition
                className=" absolute right-0 z-10 mt-2 w-40 origin-top-right rounded-md bg-white shadow-lg ring-1 ring-black 
                  ring-opacity-5 transition focus:outline-none data-[closed]:scale-95 data-[closed]:transform data-[closed]:opacity-0 data-[enter]:duration-100
                   data-[leave]:duration-75 data-[enter]:ease-out data-[leave]:ease-in"
              >
                <MenuItem>
                  <button
                    onClick={() => {
                      addNewItem(0);
                    }}
                    className="flex justify-start items-center text-nowrap block px-2 py-2 text-sm text-gray-700 data-[focus]:bg-gray-100 data-[focus]:text-gray-900 cursor-pointer"
                  >
                    <div className="w-fit mr-2">
                      <QuestionIcon size={24} />
                    </div>
                    New FAQ
                  </button>
                </MenuItem>
                <MenuItem>
                  <button
                    onClick={() => {
                      addNewItem(1);
                    }}
                    className="flex justify-start items-center text-nowrap block px-2 py-2 text-sm text-gray-700 data-[focus]:bg-gray-100 data-[focus]:text-gray-900 cursor-pointer"
                  >
                    <div className="w-fit mr-2">
                      <QuestionIcon size={24} />
                    </div>
                    New Info Card
                  </button>
                </MenuItem>
              </MenuItems>
            </Menu>
          </div>
        </div>
      </section>
    </>
  );
}
