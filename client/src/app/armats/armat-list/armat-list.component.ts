import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Armat } from 'src/app/_models/armat';
import { ArmatParams } from 'src/app/_models/armatParams';
import { Pagination } from 'src/app/_models/pagination';
import { ArmatsService } from 'src/app/_services/armats.service';

@Component({
  selector: 'app-armat-list',
  templateUrl: './armat-list.component.html',
  styleUrls: ['./armat-list.component.css']
})
export class ArmatListComponent implements OnInit{

  selectedLetter: string= 'Ա';
  letters: string[] = ['ա', 'բ', 'գ', 'դ', 'ե', 'զ', 'է', 'ը', 'թ', 'ժ', 'ի', 'լ', 'խ', 'ծ', 'կ', 'հ', 'ձ', 'ղ', 'ճ', 'մ', 'յ', 'ն', 'շ', 'ո', 'չ', 'պ', 'ջ', 'ռ', 'ս', 'վ', 'տ', 'ր', 'ց', 'ւ', 'փ', 'ք', 'օ', 'ֆ'];
  armats: Armat[] = [];
  armatPagination: Pagination | undefined;
  armatParams: ArmatParams  | undefined;

  constructor( private route: ActivatedRoute, private armatService: ArmatsService) {
    this.armatParams = new ArmatParams();
  }

  ngOnInit(): void {
    this.loadArmats(this.armatParams);
  }

  loadArmats(armatParams: ArmatParams | undefined) {
    // this is done for back from armat-tokens page
    // but this param is full arat not a letter from armat-token url
    // we can take firs letter and it is.
    var letter = String(this.route.snapshot.paramMap.get('startsWith'));
    console.log(`loadArmats: ${letter}`);
    if(letter && letter != 'null') 
    {
      console.log(`--loadArmats: ${letter}`);
      this.onGoToArmatsStartsWith(letter[0]);
      return;
    }

    console.log("get armats");

    if (armatParams) {
      this.armatParams = armatParams;
      
      console.log("get armats");

      this.armatService.getArmats(armatParams).subscribe({
        next: response => {
          if (response.result ) {
            this.armats = response.result;

            this.armatPagination = response.pagination;
          }
        }
      })
    }
  }

  loadArmatsStartsWith(startsWith: string) {
      this.armatService.getArmatsStartsByLetter(startsWith).subscribe((response: any) => this.armats = response);
}

  onArmatPaginationPageChanged(event: any){
    console.log(event);
  
    if (this.armatParams && this.armatParams?.pageNumber !== event.page) {
      this.armatParams.pageNumber = event.page;
      this.loadArmats(this.armatParams);
    }
  }

  onGoToArmatsStartsWith(letter: string){
    this.selectedLetter  = letter;
    this.armatPagination = undefined;
    console.log(`onGoToArmatsStartsWith: ${letter}`);
    console.log(`this.selectedLetter: ${this.selectedLetter}`);
    this.loadArmatsStartsWith(letter);
  }

}
