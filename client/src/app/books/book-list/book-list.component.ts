import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/_models/book';
import { BookParams } from 'src/app/_models/bookParams';
import { Pagination } from 'src/app/_models/pagination';
import { BooksService } from 'src/app/_services/books.service';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css']
})
export class BookListComponent implements OnInit{
  books: Book[] = [];
  pagination: Pagination | undefined;
  bookParams: BookParams | undefined;
  
  constructor(private bookService: BooksService) {
    this.bookParams = this.bookService.getBookParams();
  }

  ngOnInit(): void {
    this.loadBooks();
    console.log("book-list");
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

  pageChanged(event: any) {
    console.log(event);
    if (this.bookParams && this.bookParams?.pageNumber !== event.page) {
      this.bookParams.pageNumber = event.page;
      this.bookService.setBookParams(this.bookParams);
      this.loadBooks();
    }
  }

}
