import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BoardComponent } from './board/board.component';
import { HttpClientModule } from '@angular/common/http';
import { GameDto, GameService } from './game.service';
import { FightComponent } from './fight/fight.component';
import { Monster } from './objects/Fighter';
import { DeathComponent } from "./death/death.component";
import { Floor } from './objects/Floor';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [RouterOutlet, BoardComponent, FightComponent, HttpClientModule, DeathComponent]
})
export class AppComponent implements OnInit {
  title = 'DungeonGame';
  failed = false;
  game?: GameDto;
  floor?: Floor[][];


  constructor(private gameService: GameService) { }

  ngOnInit() {
    this.gameService.getBoard().then(floor => {
      this.floor = floor;
      this.gameService.getGame().then(game => this.game = game);
    }).catch(ex => this.failed = true);
  }

  monsterInFight(): Monster | undefined {
    return this.game?.board.monsters.find(a => a.id == this.game?.state.fightMode);
  }

  nextFloorCheck(game: GameDto) {
    if (!this.game) {
      this.game = game;
      return;
    }
    let temp = this.game.state.floor;
    this.game = game;
    if (this.game.state.floor != temp) {
      this.gameService.getBoard().then(floor => this.floor = floor);
    }
  }

  retry() {
    this.failed = false;
    this.ngOnInit();
  }

  restart() {
    this.gameService.restart();
    this.gameService.getGame().then(game => this.game = game);
  }
}
