import { FC, useContext } from "react";
import styles from "./DiscoveredElementsArea.module.scss";
import Element from "../Element/Element";
import ElementsContext from "../../context/ElementsContext";

interface DiscoveredElementsAreaProps {}

const DiscoveredElementsArea: FC<DiscoveredElementsAreaProps> = () => {
  const { elements, setElements } = useContext(ElementsContext)!;

  return (
    <div className={styles["discovered-elements-area"]}>
      <div className={styles["elements-list"]}>
        {elements.map(
          // (item) => item.unlocked && <Element element={item} key={item.type} />
          (item) => (
            <Element element={item} key={item.type} />
          )
        )}
      </div>

      <button className={styles["button"]}>Сортировать</button>
    </div>
  );
};

export default DiscoveredElementsArea;
