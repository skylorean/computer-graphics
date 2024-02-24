import { FC } from "react";
import styles from "./Element.module.scss";
import ElementType from "../../shared/ElementType";

interface ElementProps {
  type: ElementType;
}

const Element: FC<ElementProps> = ({ type }) => {
  return <div>{type}</div>;
};

export default Element;
