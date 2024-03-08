import { AiOutlineClear } from "react-icons/ai";
import { BiSave, BiPencil, BiEraser } from "react-icons/bi";
import { BsBorderWidth } from "react-icons/bs";
import LineWidthModal from "./modals/LineWidthModal";
import AccessibilityButton from "./AccessibilityButton";
import ColorModal from "./modals/ColorModal";
import { useContext } from "react";
import { BrushContext } from "../context/BrushContext";

type AccessibilityBarProps = {
  ctx: CanvasRenderingContext2D | undefined | null;
};

function AccessibilityBar({ ctx }: AccessibilityBarProps) {
  const brushContext = useContext(BrushContext);
  const brush = brushContext.brush;

  const setBrushType = (type: string): void => {
    if (type) {
      brushContext.setBrush({
        width: brush.width,
        type: type,
        color: brush.color,
      });
    }
  };

  const saveImage = (): void => {
    const canvasElement = document.getElementById("canvasElement");
    const a = document.createElement("a");
    a.href = (canvasElement as HTMLCanvasElement).toDataURL("image/png");
    a.download = "image.png";
    a.click();
  };

  return (
    <div className="w-full py-2 md:py-0 h-32 md:h-12 bg-neutral-100 border-b border-neutral-300 px-5 flex flex-col md:flex-row justify-between items-center text-neutral-600 z-50">
      <div className="flex h-full gap-3 justify-center items-center">
        {/* Save image */}
        <AccessibilityButton
          onBtnClick={saveImage}
          icon={<BiSave className="icon-accessibility"></BiSave>}
        ></AccessibilityButton>

        {/* Clear canvas */}
        <AccessibilityButton
          onBtnClick={() => {
            if (ctx) {
              ctx.clearRect(0, 0, ctx.canvas.width, ctx.canvas.height);
              ctx.fillStyle = "white";
              ctx.fillRect(0, 0, ctx.canvas.width, ctx.canvas.height);
            }
          }}
          icon={
            <AiOutlineClear className="icon-accessibility"></AiOutlineClear>
          }
        ></AccessibilityButton>
      </div>

      {/* Tools */}
      <div className="flex h-full gap-3 justify-center items-center order-3 md:order-none">
        {/* Pen */}
        <AccessibilityButton
          onBtnClick={() => setBrushType("PENCIL")}
          activeProp={brush.type === "PENCIL"}
          icon={<BiPencil className="icon-accessibility"></BiPencil>}
        ></AccessibilityButton>

        {/* Eraser */}
        <AccessibilityButton
          onBtnClick={() => setBrushType("ERASER")}
          activeProp={brush.type === "ERASER"}
          icon={<BiEraser className="icon-accessibility"></BiEraser>}
        ></AccessibilityButton>

        {/* Line width */}
        <AccessibilityButton
          icon={<BsBorderWidth className="icon-accessibility"></BsBorderWidth>}
          modal={<LineWidthModal></LineWidthModal>}
        ></AccessibilityButton>

        {/* Color */}
        <AccessibilityButton
          icon={
            <div
              className="h-6 w-6 border border-neutral-300 rounded-lg cursor-pointer"
              style={{ backgroundColor: brush.color }}
            ></div>
          }
          modal={<ColorModal></ColorModal>}
        ></AccessibilityButton>
      </div>
    </div>
  );
}

export default AccessibilityBar;
