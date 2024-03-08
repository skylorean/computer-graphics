import { FC, useState } from "react";
import styles from "./Element.module.scss";
import { IElement } from "../../shared/ElementType";
import classnames from "classnames";

interface ElementProps {
  element: IElement;
  setDraggedElement?: (element: IElement | null) => void;
}

const Element: FC<ElementProps> = ({ element, setDraggedElement }) => {
  const [isDragged, setIsDragged] = useState<boolean>(false);

  // const [droppedElement, setDroppedElement] = useState<IElement | null>(null); // Стейт для хранения информации о перемещаемом элементе
  const { unlocked, type } = element;

  const handleDragStart = (e: React.DragEvent<HTMLDivElement>) => {
    console.log("handleDragStart", e);
    setIsDragged(true);
    console.log("Current dragged item is");
    setDraggedElement?.(element);
  };
  const handleDragEnd = (e: React.DragEvent<HTMLDivElement>) => {
    console.log("handleDragEnd", e);
    setIsDragged(false);
    setDraggedElement?.(null);
  };
  const handleDragOver = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    // console.log("handleDragOver", e);
  };
  const handleDrag = (e: React.DragEvent<HTMLDivElement>) => {
    // console.log("handleDrag", e);
  };
  function handleDrop(e: React.DragEvent<HTMLDivElement>) {
    e.preventDefault();
    console.log("handleDrop", e);
  }

  console.log(
    `Current element is ${element.type.toString()}`,
    "isDragged",
    isDragged
  );

  return (
    <div
      draggable={unlocked}
      className={classnames(styles.element, { [styles.draggable]: isDragged })}
      onDrop={(e) => handleDrop(e)}
      onDragStart={(e) => handleDragStart(e)}
      onDragEnd={(e) => handleDragEnd(e)}
      onDragOver={(e) => handleDragOver(e)}
      onDrag={(e) => handleDrag(e)}
    >
      {unlocked ? (
        <div>
          {type}
          <div>{`${isDragged}`}</div>
        </div>
      ) : (
        "???"
      )}
    </div>
  );
};

export default Element;
