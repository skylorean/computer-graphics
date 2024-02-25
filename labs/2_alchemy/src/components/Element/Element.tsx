import { FC, useState } from "react";
import styles from "./Element.module.scss";
import { IElement } from "../../shared/ElementType";
import classnames from "classnames";

interface ElementProps {
  element: IElement;
}

const Element: FC<ElementProps> = ({ element }) => {
  const [isDragged, setIsDragged] = useState<boolean>(false);
  const { unlocked, type } = element;

  const handleDragStart = (e: React.DragEvent<HTMLDivElement>) => {
    console.log("handleDragStart", e);
    setIsDragged(true);
  };
  const handleDragEnd = (e: React.DragEvent<HTMLDivElement>) => {
    console.log("handleDragEnd", e);
    setIsDragged(false);
  };
  const handleDragOver = (e: React.DragEvent<HTMLDivElement>) => {
    // console.log("handleDragOver", e);
  };
  const handleDrag = (e: React.DragEvent<HTMLDivElement>) => {
    console.log("handleDrag", e);
  };

  console.log(isDragged);

  return (
    <div
      draggable={unlocked}
      className={classnames(styles.element, { [styles.draggable]: isDragged })}
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
