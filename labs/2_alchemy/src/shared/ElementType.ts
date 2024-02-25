export enum ElementType {
  FIRE = "FIRE",
  WATER = "WATER",
  LAVA = "LAVA",
  AIR = "AIR",
  DUST = "DUST",
  GUNPOWDER = "GUNPOWDER",
  SMOKE = "SMOKE",
  EXPLOSION = "EXPLOSION",
  ENERGY = "ENERGY",
  STORM = "STORM",
  METAL = "METAL",
  ELECTRICITY = "ELECTRICITY",
  HYDROGEN = "HYDROGEN",
  OXYGEN = "OXYGEN",
  OZONE = "OZONE",
  DIRT = "DIRT",
  GEYSER = "GEYSER",
  STREAM = "STREAM",
  BOILER = "BOILER",
  PRESSURE = "PRESSURE",
  VOLCANO = "VOLCANO",
}

export interface IElement {
  unlocked: boolean;
  type: ElementType;
}
