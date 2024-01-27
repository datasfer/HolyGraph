import { Injectable } from '@angular/core';
import { VerseParams } from '../_models/verseParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { buildHttpParams, getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Verse } from '../_models/verse';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class VersesService {
  baseUrl = environment.apiUrl;
  memberCache = new Map();
  verseParams: VerseParams | undefined;

  constructor(private http: HttpClient) {
    this.verseParams = new VerseParams();
   }

   getVerses(verseParams: VerseParams) {
    let params = buildHttpParams(verseParams);
    let res = undefined; //Observable<PaginatedResult<Verse>>;
    console.log(verseParams.shouldPaginate);
    if(verseParams.shouldPaginate)
    {
      res = getPaginatedResult<Verse[]>(this.baseUrl + 'verses', params, this.http);
    }
    else
    {
      let url = this.baseUrl + 'verses/' + verseParams.translation + '/books/' + verseParams.book + '/chapters/' + verseParams.chapter.toString();
      res = getPaginatedResult<Verse[]>(url, new HttpParams(), this.http);
    }
    return res.pipe(
      map((response: any) => {
        console.log(response);
        return response;
      })
    )
  }


  getVerses_(translation: string, book: string, chapter:number) {
    let url = this.baseUrl + 'verses/' + translation + '/books/' + book + '/chapters/' + chapter.toString();
    return getPaginatedResult<Verse[]>(url, new HttpParams(), this.http).pipe(
      map((response: any) => {
        console.log(response);
        return response;
      })
    )
  }

  getVerseParams() {
    return this.verseParams;
  }

  setVerseParams(verseParams: VerseParams) {
    this.verseParams = verseParams;
  }
}
