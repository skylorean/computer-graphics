import { useState } from "react";
import AccessibilityBar from "./components/AccessibilityBar";
import DrawingBoard from "./components/DrawingBoard";
import { BrushContext } from "./context/BrushContext";
import { IBrush } from "./shared/types";

function App() {
  const [ctx, setCtx] = useState<CanvasRenderingContext2D | undefined | null>(
    null
  );
  const [brush, setBrush] = useState<IBrush>({
    type: "PENCIL",
    width: 3,
    color: "#000000",
  });

  return (
    <BrushContext.Provider value={{ brush, setBrush }}>
      <div className="w-screen h-screen bg-neutral-200 flex flex-col">
        {/* Menu, save */}
        <AccessibilityBar ctx={ctx}></AccessibilityBar>

        {/* Canvas */}
        <div className="w-full h-full overflow-auto relative">
          <DrawingBoard ctx={ctx} setCtx={setCtx}></DrawingBoard>
        </div>
      </div>
    </BrushContext.Provider>
  );
}

export default App;
