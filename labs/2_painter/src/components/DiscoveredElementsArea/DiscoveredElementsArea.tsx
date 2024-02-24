import { FC } from "react";
import styles from "./DiscoveredElementsArea.module.scss";
import ElementType from "../../shared/ElementType";
import Element from "../Element/Element";

interface DiscoveredElementsAreaProps {}

const DiscoveredElementsArea: FC<DiscoveredElementsAreaProps> = () => {
  const allElementTypes: ElementType[] = Object.values(ElementType);

  console.log("allElementTypes", allElementTypes);

  return (
    <div>
      {allElementTypes.map((item) => (
        <Element type={item} key={item} />
      ))}

      <button>Сортировать</button>
    </div>
  );
};

export default DiscoveredElementsArea;
