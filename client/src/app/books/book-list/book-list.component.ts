import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/_models/book';
import { BookParams } from 'src/app/_models/bookParams';
import { Pagination } from 'src/app/_models/pagination';
import { Translation } from 'src/app/_models/translation';
import { Verse } from 'src/app/_models/verse';
import { VerseParams } from 'src/app/_models/verseParams';
import { BooksService } from 'src/app/_services/books.service';
import { VersesService } from 'src/app/_services/verses.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit{
  books: Book[] = [];
  verses: Verse[] = [];
  groupedVerses: { [key: string]: Verse[] } = {};
  sectionTitles: any[] = [];
  translationBooks: Book[] = [];
  translations: Translation[] = [];
  selectedBook : Book = { book: 'Matthew', bookID: '', bookOrder:1, chapterCount: 28, verseCount: 1069, abbrebiations: ['ՄԱՏԹԷՈՍ', 'Matt', 'Mateo', 'Mt'], labels: 'Matthew', testament: 'New', title: '', translationId: 'e1994-T'  };  
  selectedTranslation : string = "e1994-T";
  selectedChapter : number = 1;

  chapterNums: number[] = [...Array(60).keys()].map(k => k+1);
  verseNums: number[] = []; //[...Array(80).keys()].map(k => k+1);

  verseCount:number = 0;
  
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;

  verseParams: VerseParams | undefined;
  versePagination: Pagination | undefined;

  paginationMode = 'NoPages';
  
  constructor(private bookService: BooksService, private verseService: VersesService) {
    this.bookParams = this.bookService.getBookParams();
    this.verseParams = this.verseService.getVerseParams();
  }

  ngOnInit(): void {
    this.loadBooks();
    this.loadTranslations();
    this.onChapterChanged();
  }

  loadTranslations() {
      this.bookService.getTranslations().subscribe({
        next: response => {
          console.log(response);
          if (response.result) {
            this.translations = response.result;
            this.loadTranslationBooks(this.selectedTranslation);
             this.translations.forEach((element: any) => {
               console.log(element);
             });
          }
        }
      })
  }
  objectKeys = Object.keys;

// https://decipher.dev/30-seconds-of-typescript/docs/orderBy/
  orderBy = <T = Object>(
    arr: T[],
    props: (keyof T)[],
    orders?: ("asc" | "desc")[]
  ) =>
    [...arr].sort((a, b) =>
      props.reduce((acc, prop, i) => {
        if (acc === 0) {
          const [p1, p2] =
            orders && orders[i] === "desc"
              ? [b[prop], a[prop]]
              : [a[prop], b[prop]];
          acc = p1 > p2 ? 1 : p1 < p2 ? -1 : 0;
        }
        return acc;
      }, 0)
    );


  loadBooks() {
      if (this.bookParams) {
        this.bookService.setBookParams(this.bookParams);
        this.bookService.getBooks(this.bookParams).subscribe({
          next: response => {
            console.log(response);
            if (response.result && response.pagination) {
              
              //this.books = response.result.orderBy((a: { testament: any; })=>a.testament).orderBy ((x: { bookOrder: any; })=>x.bookOrder);
              this.books = this.orderBy(response.result, ['testament', 'bookOrder'], ["desc", "asc"]);
              this.pagination = response.pagination;

              
              // this.books.forEach((element: any) => {
              //   console.log(element);
              // });
            }
          }
        })
      }
    }

    loadVerses(verseParams: VerseParams) {
      if (verseParams) {
        this.verseParams = verseParams;
        
        this.verseService.setVerseParams(verseParams);
        this.verseService.getVerses(verseParams).subscribe({
          next: response => {
            if (response.result ) {
              this.verses = this.orderBy(response.result, ['verse'], ["asc"]);
              this.verseCount = this.verses.length;
              this.groupedVerses = this.verses.reduce(
                (result:any, currentValue:any) => { 
                  (result[currentValue['sectionTitle']] = result[currentValue['sectionTitle']] || []).push(currentValue);
                  return result;
                }, {});

                this.sectionTitles = Object.keys(this.groupedVerses);
                this.sectionTitles.forEach(element => {
                  console.log(element);

                  this.groupedVerses[element].forEach((verse: any) => {
                    console.log(verse);
                  });
                });

                //console.log(this.groupedVerses);
                // Object.keys(this.groupedVerses).forEach((element: any) => {
                //      console.log(element);
                //      console.log(this.groupedVerses[element]);
                //    });

              this.versePagination = response.pagination;

              if(this.versePagination)
              {
                this.verseNums = [...Array(this.pagination?.totalItems).keys()].map(k => k+1);
              }
            }
          }
        })
      }
    }

  loadTranslationBooks(translation: string) {
    if (this.bookParams) {
      this.bookService.getTranslationBooks(translation).subscribe({
        next: response => {
          console.log(response);
          if (response.result) {
            this.translationBooks = this.orderBy(response.result, ['testament', 'bookOrder'], ["asc", "asc"]);
            this.selectedBook = this.translationBooks[0];
          }
        }
      })
    }
  }

  pageChanged(event: any) {
    console.log(event);
    if (this.bookParams && this.bookParams?.pageNumber !== event.page) {
      this.bookParams.pageNumber = event.page;
      this.bookService.setBookParams(this.bookParams);
      this.loadBooks();
    }
  }

  onTranslationChanged(translation: string){
      console.log(translation);
      this.translationBooks = [];
      this.loadTranslationBooks(translation);
   }

  onBookChanged(){
    console.log(this.paginationMode);
    console.log( JSON.stringify(this.selectedBook));
    console.log(this.selectedTranslation);
    var ps = new VerseParams();

    this.selectedChapter = 1;
    ps.translation = this.selectedTranslation;
    ps.book =this.selectedBook.book;
    ps.chapter = this.selectedChapter;
    ps.verse = 1;
    ps.verseTo = 100;
    ps.shouldPaginate = this.paginationMode == "NoPages" ? true : false;
    console.log(ps);
    var totalChapters = this.pagination?.totalItems;
    console.log(totalChapters);
    this.chapterNums = [...Array(this.selectedBook.chapterCount).keys()].map(k => k+1);
    this.loadVerses(ps);
 }

 onChapterChanged(){
  console.log("onChapterChanged");
  console.log(`selectedChapter: ${this.selectedChapter}`);
  console.log(this.verseParams);
  if (this.verseParams) {
    this.verseParams.pageNumber = 1;
    if(this.pagination) this.pagination.currentPage = 1;

    this.verseParams.chapter = this.selectedChapter; //parseInt(chapter);
    console.log(this.verseParams);
    this.verseService.setVerseParams(this.verseParams);
    this.loadVerses(this.verseParams);
  }

}

onVerseChanged(verse: string){
  console.log(verse);
}


onVersePaginationPageChanged(event: any){
  console.log(event);

  if (this.verseParams && this.verseParams?.pageNumber !== event.page) {
    this.verseParams.pageNumber = event.page;
    this.verseService.setVerseParams(this.verseParams);
    this.loadVerses(this.verseParams);
  }

}

onNext(){
  this.selectedChapter++;
  this.onChapterChanged();
}

onPrev(){
  this.selectedChapter--;
  this.onChapterChanged();
}

}
