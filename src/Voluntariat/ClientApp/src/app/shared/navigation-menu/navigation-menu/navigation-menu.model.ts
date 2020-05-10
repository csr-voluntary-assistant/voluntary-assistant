export interface NavigationMenuModel {
  name: string;
  url: string[];
  order: number;
  position: NavigationPosition;
  type?: NavigationMenuType;
}

export enum NavigationPosition {
  Left = 0,
  Right = 1
}

export enum NavigationMenuType {
  Normal = 0,
  Highlighted = 1,
  HighlightedWithBorder = 2
}
