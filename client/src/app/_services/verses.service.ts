import { Injectable } from '@angular/core';
import { VerseParams } from '../_models/verseParams';
import { environment } from 'src/environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { buildHttpParams, getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Verse } from '../_models/verse';

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
    return getPaginatedResult<Verse[]>(this.baseUrl + 'verses', params, this.http).pipe(
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
