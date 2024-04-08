import { Component, EventEmitter, HostListener, Input, OnInit, Output } from '@angular/core';
import { GameDto, GameService } from '../game.service';
import { Placeable, Position } from '../objects/Placeable';
import { PlaceableType } from '../objects/PlaceableType';
import { Entities } from '../objects/Entities';
import { Direction } from '../objects/Direction';
import { Floor } from '../objects/Floor';

@Component({
  selector: 'app-board',
  standalone: true,
  imports: [],
  templateUrl: './board.component.html',
  styleUrl: './board.component.scss'
})
export class BoardComponent implements OnInit {
  @Input() entities?: Entities;
  @Input() floor?: Floor[][];
  @Output() update = new EventEmitter<GameDto>;
  board: PlaceableType[][] = [];
  cellSize: string = "";
  usedSprites: Set<string> = new Set()
  legend: { name: string, url: string }[] = [];
  keyboardDirection: number = 0;
  floorCount = 0;
  constructor(private game: GameService) { }

  ngOnInit(): void {
    this.game.getBoard().then(floor => {
      this.floor = floor;
      this.createBoardMatrix();
      this.getSize();
      this.placeEntities();
      this.initializeLegend();
    });
  }

  @HostListener('window:keydown.ArrowUp', ['$event'])
  @HostListener('window:keydown.ArrowDown', ['$event'])
  @HostListener('window:keydown.ArrowLeft', ['$event'])
  @HostListener('window:keydown.ArrowRight', ['$event'])
  handleKeyDown(event: KeyboardEvent) {
    if (event.key == "ArrowUp") {
      this.move(Direction.Up);
    }
    if (event.key == "ArrowDown") {
      this.move(Direction.Down);
    }
    if (event.key == "ArrowLeft") {
      this.move(Direction.Left);
    }
    if (event.key == "ArrowRight") {
      this.move(Direction.Right);
    }
  }

  move(direction: Direction) {
    if (!this.checkDirection(direction)) {
      return;
    }
    this.game.walk(direction).then(game => {
      this.game.getBoard().then(floor => {
        this.floor = floor;
        this.resetBoard();
        this.entities = game.board;
        this.placeEntities();
        this.initializeLegend();
        this.update.emit(game);
      })
    })
  }

  placeEntities() {
    if (!this.entities) {
      return;
    }
    this.board[this.entities.door.position.x + 1][this.entities.door.position.y + 1] = PlaceableType.Door;
    this.usedSprites.add(PlaceableType[PlaceableType.Door]);
    this.board[this.entities.player.position.x + 1][this.entities.player.position.y + 1] = PlaceableType.Player;
    this.usedSprites.add(PlaceableType[PlaceableType.Player]);

    for (let monster of this.entities.monsters) {
      this.board[monster.position.x + 1][monster.position.y + 1] = this.game.MonsterType(monster.type);
      this.usedSprites.add(PlaceableType[this.game.MonsterType(monster.type)]);
    }

    for (let item of this.entities.items) {
      this.board[item.position.x + 1][item.position.y + 1] = this.game.ItemType(item.type);
      this.usedSprites.add(PlaceableType[this.game.ItemType(item.type)]);
    }
  }

  resetBoard() {
    this.board = [];
    if (this.floor) {
      this.createBoardMatrix();
    }
  }

  createBoardMatrix() {
    if (!this.floor) {
      return;
    }
    for (let i = 0; i < this.floor.length; i++) {
      this.board.push([]);
      for (let j = 0; j < this.floor[i].length; j++) {
        if (this.floor) {
          switch (this.floor[i][j]) {
            case Floor.Tree:
              this.board[i].push(PlaceableType.Tree);
              break;
            case Floor.Normal:
              this.board[i].push(PlaceableType.Empty);
              break;
            case Floor.Mud:
              this.usedSprites.add(Floor[Floor.Mud]);
              this.board[i].push(PlaceableType.Empty);
              break;
            case Floor.Road:
              this.usedSprites.add(Floor[Floor.Road])
              this.board[i].push(PlaceableType.Empty);
              break;
          }
        }
      }
    }
  }

  checkDirection(direction: Direction): boolean {
    if (!this.entities) return false;
    let newPosition: Position;
    switch (direction) {
      case Direction.Left:
        newPosition = new Position(this.entities.player.position.x - 1, this.entities.player.position.y)
        break;
      case Direction.Right:
        newPosition = new Position(this.entities.player.position.x + 1, this.entities.player.position.y)
        break;
      case Direction.Up:
        newPosition = new Position(this.entities.player.position.x, this.entities.player.position.y - 1)
        break;
      case Direction.Down:
        newPosition = new Position(this.entities.player.position.x, this.entities.player.position.y + 1)
        break;
    }
    return this.board[newPosition.x + 1][newPosition.y + 1] != PlaceableType.Tree;
  }

  getSize() {
    if (this.board.length == 0 || this.board[0].length == 0) {
      this.cellSize = "";
    }
    let maxWidth = window.innerWidth / this.board[0].length;
    let maxHeight = window.innerHeight / this.board.length;
    this.cellSize = "" + Math.ceil(Math.min(maxHeight, maxWidth) * 0.8) + "px";
  }


  getImageUrl(field: PlaceableType): string {
    return this.game.getImageUrl(field);
  }

  getFloorUrl(row: number, column: number): string {
    if (!this.floor) {
      return this.game.getFloorUrl(Floor.Normal);
    }
    switch (this.floor[row][column]) {
      case Floor.Normal:
        return this.game.getFloorUrl(Floor.Normal);
      case Floor.Mud:
        return this.game.getFloorUrl(Floor.Mud);
      case Floor.Road:
        return this.game.getFloorUrl(Floor.Road);
      case Floor.Tree:
        return this.game.getFloorUrl(Floor.Normal);
    }
  }

  getFloor(floor: Floor): string {
    return this.game.getFloorUrl(floor);
  }

  initializeLegend() {
    this.legend = [];
    this.usedSprites.forEach(sprite => {
      this.legend.push({ name: sprite, url: this.game.getImagePath(sprite) });
    });

  }
}
