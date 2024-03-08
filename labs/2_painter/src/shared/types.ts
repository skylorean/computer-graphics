export type CanvasPoint = {
  x: number;
  y: number;
};

export interface IBrush {
  type: string;
  width: number;
  color: string;
}
