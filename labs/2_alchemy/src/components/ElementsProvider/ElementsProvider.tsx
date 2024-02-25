import { FC, ReactNode, useState } from "react";
import { ElementType, IElement } from "../../shared/ElementType";
import ElementsContext from "../../context/ElementsContext";

interface ElementsProviderProps {
  children: ReactNode;
}

const DEFAULT_UNLOCKED_ELEMENTS = [ElementType.FIRE, ElementType.WATER];

const DEFAULT_ELEMENTS: IElement[] = Object.values(ElementType).map((item) => {
  const element: IElement = { unlocked: false, type: item };

  if (DEFAULT_UNLOCKED_ELEMENTS.includes(element.type)) {
    element.unlocked = true;
  }

  return element;
});

const ElementsProvider: FC<ElementsProviderProps> = ({ children }) => {
  const [elements, setElements] = useState<IElement[]>(DEFAULT_ELEMENTS);

  return (
    <ElementsContext.Provider value={{ elements, setElements }}>
      {children}
    </ElementsContext.Provider>
  );
};

export default ElementsProvider;
