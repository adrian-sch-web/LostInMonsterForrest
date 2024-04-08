import { Component, Input } from '@angular/core';
import { Fighter } from '../objects/Fighter';

@Component({
  selector: 'app-healthbar',
  standalone: true,
  imports: [],
  templateUrl: './healthbar.component.html',
  styleUrl: './healthbar.component.scss'
})
export class HealthbarComponent {
  @Input() fighter?: Fighter;
  @Input() flip: boolean = false;


  calcHpBar(fighter: Fighter): string {
    return (290 * fighter.hp / fighter.maxHp).toString();
  }

  getFlip(flip: boolean): string {
    if (flip) {
      return "scale(1,1) scale(-1,1)";
    }
    return "";
  }
}
