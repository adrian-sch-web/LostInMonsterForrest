import { Monster } from "./Fighter";

export class Attack {
    constructor(
        public attacker: boolean,
        public monster: Monster,
        public risky: boolean,
        public criticalStrike: boolean,
        public kill: boolean,
        public damage: number
        ) {}
}