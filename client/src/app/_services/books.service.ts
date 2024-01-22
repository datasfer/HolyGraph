import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Book } from '../_models/book';
import { environment } from 'src/environments/environment.development';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  baseUrl = environment.apiUrl;
  members: Book[] = [];
  memberCache = new Map();
  
  constructor(private http: HttpClient) { }


  getBooks() {
    
    let params = getPaginationHeaders(1, 10);
    return getPaginatedResult<Book[]>(this.baseUrl + 'scriptures', params, this.http).pipe(
      map((response: any) => {
        console.log(response);
        return response;
      })
    )
  }

  
}
