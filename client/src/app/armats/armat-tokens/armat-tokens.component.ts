import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArmatsService } from 'src/app/_services/armats.service';
import { Location } from '@angular/common';
import { Armat } from 'src/app/_models/armat';
import { Token } from 'src/app/_models/token';

@Component({
  selector: 'app-armat-tokens',
  templateUrl: './armat-tokens.component.html',
  styleUrls: ['./armat-tokens.component.css']
})
export class ArmatTokensComponent implements OnInit {

  tokens: Token[] = [];
  armat: string = '';

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private armatService: ArmatsService,
    private location: Location
  ) {}


  ngOnInit(): void {
    this.getTokens();
  }


  getTokens(): void {
    //const id = Number(this.route.snapshot.paramMap.get('id'));
    this.armat = String(this.route.snapshot.paramMap.get('armat'));
    console.log(this.armat);
    this.armatService.getTokensByArmat(this.armat).subscribe((response: any) => this.tokens = response);
  }

  goBack(): void {
    this.location.back();
    var url = `/armats/${this.armat}`;
    this.router.navigateByUrl(url);
  }

}
