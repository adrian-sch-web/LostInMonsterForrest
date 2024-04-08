export class Placeable {
    constructor(
        public position: Position
    ) { }
}

export class Item extends Placeable {
    constructor(
        public id: number,
        public type: ItemType,
        position: Position
    ) {
        super(position);
    }
}

export class Position {
    constructor(
        public x: number,
        public y: number
    ) { }
}

export enum ItemType {
    Crit,
    Damage,
    Heal
}