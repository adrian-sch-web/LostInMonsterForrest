import { Component, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { Fighter, Monster, MonsterType } from '../objects/Fighter';
import { Attack } from '../objects/Attack';
import { GameDto, GameService } from '../game.service';
import { PlaceableType } from '../objects/PlaceableType';
import { HealthbarComponent } from '../healthbar/healthbar.component';

@Component({
  selector: 'app-fight',
  standalone: true,
  imports: [HealthbarComponent],
  templateUrl: './fight.component.html',
  styleUrl: './fight.component.scss'
})
export class FightComponent {
  @Input() monster?: Monster;
  @Input() player?: Fighter;
  @Input() attacks: Attack[] = [];

  @Output() update = new EventEmitter<GameDto>;
  risky: boolean = false;

  constructor(private gameService: GameService) { }

  @HostListener('window:keydown.ArrowUp', ['$event'])
  @HostListener('window:keydown.ArrowDown', ['$event'])
  @HostListener('window:keydown.Enter', ['$event'])
  handleKeyDown(event: KeyboardEvent) {
    if (event.key == "ArrowUp" && this.risky) {
      this.risky = false;
    }
    if (event.key == "ArrowDown" && !this.risky) {
      this.risky = true;
    }
    if (event.key == "Enter") {
      this.attack();
    }
  }

  attack(risky = this.risky) {
    this.risky = risky;
    this.gameService.fight(risky).then(game => {
      this.monster = game.board.monsters.find(a => a.id == this.monster?.id);
      this.player = game.board.player;
      this.attacks = game.attacks;
      this.update.emit(game);
    });
  }

  PlayerImage(): string {
    return this.gameService.getFightImageUrl(PlaceableType.Player);
  }

  MonsterImage(): string {
    if (!this.monster) {
      return "";
    }
    return this.gameService.getFightImageUrl(this.gameService.MonsterType(this.monster.type));
  }

  message(attack: Attack): string {
    let attackString: string = "";
    if (!this.monster) {
      return "";
    }
    if (attack.attacker) {
      attackString += "You attacked " + MonsterType[this.monster.type] + " with a ";
      attackString += attack.risky ? "risky " : "normal ";
      attackString += "attack. ";
      if (attack.damage > 0) {
        attackString += attack.criticalStrike ? "It was a critical strike! " : "";
        attackString += MonsterType[this.monster.type] + " took " + attack.damage + " damage "
        attackString += attack.kill ? MonsterType[this.monster.type] + " died" : "";
      }
      else {
        attackString += "Oh no, " + MonsterType[this.monster.type] + " dodged the attack!"
      }

    } else {
      attackString += MonsterType[this.monster.type] + " attacked you. ";
      attackString += attack.criticalStrike ? "It was a critical strike! " : "";
      attackString += "You took " + attack.damage + " damage.";
      attackString += attack.kill ? "You died" : "";
    }
    return attackString;
  }

  calcHpBar(fighter: Fighter): string {

    return (290 * fighter.hp / fighter.maxHp).toString();
  }
}
