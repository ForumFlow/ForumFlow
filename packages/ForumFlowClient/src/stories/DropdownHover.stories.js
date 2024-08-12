import { fn } from "@storybook/test";
import { DropdownHover } from "./DropdownHover";

export default {
  title: "DropdownHover",
  component: DropdownHover,
  parameters: {
    // Optional parameter to center the component in the Canvas. More info: https://storybook.js.org/docs/configure/story-layout
    layout: "centered",
  },
  // This component will have an automatically generated Autodocs entry: https://storybook.js.org/docs/writing-docs/autodocs
  tags: ["autodocs"],
  // More on argTypes: https://storybook.js.org/docs/api/argtypes
  argTypes: {
    buttonName: { control: "Create FAQ" },
    options: { label: "Add New FAQ", action: fn() },
  },
  // Use `fn` to spy on the onClick arg, which will appear in the actions panel once invoked: https://storybook.js.org/docs/essentials/actions#action-args
};
