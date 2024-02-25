import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Armat } from 'src/app/_models/armat';
import { ArmatsService } from 'src/app/_services/armats.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-armat-alphabetical',
  templateUrl: './armat-alphabetical.component.html',
  styleUrls: ['./armat-alphabetical.component.css']
})
export class ArmatAlphabeticalComponent implements OnInit {
  
  armats: Armat[] = [];
  letter: string = 'Ա';

  constructor(
    private route: ActivatedRoute,
    private armatService: ArmatsService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.loadArmatsStartsWith();
    console.log("ArmatAlphabeticalComponent");
  }


  loadArmatsStartsWith() {
    this.letter = String(this.route.snapshot.paramMap.get('startsWith'));
    console.log(this.letter);
    this.armatService.getArmatsStartsByLetter(this.letter).subscribe((response: any) => this.armats = response);
}

}
