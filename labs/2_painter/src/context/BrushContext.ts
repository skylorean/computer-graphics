import { createContext } from "react";
import { IBrush } from "../shared/types";

export const BrushContext = createContext({
  brush: { type: "PENCIL", width: 1, color: "#d946ef" },
  setBrush: (brush: IBrush) => {},
});
