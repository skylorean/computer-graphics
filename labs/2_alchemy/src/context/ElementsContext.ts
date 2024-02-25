import { createContext } from "react";
import { IElement } from "../shared/ElementType";

export interface IElementsContext {
  elements: IElement[];
  setElements: (elements: IElement[]) => void;
}

const ElementsContext = createContext<IElementsContext | null>(null);

export default ElementsContext;
