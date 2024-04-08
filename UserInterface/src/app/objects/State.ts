export class State {
    running: boolean = true;
    fightMode: number = -1;
    recordSubmitted: boolean = false;
    constructor(
        public floor: number,
        public kills: number,
        public steps: number) {
    }
}