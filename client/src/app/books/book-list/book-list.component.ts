import { Component, OnInit } from '@angular/core';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
import { Book } from 'src/app/_models/book';
import { BookParams } from 'src/app/_models/bookParams';
import { Pagination } from 'src/app/_models/pagination';
import { Translation } from 'src/app/_models/translation';
import { BooksService } from 'src/app/_services/books.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit{
  books: Book[] = [];
  translationBooks: Book[] = [];
  translations: Translation[] = [];
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;
  
  constructor(private bookService: BooksService) {
    this.bookParams = this.bookService.getBookParams();
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
             this.translations.forEach((element: any) => {
               console.log(element);
             });
          }
        }
      })
  }

  loadBooks() {
    if (this.bookParams) {
      this.bookService.setBookParams(this.bookParams);
      this.bookService.getBooks(this.bookParams).subscribe({
        next: response => {
          console.log(response);
          if (response.result && response.pagination) {
            this.books = response.result;
            this.pagination = response.pagination;
            // this.books.forEach((element: any) => {
            //   console.log(element);
            // });
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
            this.translationBooks = response.result;
            // this.books.forEach((element: any) => {
            //   console.log(element);
            // });
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

  selectItem(value: string){
      console.log(value);
      this.loadTranslationBooks(value);
   }

  onTranslationChanged(translation: string){
    this.loadTranslationBooks(translation);
 }

}
