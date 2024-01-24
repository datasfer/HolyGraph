import { Component, OnInit } from '@angular/core';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
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
  translationBooks: Book[] = [];
  translations: Translation[] = [];
  selectedBook : string = "Matthew";  
  selectedTranslation : string = "e1994-T";
  selectedChapter : number = 1;

  chapterNums: number[] = [...Array(60).keys()].map(k => k+1);
  verseNums: number[] = [...Array(80).keys()].map(k => k+1);

  
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;

  verseParams: VerseParams | undefined;
  versePagination: Pagination | undefined;
  
  constructor(private bookService: BooksService, private verseService: VersesService) {
    this.bookParams = this.bookService.getBookParams();
    this.verseParams = this.verseService.getVerseParams();
  }

  ngOnInit(): void {
    this.loadBooks();
    this.loadTranslations();
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
            console.log(response);
            if (response.result && response.pagination) {
              this.verses = this.orderBy(response.result, ['verse'], ["asc"]);
              this.versePagination = response.pagination;
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

  onBookChanged(book: string){
    console.log(book);
    console.log(this.selectedTranslation);
    var ps = new VerseParams();

    ps.translation = this.selectedTranslation;
    ps.book = this.selectedBook;
    ps.chapter = this.selectedChapter;
    ps.verse = 1;
    ps.verseTo = 100;

    this.loadVerses(ps);
 }

 onChapterChanged(chapter: string){
  console.log(chapter);

}

onVerseChanged(verse: string){
  console.log(verse);
}


onVersePaginationChanged(verse: any){
  console.log(verse);
}

}
