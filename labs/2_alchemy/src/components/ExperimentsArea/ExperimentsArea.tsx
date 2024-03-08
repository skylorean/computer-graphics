import { FC, useContext, useEffect, useRef, useState } from "react";
import styles from "./ExperimentsArea.module.scss";
import ElementsContext from "../../context/ElementsContext";
import { IElement } from "../../shared/ElementType";
import Element from "../Element/Element";

interface ExperimentsAreaProps {
  draggedElement: IElement | null;
}

interface IExperimentItem extends IElement {
  id: string;
}

const ExperimentsArea: FC<ExperimentsAreaProps> = ({ draggedElement }) => {
  const { elements, setElements } = useContext(ElementsContext)!;
  const [experimentElements, setExperimentElements] = useState<
    IExperimentItem[]
  >([]);

  const unlockElements = () => {
    setElements(elements.map((item) => ({ ...item, unlocked: true })));
  };

  function handleDragOver(e) {
    e.preventDefault(); // Предотвращаем стандартное поведение браузера
    console.log("handleDragOverhandleDragOver", e);
  }

  function handleDrop(e) {
    if (!draggedElement) {
      return;
    }
    e.preventDefault();
    console.log("handleDrop", e);

    // Обработка события onDrop
    console.log("ExperimentArea draggedElement", draggedElement);
    setExperimentElements([
      ...experimentElements,
      { ...draggedElement, id: new Date().toString() },
    ]);
  }

  function handleDrag(e) {
    e.preventDefault();
    console.log("handleDrag", e);

    // Обработка события onDrop
  }

  // function handleDrop(e) {
  //   e.preventDefault();
  //   console.log("handleDrop", e);

  //   // Обработка события onDrop
  // }

  const containerRef = useRef<HTMLDivElement>(null);
  const boxRef = useRef<HTMLDivElement>(null);

  const isClicked = useRef<boolean>(false);

  const coords = useRef<{
    startX: number;
    startY: number;
    lastX: number;
    lastY: number;
  }>({
    startX: 0,
    startY: 0,
    lastX: 0,
    lastY: 0,
  });

  return (
    <div
      id="experiments"
      onDrag={(e) => handleDrag(e)}
      onDragOver={(e) => handleDragOver(e)}
      onDrop={(e) => handleDrop(e)}
      className={styles["experiments-area"]}
      ref={containerRef}
    >
      <div className={styles["elements-list"]}>
        {experimentElements.map(
          // (item) => item.unlocked && <Element element={item} key={item.type} />
          (item) => (
            <Element element={item} key={item.id} />
            // <div className={styles.box} ref={boxRef} key={item.id}>
            //   {item.type.toString()}
            // </div>
          )
        )}
      </div>
      {/* <div ref={boxRef} className={styles.box}></div> */}
      <button onClick={unlockElements}>click</button>
    </div>
  );
};

export default ExperimentsArea;
