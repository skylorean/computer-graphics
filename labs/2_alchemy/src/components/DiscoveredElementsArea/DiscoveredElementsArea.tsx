import { FC, useContext } from "react";
import styles from "./DiscoveredElementsArea.module.scss";
import Element from "../Element/Element";
import ElementsContext from "../../context/ElementsContext";
import { IElement } from "../../shared/ElementType";

interface DiscoveredElementsAreaProps {
  setDraggedElement: (element: IElement | null) => void;
}

const DiscoveredElementsArea: FC<DiscoveredElementsAreaProps> = ({
  setDraggedElement,
}) => {
  const { elements } = useContext(ElementsContext)!;

  return (
    <div className={styles["discovered-elements-area"]}>
      <div className={styles["elements-list"]}>
        {elements.map(
          // (item) => item.unlocked && <Element element={item} key={item.type} />
          (item) => (
            <Element
              element={item}
              key={item.type}
              setDraggedElement={setDraggedElement}
            />
          )
        )}
      </div>

      <button className={styles["button"]}>Сортировать</button>
    </div>
  );
};

export default DiscoveredElementsArea;
