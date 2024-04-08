import { Injectable } from '@angular/core';
import { ItemType } from './objects/Placeable';
import { Direction } from './objects/Direction';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';
import { Attack } from './objects/Attack';
import { MonsterType } from './objects/Fighter';
import { Entities } from './objects/Entities';
import { State } from './objects/State';
import { PlaceableType } from './objects/PlaceableType';
import { LeaderboardEntry } from './objects/LeaderboardEntry';
import { Floor } from './objects/Floor';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  gameState?: GameDto;
  private url = "https://localhost:7217";
  constructor(private http: HttpClient) { }

  async getBoard(): Promise<Floor[][]> {
    let result = await firstValueFrom(this.http.get<BoardDto>(this.url + '/DungeonGame/Board'));
    let floor = result.floorTypeBoard;
    let floorWithBorder: Floor[][] = [];
    let firstAndLastRow: Floor[] = [];
    for (let i = 0; i < floor.length + 2; i++) {
      firstAndLastRow.push(Floor.Tree);
    }
    floorWithBorder.push(firstAndLastRow);
    for (let i = 0; i < floor.length; i++) {
      floorWithBorder.push([Floor.Tree, ...floor[i], Floor.Tree]);
    }
    floorWithBorder.push(firstAndLastRow);
    return floorWithBorder;

  }

  async getGame(): Promise<GameDto> {
    let result = await firstValueFrom(this.http.get<GameDto>(this.url + '/DungeonGame/State'));
    return result;
  }

  async walk(direction: Direction): Promise<GameDto> {
    let result = await firstValueFrom(this.http.post<GameDto>(this.url + '/DungeonGame/Move', new DirectionDto(direction)));
    return result;
  }

  async fight(risky: boolean): Promise<GameDto> {
    let result = await firstValueFrom(this.http.post<GameDto>(this.url + '/DungeonGame/Fight', new RiskyDto(risky)));
    return result;
  }

  async restart() {
    await firstValueFrom(this.http.delete(this.url + '/DungeonGame/Restart'));
  }

  async getEntities(): Promise<Entities> {
    let game = await this.getGame();
    return game.board;
  }

  async getLeaderBoard(): Promise<LeaderboardEntry[]> {
    let leaderboard = await firstValueFrom(this.http.get<LeaderboardEntry[]>(this.url + '/DungeonGame/Leaderboard'));
    return leaderboard;
  }

  async submitScore(entry: LeaderboardEntry): Promise<LeaderboardEntry[]> {
    let leaderboard = await firstValueFrom(this.http.post<LeaderboardEntry[]>(this.url + '/DungeonGame/SubmitScore', entry));
    return leaderboard;
  }

  getFightImageUrl(field: PlaceableType): string {
    return this.getImageUrl(field) + "Fight";
  }

  getImageUrl(field: PlaceableType): string {
    let url2ndPart = "/DungeonGame/Image/";
    return this.url + url2ndPart + PlaceableType[field];
  }

  getFloorUrl(floor: Floor): string {
    switch (floor) {
      case Floor.Normal:
        return "https://localhost:7217/DungeonGame/Image/Floor";
      case Floor.Mud:
        return "https://localhost:7217/DungeonGame/Image/Mud";
      case Floor.Road:
        return "https://localhost:7217/DungeonGame/Image/Road";
      default:
        return "https://localhost:7217/DungeonGame/Image/Floor";
    }
  }

  getImagePath(image: string): string {
    return this.url + "/DungeonGame/Image/" + image;
  }

  getTombImageUrl(): string {
    return this.url + "/DungeonGame/Image/Tomb";
  }

  MonsterType(monsterType: MonsterType): PlaceableType {
    switch (monsterType) {
      case MonsterType.Giganto:
        return PlaceableType.Giganto;
      case MonsterType.Normalo:
        return PlaceableType.Normalo;
      case MonsterType.Attacko:
        return PlaceableType.Attacko;
    }
  }

  ItemType(itemType: ItemType): PlaceableType {
    switch (itemType) {
      case ItemType.Crit:
        return PlaceableType.CritUp;
      case ItemType.Damage:
        return PlaceableType.DamageUp;
      case ItemType.Heal:
        return PlaceableType.Heal;
    }
  }
}

class RiskyDto {
  risky: boolean;
  constructor(risky: boolean) {
    this.risky = risky;
  }
}

class DirectionDto {
  direction: Direction;
  constructor(dir: Direction) {
    this.direction = dir;
  }
}

class BoardDto {
  floorTypeBoard: Floor[][] = [];
}

export interface GameDto {
  attacks: Attack[];
  board: Entities;
  state: State;

}
