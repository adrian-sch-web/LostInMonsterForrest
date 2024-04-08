import { Placeable, Position } from "./Placeable";

export class Fighter extends Placeable {
    constructor(
        position: Position,
        public maxHp: number,
        public hp: number,
        public damage: number,
        public critChance: number) {
        super(position);
    }
}

export class Monster extends Fighter {
    constructor(
        public id: number,
        public type: MonsterType,
        position: Position,
        maxHp: number,
        hp: number,
        damage: number,
        critChance: number
    ) {
        super(position, maxHp, hp, damage, critChance);
    }
}

export enum MonsterType {
    Giganto,
    Normalo,
    Attacko
}