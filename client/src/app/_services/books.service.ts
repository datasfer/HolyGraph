import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../_models/book';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map } from 'rxjs';
import { BookParams } from '../_models/bookParams';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  baseUrl = environment.apiUrl;
  members: Book[] = [];
  memberCache = new Map();
  bookParams: BookParams | undefined;
  
  constructor(private http: HttpClient) {
    this.bookParams = new BookParams();
   }

  getBooks(bookParams: BookParams) {
    let params = getPaginationHeaders(bookParams.scripture, bookParams.pageNumber, bookParams.pageSize);
    return getPaginatedResult<Book[]>(this.baseUrl + 'books', params, this.http).pipe(
      map((response: any) => {
        console.log(response);
        return response;
      })
    )
  }

  getBookParams() {
    return this.bookParams;
  }

  setBookParams(bookParams: BookParams) {
    this.bookParams = bookParams;
  }

  
}
