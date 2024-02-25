import { FC, useContext } from "react";
import styles from "./ExperimentsArea.module.scss";
import ElementsContext from "../../context/ElementsContext";

interface ExperimentsAreaProps {}

const ExperimentsArea: FC<ExperimentsAreaProps> = () => {
  const { elements, setElements } = useContext(ElementsContext)!;

  const unlockElements = () => {
    setElements(elements.map((item) => ({ ...item, unlocked: true })));
  };

  function handleDragOver(e) {
    e.preventDefault(); // Предотвращаем стандартное поведение браузера
    console.log("handleDragOverhandleDragOver", e);
  }

  function handleDrop(e) {
    e.preventDefault();
    console.log("handleDrophandleDrop", e);

    // Обработка события onDrop
  }

  return (
    <div
      onDragOver={(e) => handleDragOver(e)}
      onDrop={(e) => handleDrop(e)}
      className={styles["experiments-area"]}
    >
      <button onClick={unlockElements}>click</button>
    </div>
  );
};

export default ExperimentsArea;
