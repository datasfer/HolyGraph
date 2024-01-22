import { Component, OnInit } from '@angular/core';
import { Book } from 'src/app/_models/book';
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
  
  constructor(private bookService: BooksService) {}

  ngOnInit(): void {
    this.loadBooks();
    console.log("book-list");
  }

  loadBooks() {
      this.bookService.getBooks().subscribe({
        next: response => {
          if (response.result) {
            this.books = response.result;
            this.pagination = response.pagination;

            this.books.forEach((element: any) => {
              console.log(element);
            });
          }
        }
      })
  }
}
