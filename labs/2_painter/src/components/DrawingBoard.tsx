import { useContext, useEffect, useRef, useState } from "react";
import { BrushContext } from "../context/BrushContext";
import { CanvasPoint } from "../shared/types";

type DrawingBoardProps = {
  ctx: CanvasRenderingContext2D | undefined | null;
  setCtx: (ctx: CanvasRenderingContext2D | undefined | null) => void;
};

function DrawingBoard({ ctx, setCtx }: DrawingBoardProps) {
  const [points, setPoints] = useState(Array<CanvasPoint>());

  const brushContext = useContext(BrushContext);
  const brush = brushContext.brush;

  const [painting, setPainting] = useState({
    isPainting: false,
    startX: 0,
    startY: 0,
  });

  const canvasElement = useRef<HTMLCanvasElement>(null);

  useEffect(() => {
    setCtx(canvasElement.current?.getContext("2d"));
    if (canvasElement.current) {
      canvasElement.current.width = canvasElement.current.clientWidth;
      canvasElement.current.height = canvasElement.current.clientHeight;
      (canvasElement.current.getContext("2d") as any).fillStyle = "white";
      canvasElement.current
        .getContext("2d")
        ?.fillRect(
          0,
          0,
          canvasElement.current.clientWidth,
          canvasElement.current.clientHeight
        );
    }
  }, [canvasElement]);

  const handleMouseDown = (
    e: React.MouseEvent<HTMLCanvasElement, MouseEvent>
  ) => {
    e.preventDefault();

    if (ctx && canvasElement.current) {
      const rect = (e.target as HTMLElement).getBoundingClientRect();
      const x = e.clientX - rect.left;
      const y = e.clientY - rect.top;

      setPainting({
        isPainting: true,
        startX: x,
        startY: y,
      });
      setPoints([]);
    }
  };

  const handleMouseUp = (
    e: React.MouseEvent<HTMLCanvasElement, MouseEvent>
  ) => {
    handleDraw(e);

    const rect = (e.target as HTMLElement).getBoundingClientRect();
    // const x = e.clientX - rect.left;
    // const y = e.clientY - rect.top;

    setPainting({
      isPainting: false,
      startX: 0,
      startY: 0,
    });

    setPoints([]);
    if (ctx) {
      ctx.stroke();
      ctx.beginPath();
    }
  };

  const handleDraw = (e: React.MouseEvent<HTMLCanvasElement, MouseEvent>) => {
    if (!painting.isPainting) return;

    const rect = (e.target as HTMLElement).getBoundingClientRect();
    const x = e.clientX - rect.left;
    const y = e.clientY - rect.top;

    if (ctx && canvasElement.current) {
      setPoints([...points, { x: x, y: y }]);

      ctx.lineWidth = brush.width;
      if (brush.type === "PENCIL") {
        ctx.strokeStyle = brush.color;
      } else if (brush.type === "ERASER") {
        ctx.strokeStyle = "white";
      }
      ctx.lineCap = "round";
      ctx.lineJoin = "round";
      ctx.lineTo(x, y);
      ctx.stroke();
    }
  };

  return (
    <canvas
      ref={canvasElement}
      onMouseDown={(e) => handleMouseDown(e)}
      onMouseUp={(e) => handleMouseUp(e)}
      onMouseMove={(e) => handleDraw(e)}
      onMouseLeave={(e) => {
        handleDraw(e);
        setPainting({
          isPainting: false,
          startX: 0,
          startY: 0,
        });
      }}
      id="canvasElement"
      className="absolute cursor-crosshair origin-top-left"
      style={{
        width: "800px",
        height: "600px",
      }}
    ></canvas>
  );
}

export default DrawingBoard;
