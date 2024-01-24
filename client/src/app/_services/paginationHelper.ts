import { HttpClient, HttpParams } from "@angular/common/http";
import { map } from "rxjs";
import { PaginatedResult } from "../_models/pagination";
import { VerseParams } from "../_models/verseParams";

export function getPaginatedResult<T>(url: string, params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>;
    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if (response.body) {
          paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          paginatedResult.pagination = JSON.parse(pagination);
        }
        return paginatedResult;
      })
    );
  }

  export function getPaginationHeaders(scripture: string, pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('scripture', scripture);
    params = params.append('pageNumber', pageNumber);
    params = params.append('pageSize', pageSize);

    return params;
  }

  export function buildHttpParams(verseParams: VerseParams) {
    let params = new HttpParams();

    console.log(verseParams);
    params = params.append('scripture', verseParams.scripture);
    params = params.append('translation', verseParams.translation);
    params = params.append('book', verseParams.book);
    params = params.append('chapter', verseParams.chapter);
    params = params.append('verse', verseParams.verse);
    params = params.append('verseTo', verseParams.verseTo);
    params = params.append('pageNumber', verseParams.pageNumber);
    params = params.append('pageSize', verseParams.pageSize);

    return params;
  }