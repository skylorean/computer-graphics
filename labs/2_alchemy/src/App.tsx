import { useState } from "react";
import DiscoveredElementsArea from "./components/DiscoveredElementsArea/DiscoveredElementsArea";
import ExperimentsArea from "./components/ExperimentsArea/ExperimentsArea";
import { IElement } from "./shared/ElementType";

function App() {
  const [draggedElement, setDraggedElement] = useState<IElement | null>(null); // Стейт для хранения информации о перемещаемом элементе

  console.log("App.tsx draggedElement:", draggedElement);

  return (
    <div className="container">
      <div className="board">
        <DiscoveredElementsArea setDraggedElement={setDraggedElement} />
        <ExperimentsArea draggedElement={draggedElement} />
      </div>
    </div>
  );
}

export default App;
