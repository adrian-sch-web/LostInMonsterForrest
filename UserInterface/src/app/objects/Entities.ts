import { Fighter, Monster } from "./Fighter";
import { Placeable, Item } from "./Placeable";

export class Entities {
    constructor(
        public player: Fighter,
        public door: Placeable,
        public monsters: Monster[],
        public items: Item[],
        public trees: Placeable[]
    ) { }
}