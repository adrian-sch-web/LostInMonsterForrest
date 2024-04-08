import { Component, EventEmitter, HostListener, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { State } from '../objects/State';
import { LeaderboardEntry } from '../objects/LeaderboardEntry';
import { GameService } from '../game.service';

@Component({
  selector: 'app-death',
  standalone: true,
  imports: [],
  templateUrl: './death.component.html',
  styleUrl: './death.component.scss'
})
export class DeathComponent implements OnInit, OnChanges {
  @Input() stats?: State;
  @Output() restart = new EventEmitter<boolean>;
  leaderboard: LeaderboardEntry[] = [];
  saveSelected: boolean = true;
  name = "";

  constructor(private game: GameService) { }

  ngOnInit(): void {
    this.game.getLeaderBoard().then(a => {
      this.leaderboard = a;
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.stats?.recordSubmitted) {
      this.saveSelected = false;
    }
  }

  @HostListener('window:keydown.Enter', ['$event'])
  @HostListener('window:keydown.ArrowUp', ['$event'])
  @HostListener('window:keydown.ArrowDown', ['$event'])
  handleKeyDown(event: KeyboardEvent) {
    if (event.key == "Enter") {
      if (this.saveSelected) {
        this.submit();
      }
      else {
        this.restartGame();
      }
    }
    if (event.key == "ArrowUp") {
      if (!this.stats?.recordSubmitted && !this.saveSelected) {
        this.saveSelected = true;
      }
    }
    if (event.key == "ArrowDown") {
      if (this.saveSelected) {
        this.saveSelected = false;
      }
    }
  }

  restartGame() {
    this.restart.emit(true);
  }

  submit() {
    if (this.stats && this.name != "") {
      this.saveSelected = false;
      this.name = this.name.trim();
      this.game.submitScore(new LeaderboardEntry(this.getId(), this.name, this.stats.floor, this.stats.kills)).then(newLeaderboard => {
        this.leaderboard = newLeaderboard;
      });
      this.stats.recordSubmitted = true;
    }
  }


  getId(): number {
    return 0;
  }

  getTombImageUrl(): string {
    return this.game.getTombImageUrl()
  }
}
